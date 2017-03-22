using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Threading;
using System.Web.Routing;
using System.Security.Claims;
using System.Web.Mvc.Filters;

namespace ContactHub_MVC.Filters
{
    public class ContactHubAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //var IsAjaxRequest = filterContext.HttpContext.Request.IsAjaxRequest();
            //var user = filterContext.HttpContext.User;
            //switch((user != null && !user.Identity.IsAuthenticated))
            //{
            //    case true:
            //        var routeValue = new RouteValueDictionary(new { controller = "Unauthorize", action = "Index", errorcode = (int)HttpStatusCode.Unauthorized });
            //        switch (IsAjaxRequest)
            //        {
            //            case true:
            //                filterContext.Result = new RedirectToRouteResult(routeValue);
            //                break;
            //            case false:
            //                filterContext.Result = new RedirectToRouteResult(routeValue);
            //                break;
            //        }
            //        break;
            //    case false: break;
            //}
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var context = filterContext.HttpContext;
            var contextUser = context.User.Identity;
            var contextToken = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var HasToken = contextToken.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Authentication).Value;
            if (contextUser == null || !contextUser.IsAuthenticated || string.IsNullOrEmpty(HasToken))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Unauthorize", action = "Index", errorCode = (int)HttpStatusCode.Unauthorized }));
            }
        }
    }
}
