using System.Web.Mvc;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using ContactHub_MVC.Models.AccountModel;
using ContactHub_MVC.CommonData.Constants;
using System.Security.Claims;
using ContactHub_MVC.Helper;

namespace ContactHub_MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(SignupViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(SigninViewModel model)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("UserName",model.Username));
            claims.Add(new Claim("UserId", "1"));
            return View();
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            return RedirectToActionPermanent("Login", "Home");
        }

        [HttpGet]
        public JsonResult GetCountryListXml()
        {
            var countryList = Utility.GetXmlCountryList(Server.MapPath(ContactHubConstants.DataPathConstants.CountryFileXmlPath));
            return Json(countryList, JsonRequestBehavior.AllowGet);
        }
    }
}