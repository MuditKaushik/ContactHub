using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ContactHub_MVC.CommonData.Constants;
using Newtonsoft.Json;

namespace ContactHub_MVC.Helper
{
    public static class Utility
    {
        public static Task<string> PartialViewToHtml(Controller controller, string partialView, object model)
        {
            controller.ViewData.Model = model;
            using (var viewString = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, partialView);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, viewString);
                viewResult.View.Render(viewContext, viewString);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                var renderedView = viewString.ToString();
                return Task.FromResult(renderedView);
            }
        }

        public static Task<FileResult> Download()
        {
            //var path = new HttpServerUtilityBase().MapPath("");
            var fileBytes = File.ReadAllBytes("");
            return null;
        }

        public static Task<IEnumerable<SelectListItem>> GetContryDialCode(string filePath)
        {
            var dialCodes = new List<SelectListItem>();
            var file = File.ReadAllText(filePath);
            var fileData = JsonConvert.DeserializeObject<dynamic>(file);
            foreach(var item in fileData)
            {
                dialCodes.Add(new SelectListItem() {
                    Text = $"{item.code} ({item.dial_code})",
                    Value = item.dial_code
                });
            }
            return Task.FromResult<IEnumerable<SelectListItem>>(dialCodes);
        }
    }
}