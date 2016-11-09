using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ContactHub_MVC.Models.SignupModel;

namespace ContactHub_MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var model = new SignupViewModel() {
                CountryList = GetJsonCountryList()
            };
            return View(model);
        }
        [HttpGet]
        public JsonResult GetCountryListXml()
        {
            return Json(GetXmlCountryList(), JsonRequestBehavior.AllowGet);
        }

        protected CountryList GetXmlCountryList()
        {
            var countryData = new List<SelectListItem>();
            var xmlPath = Server.MapPath(@"~/CommonData/countries.xml");
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
            var jsonPath = Server.MapPath(@"~/CommonData/contries.json");
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