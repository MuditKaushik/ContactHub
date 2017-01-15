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
            var contactList = GetContactList();
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
            return View();
        }

        [HttpGet]
        public ActionResult EditContacts()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SyncContacts()
        {
            return View();
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
            var contactList = GetContactList().Skip(skipContactCount).Take(10);
            return PartialView("Partial/_Contacts",contactList);
        }

        [HttpGet]
        public ActionResult RemoveContact(string Id)
        {
            var ContactList = GetContactList().Take(10);
            var NewContactList = ContactList.Where(x => x.Id != Id);
            var renderView = Converter.PartialViewToHtml(this, "Partial/_Contacts", NewContactList);
            return Json(new { result = true, newList = renderView }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetContactById(string Id)
        {
            var ContactList = GetContactList();

            var Contact = ContactList.FirstOrDefault(x => x.Id.Equals(Id));
            return PartialView("Partial/_EditContact", Contact);
        }

        private IEnumerable<ContactDetails> GetContactList()
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
                    Phone = item.phone
                });
            }
            return contactList;
        }

        private ContactDetails GetContact(string contactId)
        {
            var ContactList = GetContactList();
            var Contact = ContactList.FirstOrDefault(x => x.Id.Equals(contactId));
            return Contact;
        }

    }
}