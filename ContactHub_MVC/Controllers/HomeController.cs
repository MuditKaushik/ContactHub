using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ContactHub_MVC.Helper;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using ContactHub_MVC.Models.AccountModel;
using ContactHub_MVC.CommonData.Constants;

namespace ContactHub_MVC.Controllers
{
    [OutputCache(NoStore = true,Duration = 1)]
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToActionPermanent("User", "Dashboard");
            }
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
        public async Task<ActionResult> Login(SigninViewModel model)
        {
            await AuthenticateUser(model);
            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Dashboard" }));
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToActionPermanent("Login", "Home");
        }

        [HttpGet]
        public JsonResult GetCountryListXml()
        {
            var countryList = Utility.GetXmlCountryList(Server.MapPath(ContactHubConstants.DataPathConstants.CountryFileXmlPath));
            return Json(countryList, JsonRequestBehavior.AllowGet);
        }

        #region PrivateMethods
        private string IdentityProvider
        {
            get
            {
                return @"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private async Task AuthenticateUser(SigninViewModel loginDetails)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identityUser = await CreateCustomClaims(loginDetails);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identityUser);
        }
        private async Task<ClaimsIdentity> CreateCustomClaims(SigninViewModel model)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier,"1"),
                new Claim(ClaimsIdentity.DefaultNameClaimType,model.Username),
                new Claim(ClaimTypes.Authentication,"ContactHub"),
                new Claim(ClaimTypes.SerialNumber,Guid.NewGuid().ToString()),
            };
            var identityClaims = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            return await Task.FromResult(identityClaims);
        }
        #endregion
    }
}