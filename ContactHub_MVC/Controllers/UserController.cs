using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactHub_MVC.Models.UserModel;
using ContactHub_MVC.CommonData.Constants;
using Newtonsoft.Json;
using ContactHub_MVC.Helper;

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
                ContactModeList = ContactMode(),
                //DialCodeList = GetContryDialCodes()
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

        [HttpGet]
        public ActionResult DownloadContact(string Id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult SyncContacts(SyncContacts model)
        {
            return Json(new { result = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDialCodes()
        {
            return Json(GetContryDialCodes(), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<ContactDetails> GetContactList(bool isEditable)
        {
            var contactList = new List<ContactDetails>();
            var contactsPath = Server.MapPath(ContactHubConstants.ContactListPath);
            var contactFileRead = System.IO.File.ReadAllText(contactsPath);
            var contactJsonList = JsonConvert.DeserializeObject<dynamic>(contactFileRead);
            foreach (var item in contactJsonList)
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

        private IEnumerable<SelectListItem> ContactMode()
        {
            var contactModes = new List<SelectListItem>() {
                new SelectListItem() {
                Text="Mobile",
                Value = "1"
            },
            new SelectListItem() {
                Text = "Email",
                Value = "2"
            }
            };
            return contactModes;
        }

        private ContactDetails GetContact(string contactId, bool isEditable)
        {
            var ContactList = GetContactList(isEditable);
            var Contact = ContactList.FirstOrDefault(x => x.Id.Equals(contactId));
            return Contact;
        }

        private IEnumerable<SelectListItem> GetContryDialCodes()
        {
            var path = Server.MapPath(ContactHubConstants.CountryFileJsonPath);
            return Utility.GetContryDialCode(path).Result;
        }

    }
}