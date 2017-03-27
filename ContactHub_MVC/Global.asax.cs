using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.Routing;
using System.Security.Claims;
using System.Web.Optimization;
using System.Net;

namespace ContactHub_MVC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var raisedException = Server.GetLastError();
            var exception = raisedException as HttpException;
            Response.Clear();
            Server.ClearError();
            Response.RedirectToRoutePermanent(new RouteValueDictionary(new { controller = "Unauthorize", action = "Index", errorcode = (exception != null) ? exception.GetHttpCode() : (int)HttpStatusCode.InternalServerError }));
        }
    }
}
