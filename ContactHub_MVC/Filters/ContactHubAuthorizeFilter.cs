using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Routing;
using System.Net;
using System.Threading;
using System.Security.Claims;

namespace ContactHub_MVC.Filters
{
    public class ContactHubAuthorizeFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var context = filterContext.HttpContext;
            var contextUser = context.User.Identity;
            var contextToken = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var HasToken = contextToken.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Authentication).Value;
            var IsAjaxRequest = filterContext.HttpContext.Request.IsAjaxRequest();
            if (contextUser == null || !contextUser.IsAuthenticated || string.IsNullOrEmpty(HasToken))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Unauthorize", action = "Index", errorCode = (int)HttpStatusCode.Unauthorized }));
            }
        }
    }
}
