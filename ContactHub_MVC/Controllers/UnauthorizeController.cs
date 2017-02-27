using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactHub_MVC.CommonData.Constants;

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
                case (int)HttpStatusCode.NotFound: ViewBag.Message = ContactHubConstants.UnauthorizeErrorMessgae.PageNotFount; break;
                case (int)HttpStatusCode.Unauthorized: ViewBag.Message = ContactHubConstants.UnauthorizeErrorMessgae.UnauthorizeAccess; break;
                default: ViewBag.Message = ContactHubConstants.UnauthorizeErrorMessgae.InternalServer; break;
            }
            return View();
        }
    }
}