using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ContactHub_MVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundle)
        {
            //jQuery and jQuery-Validaion
            bundle.Add(new ScriptBundle("/Script/jQuery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*"
                ));
            //jQuery unobstrusive js
            bundle.Add(new ScriptBundle("/Script/jQuery-Unobtrusive").Include(
                "~/Scripts/jquery.validate.unobtrusive*"));
            //Bootstrap js
            bundle.Add(new ScriptBundle("/Script/bootstrap").Include(
                "~/Scripts/bootstrap*"));
            //Respond js
            bundle.Add(new ScriptBundle("/Script/respond").Include(
                "~/Scripts/respond*"));
            //Modernizer js
            bundle.Add(new ScriptBundle("/Script/modernizer").Include(
                "~/Scripts/modernizr-{version}.js"));
            //custom js
            bundle.Add(new ScriptBundle("/Script/").Include());

            /*--------------------Stylesheet bundles--------------------*/
            //bootstrap css
            bundle.Add(new StyleBundle("/Script/bootstrap").Include(
                "~/Content/bootstrap*"));
            //custom css
            bundle.Add(new StyleBundle("/Script/site").Include("~/Content/Site.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}