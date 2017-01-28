using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ContactHub_MVC.CommonData.Constants;
using Newtonsoft.Json;
using System.Diagnostics;
using ContactHub_MVC.Models.UserModel;
using iTextSharp.text.pdf;
using iTextSharp.text;

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

        public async static Task<bool> CreateFile(string filePath, List<ContactDetails> Contacts, FileType fileType)
        {
            var IsFileCreated = default(bool);
        createNew: var fileInfo = new FileInfo(filePath);
            switch (fileInfo.Exists)
            {
                case true:
                    goto default;
                case false:
                    File.Create(filePath).Close();
                    switch (fileType)
                    {
                        case FileType.Text: IsFileCreated = await CreateTextFile(filePath, Contacts); break;
                        case FileType.Pdf: IsFileCreated = await CreatePdfFile(filePath, Contacts); break;
                        case FileType.Contact: IsFileCreated = await CreateCsvFile(filePath, Contacts); break;
                        default: break;
                    }
                    break;
                default: await DeleteFile(filePath); goto createNew;
            }
            return await Task.FromResult(IsFileCreated);
        }

        private async static Task<bool> CreateTextFile(string filePath, List<ContactDetails> Contacts)
        {
            var isFileCreated = default(bool);
            using (var fStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (var sWriter = new StreamWriter(fStream))
                {
                    sWriter.WriteLine("\tName\tGender\tDate of birth\tPhone\tEmail");
                    for (var i = 0; i < Contacts.Count(); i++)
                    {
                        sWriter.WriteLine($"{i + 1}\t{Contacts[i].FullName}\t{Contacts[i].Gender}\t{Contacts[i].Dob}\t{Contacts[i].Phone}\t{Contacts[i].EmailAddress}");
                    }
                    isFileCreated = true;
                }
            }
            return await Task.FromResult(isFileCreated);
        }

        private async static Task<bool> CreatePdfFile(string filePath, List<ContactDetails> Contacts)
        {
            var isFileCreated = default(bool);
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                var pdfDoc = new Document(PageSize.A4);
                var tableWidth = new int[] {3,10,5,7,6,10 };
                var pdfTable = new PdfPTable(6)
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    WidthPercentage = 100,
                };

                pdfTable.SetWidths(tableWidth);
                pdfTable.AddCell("SNo.");
                pdfTable.AddCell("Name");
                pdfTable.AddCell("Gender");
                pdfTable.AddCell("Date of birth");
                pdfTable.AddCell("Phone");
                pdfTable.AddCell("Email");

                for(var i=0;i<Contacts.Count;i++)
                {
                    pdfTable.AddCell((i+1).ToString());
                    pdfTable.AddCell(Contacts[i].FullName);
                    pdfTable.AddCell(Contacts[i].Gender);
                    pdfTable.AddCell(Contacts[i].Dob);
                    pdfTable.AddCell(Contacts[i].Phone);
                    pdfTable.AddCell(Contacts[i].EmailAddress);
                }

                var pdfWriter = PdfWriter.GetInstance(pdfDoc, fileStream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                isFileCreated = true;
            }
            return await Task.FromResult(isFileCreated);
        }

        private async static Task<bool> CreateCsvFile(string filePath, List<ContactDetails> Contacts)
        {
            var isFileCreated = default(bool);
            return await Task.FromResult(isFileCreated);
        }

        public async static Task<bool> DeleteFile(string filePath)
        {
            var fileDeleted = default(bool);
            var fileInfo = new FileInfo(filePath);
            switch (fileInfo.Exists)
            {
                case true: File.Delete(filePath); fileDeleted = true; break;
                case false: break;
                default: break;
            }
            return await Task.FromResult(fileDeleted);
        }

        public async static Task<byte[]> FileBytes(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            switch (fileInfo.Exists)
            {
                case true: return await Task.FromResult(File.ReadAllBytes(filePath));
                case false: return await Task.FromResult(default(byte[]));
                default: return await Task.FromResult(default(byte[]));
            }
        }

        public static Task<IEnumerable<SelectListItem>> GetContryDialCode(string filePath)
        {
            var dialCodes = new List<SelectListItem>();
            var file = File.ReadAllText(filePath);
            var fileData = JsonConvert.DeserializeObject<dynamic>(file);
            foreach (var item in fileData)
            {
                dialCodes.Add(new SelectListItem()
                {
                    Text = $"{item.code} ({item.dial_code})",
                    Value = item.dial_code
                });
            }
            return Task.FromResult<IEnumerable<SelectListItem>>(dialCodes);
        }
    }
}