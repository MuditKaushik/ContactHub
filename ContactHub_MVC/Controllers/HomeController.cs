using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using ContactHub_MVC.Models.AccountModel;
using ContactHub_MVC.CommonData.Constants;
using ContactHub_MVC.DataAccessLayer;
using ContactHub_MVC.Helper;

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
                return RedirectToAction("User", "Dashboard");
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
            //var result = AccessAPI<SigninViewModel, SigninViewModel>.AuthenticateUser(model,"AuthorizeUser").Result;
            //if (result != null)
            //{
                var apiToken = await AccessAPI<SigninViewModel,dynamic>.GetApiToken(model, "token");
                //await AuthenticateUser(result);
                //return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Dashboard" }));
            //}
            ViewBag.LoginError ="Invalid username or password";
            return View(model);
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
            Session.Clear();
            Session.Abandon();
            return RedirectToActionPermanent("Login", "Home");
        }

        [HttpGet]
        public JsonResult GetCountryListXml()
        {
            var countryList = Utility.GetXmlCountryList(Server.MapPath(ContactHubConstants.DataPathConstants.CountryFileXmlPath));
            return Json(countryList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Download/{filename}")]
        public async Task<ActionResult> Download(string filename)
        {
            if (string.IsNullOrEmpty(filename)) { return null; }
            var folder = Server.MapPath(ContactHubConstants.DataPathConstants.TempFilePath);
            var path = Path.Combine(folder, filename);
            var fileInfo = new FileInfo(path);
            switch (fileInfo.Exists)
            {
                case true:
                    var fileBytes = Utility.FileBytes(path);
                    var fileDelete = Utility.DeleteFile(path);
                    await Task.WhenAll(fileBytes, fileDelete);
                    return File(fileBytes.Result, ContactHubConstants.FileAttributesConstants.FileContentType, Path.GetFileName(path));
                case false: await Utility.DeleteFile(path); break;
            }
            return null;
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
            var identityUser = CreateCustomClaims(loginDetails);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, await Task.WhenAll(identityUser));
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