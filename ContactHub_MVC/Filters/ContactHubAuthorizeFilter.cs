using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Routing;
using System.Net;

namespace ContactHub_MVC.Filters
{
    public class ContactHubAuthorizeFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var context = filterContext.HttpContext;
            var contextUser = context.User.Identity;
            var IsAjaxRequest = filterContext.HttpContext.Request.IsAjaxRequest();
            if (contextUser == null || !contextUser.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Unauthorize", action = "Index", errorCode = (int)HttpStatusCode.Unauthorized }));
            }
        }
    }
}
