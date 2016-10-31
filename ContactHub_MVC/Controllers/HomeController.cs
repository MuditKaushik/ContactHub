using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ContactHub_MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var xmlDocument = XDocument.Load("~/CommomData/countries.xml");
            var xmlElements = xmlDocument.Elements();
            return View();
        }
    }
}