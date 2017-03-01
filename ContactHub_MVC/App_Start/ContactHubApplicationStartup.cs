using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

namespace ContactHub_MVC
{
    public partial class ContactHubApplicationStartup
    {
        public void ContactHubStartup(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions() {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Index")
            });
        }
    }
}
