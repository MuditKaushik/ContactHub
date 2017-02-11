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
            bundle.Add(new ScriptBundle("~/Script/jQuery").Include(
                "~/Scripts/jQuery/jquery-{version}.js"));
            //jQuery unobstrusive js
            bundle.Add(new ScriptBundle("~/Script/jQuery-Unobtrusive").Include(
                "~/Scripts/jQuery/jquery.validate.js",
                "~/Scripts/jQuery/jquery.validate.unobtrusive.js"));
            //Bootstrap js
            bundle.Add(new ScriptBundle("~/Script/bootstrap").Include(
                "~/Scripts/bootstrap/bootstrap*"));
            //Respond js
            bundle.Add(new ScriptBundle("~/Script/respond").Include(
                "~/Scripts/respond/respond*"));
            //Modernizer js
            bundle.Add(new ScriptBundle("~/Script/modernizer").Include(
                "~/Scripts/modernizr/modernizr-*"));
            //es6-module-loader
            bundle.Add(new ScriptBundle("~/Script/es6-module-loader").Include(
                "~/Scripts/traceur/traceur.js",
                "~/Scripts/es6ModuleLoader/es6-module-loader-dev.js"));
            //jsany bootstrap js
            bundle.Add(new ScriptBundle("~/Script/jsany_Bootstrap").Include(
                "~/Scripts/JsanyBootstrap/jsany-bootstrap.js"));
            //Bootstrap date-picker
            bundle.Add(new ScriptBundle("~/Scripts/BootstrapDatePicker").Include(
                "~/Scripts/bootstrap/bootstrap-datepicker.js"));
            /*--------------------Stylesheet bundles--------------------*/
            //bootstrap css
            bundle.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap*"));
            //fontawesome
            bundle.Add(new StyleBundle("~/Content/fontawesome").Include(
                "~/Content/font-awesome.css"));
            //jsany bootstrap css
            bundle.Add(new StyleBundle("~/Content/jsany_Bootstrap").Include(
                "~/Content/jsany-bootstrap.css"));
            //custom css
            bundle.Add(new StyleBundle("~/Content/site").Include("~/Content/Site.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}