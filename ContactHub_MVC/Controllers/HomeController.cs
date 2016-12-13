using System.Web.Mvc;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using ContactHub_MVC.CommonData.Constants;
using ContactHub_MVC.Models.AccountModel;

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
            return View();
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetCountryListXml()
        {
            return Json(GetXmlCountryList(), JsonRequestBehavior.AllowGet);
        }

        protected CountryList GetXmlCountryList()
        {
            var countryData = new List<SelectListItem>();
            var xmlPath = Server.MapPath(ContactHubConstants.CountryFilePath);
            var xmlDocument = XDocument.Load(xmlPath);
            var elements = xmlDocument.Element("countries").Elements("country");
            foreach (var item in elements)
            {
                countryData.Add(new SelectListItem()
                {
                    Text = $"{item.Value}({item.Attribute("code").Value})",
                    Value = item.Value
                });
            }
            return new CountryList() { Countries = countryData};
        }
        protected CountryList GetJsonCountryList()
        {
            var countryData = new List<SelectListItem>();
            var jsonPath = Server.MapPath(ContactHubConstants.CountryFilePath);
            var jsonFile = System.IO.File.ReadAllText(jsonPath); 
            var jsonDocument = JsonConvert.DeserializeObject<dynamic>(jsonFile);
            foreach (var item in jsonDocument)
            {
                countryData.Add(new SelectListItem()
                {
                    Text = $"{item.name}({item.code})",
                    Value = item.name
                });
            }
            return new CountryList() { Countries = countryData};
        }
    }
}