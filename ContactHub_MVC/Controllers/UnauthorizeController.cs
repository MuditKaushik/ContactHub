using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ContactHub_MVC.Controllers
{
    public class UnauthorizeController : Controller
    {
        // GET: Unauthorize
        [Route("Index/{errorCode}")]
        public ActionResult Index(int? errorCode)
        {
            switch (errorCode)
            {
                case (int)HttpStatusCode.NotFound: return View();
                case (int)HttpStatusCode.Unauthorized:return View();
                default: return View();
            }
        }
    }
}