using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using ContactHub_MVC.Helper;
using ContactHub_MVC.Filters;
using System.Threading.Tasks;
using System.Collections.Generic;
using ContactHub_MVC.Models.UserModel;
using ContactHub_MVC.CommonData.Constants;

namespace ContactHub_MVC.Controllers
{
    [ContactHubAuthorizeFilter]
    [ContactHubAuthenticationFilter]
    [OutputCache(NoStore = true, Duration = 1)]
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Dashboard()
        {
            var contactList = GetContactList(true);
            var model = new ContactViewModel()
            {
                ContactList = contactList.Take(10),
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult SearchContacts()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddContacts()
        {
            var model = new AddContactsViewModel()
            {
                Contacts = GetContactList(false).Take(10)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddContacts(AddContactsViewModel model,IList<HttpPostedFileBase> uploadFiles)
        {
            var files = Utility.GetFilesToUpload(model.FileNames, uploadFiles).Result;
            return RedirectToAction("AddContacts");
        }

        [HttpGet]
        public ActionResult EditContacts()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SyncContacts()
        {
            var model = new SyncContacts()
            {
                ContactList = GetContactList(false).Take(10).ToList(),
                ContactModeList = Utility.ContactMode(),
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Settings()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetContactListByPage(int pageNumber)
        {
            var skipContactCount = (pageNumber - 1) * 10;
            var contactList = GetContactList(true).Skip(skipContactCount).Take(10);
            return PartialView("Partial/_Contacts", contactList);
        }

        [HttpGet]
        public ActionResult RemoveContact(string Id)
        {
            var ContactList = GetContactList(true).Take(10);
            var NewContactList = ContactList.Where(x => x.Id != Id);
            var renderView = Utility.PartialViewToHtml(this, "Partial/_Contacts", NewContactList).Result;
            return Json(new { result = true, newList = renderView }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetContactById(string Id)
        {
            var ContactList = GetContactList(true);
            var Contact = ContactList.FirstOrDefault(x => x.Id.Equals(Id));
            return PartialView("Partial/_EditContact", Contact);
        }

        [HttpPost]
        public async Task<ActionResult> DownloadContact(int FileType,params int[] Ids)
        {
            var contactList = new List<ContactDetails>();
            foreach (var id in Ids)
            {
                contactList.Add(GetContact(id.ToString(), false));
            }
            var serverPath = Server.MapPath(ContactHubConstants.DataPathConstants.TempFilePath);
            var result = await CreateFile(FileType, contactList, serverPath);
            return Json(new { filename = (result.IsFileCreated) ? $"{ContactHubConstants.DataPathConstants.DownloadFileMethod}/{result.FileName}" : null }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SyncContacts(SyncContacts model)
        {
            var contacts = new List<ContactDetails>();
            foreach (var item in model.ContactList.Where(x => x.IsSelected))
            {
                contacts.Add(GetContact(item.Id, false));
            }
            if (contacts.Count > default(int))
            {
                var createFile = CreateFile((int)FileType.Pdf, contacts, Server.MapPath(ContactHubConstants.DataPathConstants.TempFilePath)).Result;
                switch (createFile.IsFileCreated)
                {
                    case true:
                        var attachmentFilePath = Path.Combine(Server.MapPath(ContactHubConstants.DataPathConstants.TempFilePath), createFile.FileName);
                        var HasMailSent = await Utility.SynchronizeContacts(new[] { "garima.solanki8@gmail.com" }, Server.MapPath(ContactHubConstants.DataPathConstants.MailingCredential), attachmentFilePath);
                        var HasFileDeleted = await Utility.DeleteFile(attachmentFilePath);
                        break;
                    case false: break;
                    default: break;
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetDialCodes()
        {
            return Json(GetContryDialCodes(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDeactivationReason()
        {
            var serverPath = Server.MapPath(ContactHubConstants.DataPathConstants.DeactivateAccounts);
            return Json(Utility.GetReasonForDeactivateAccount(serverPath), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdatePassword(UserSettingViewModel model)
        {
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeUserInformation(UserSettingViewModel model)
        {
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeactivateUserAccount(UserSettingViewModel model)
        {
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #region Private Methods
        private IEnumerable<ContactDetails> GetContactList(bool isEditable)
        {
            var contactList = new List<ContactDetails>();
            var contactsPath = Server.MapPath(ContactHubConstants.DataPathConstants.ContactListPath);
            foreach (var item in Utility.GetContactData(contactsPath))
            {
                contactList.Add(new ContactDetails()
                {
                    Id = item.id,
                    FirstName = item.firstname,
                    MiddleName = item.middlename,
                    LastName = item.lastname,
                    EmailAddress = item.emailAddress,
                    Dob = item.dob,
                    Gender = item.sex,
                    Phone = item.phone,
                    IsEditable = isEditable
                });
            }
            return contactList;
        }
        private ContactDetails GetContact(string contactId, bool isEditable)
        {
            var ContactList = GetContactList(isEditable);
            var Contact = ContactList.FirstOrDefault(x => x.Id.Equals(contactId));
            return Contact;
        }
        private IEnumerable<SelectListItem> GetContryDialCodes()
        {
            var path = Server.MapPath(ContactHubConstants.DataPathConstants.CountryFileJsonPath);
            return Utility.GetContryDialCode(path).Result;
        }
        private async Task<DownloadFileViewModel> CreateFile(int FileType,List<ContactDetails> ContactList,string FilePath)
        {
            var file = Guid.NewGuid().ToString() + $".{Enum.GetName(typeof(FileType), FileType).ToLower().ToString()}";
            var filePath = Path.Combine(FilePath, file);
            var isFileCreated = await Utility.CreateFile(filePath, ContactList, FileType);
            return await Task.FromResult(
                new DownloadFileViewModel() {
                    FileName = file,
                    IsFileCreated = isFileCreated
            });
        }
        #endregion
    }
}