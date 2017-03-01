using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ContactHub_MVC.ContactHubApplicationStartup))]

namespace ContactHub_MVC
{
    public partial class ContactHubApplicationStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            ContactHubStartup(app);
        }
    }
}
