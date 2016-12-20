using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactHub_MVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
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
    }
}