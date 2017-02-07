using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactHub_MVC.Models.UserModel;
using ContactHub_MVC.CommonData.Constants;
using Newtonsoft.Json;
using ContactHub_MVC.Helper;
using System.IO;
using System.Threading.Tasks;

namespace ContactHub_MVC.Controllers
{
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
        public ActionResult AddContacts(AddContactsViewModel model)
        {
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
            var result = await CreateFile(FileType, contactList);
            return Json(new { filename = (result.IsFileCreated) ? result.FileName : null, path = ContactHubConstants.DataPathConstants.DownloadFileMethod }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SyncContacts(SyncContacts model)
        {
            var contacts = new List<ContactDetails>();
            foreach (var id in model.ContactIds)
            {
                contacts.Add(GetContact(id.ToString(), false));
            }

            return Json(new { result = contacts }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDialCodes()
        {
            return Json(GetContryDialCodes(), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Download(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return null; }
            var folder = Server.MapPath(ContactHubConstants.DataPathConstants.TempFilePath);
            var path = Path.Combine(folder, fileName);
            var fileInfo = new FileInfo(path);
            switch (fileInfo.Exists)
            {
                case true:
                    var fileBytes = await Utility.FileBytes(path);
                    await Utility.DeleteFile(path);
                    return File(fileBytes, ContactHubConstants.FileAttributesConstants.FileContentType, Path.GetFileName(path));
                case false: await Utility.DeleteFile(path); break;
            }
            return null;
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
        private async Task<DownloadFileViewModel> CreateFile(int FileType,List<ContactDetails> ContactList)
        {
            var serverPath = Server.MapPath(ContactHubConstants.DataPathConstants.TempFilePath);
            var file = Guid.NewGuid().ToString() + $".{Enum.GetName(typeof(FileType), FileType).ToLower().ToString()}";
            var filePath = Path.Combine(serverPath, file);
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