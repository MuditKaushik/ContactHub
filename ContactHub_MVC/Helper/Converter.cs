using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactHub_MVC.Helper
{
    public static class Converter
    {
        public static string PartialViewToHtml(Controller controller, string partialView, object model)
        {
            controller.ViewData.Model = model;
            using (var viewString = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, partialView);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, viewString);
                viewResult.View.Render(viewContext, viewString);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                var renderedView = viewString.ToString();
                return renderedView;
            }
        }
    }
}