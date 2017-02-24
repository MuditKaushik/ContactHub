using System;
using System.IO;
using System.Net;
using System.Web;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.Mvc;
using Newtonsoft.Json;
using iTextSharp.text;
using System.Net.Mail;
using iTextSharp.text.pdf;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf.parser;
using System.Collections.Generic;
using ContactHub_MVC.Models.UserModel;
using ContactHub_MVC.CommonData.Constants;
using ContactHub_MVC.Models.MailingModel;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;

namespace ContactHub_MVC.Helper
{
    public static class Utility
    {
        public async static Task<string> PartialViewToHtml(Controller controller, string partialView, object model)
        {
            controller.ViewData.Model = model;
            using (var viewString = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, partialView);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, viewString);
                viewResult.View.Render(viewContext, viewString);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                var renderedView = viewString.ToString();
                return await Task.FromResult(renderedView);
            }
        }

        public async static Task<bool> CreateFile(string filePath, List<ContactDetails> Contacts, int fileType)
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
                        case (int)FileType.Txt: IsFileCreated = await CreateTextFile(filePath, Contacts); break;
                        case (int)FileType.Pdf: IsFileCreated = await CreatePdfFile(filePath, Contacts); break;
                        case (int)FileType.Csv: IsFileCreated = await CreateCsvFile(filePath, Contacts); break;
                        default: break;
                    }
                    break;
                default: await DeleteFile(filePath); goto createNew;
            }
            return await Task.FromResult(IsFileCreated);
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

        public static IEnumerable<SelectListItem> ContactMode()
        {
            var contactModes = new List<SelectListItem>() {
                new SelectListItem() {
                Text="Mobile",
                Value = "1"
            },
            new SelectListItem() {
                Text = "Email",
                Value = "2"
            }
            };
            return contactModes;
        }

        public static dynamic GetContactData(string filePath)
        {
            var contactFileRead = System.IO.File.ReadAllText(filePath);
            var contactJsonList = JsonConvert.DeserializeObject<dynamic>(contactFileRead);
            return contactJsonList;
        }

        public static dynamic GetReasonForDeactivateAccount(string filePath)
        {
            var deactiveAccountFile = File.ReadAllText(filePath);
            var reasontypes = new JavaScriptSerializer().Deserialize<dynamic>(deactiveAccountFile);
            return reasontypes;
        }

        public static async Task<MailingFailureViewModel> SynchronizeContacts(IEnumerable<string> ReceiverMails,string CredentialFilePath,string AttachmentFilePath)
        {
            var mailingStatus = new MailingFailureViewModel();
            foreach(var mail in ReceiverMails)
            {
                mailingStatus.IsMailSent = await SendEmail(mail, CredentialFilePath,AttachmentFilePath);
                if (!mailingStatus.IsMailSent)
                {
                    mailingStatus.EmailFailures.Add(mail);
                }
            }
            return await Task.FromResult(mailingStatus);
        } 

        public static async Task<IEnumerable<HttpPostedFileBase>> GetFilesToUpload(string FileNames,IEnumerable<HttpPostedFileBase> Files)
        {
            if (string.IsNullOrEmpty(FileNames)) { return await Task.FromResult(Enumerable.Empty<HttpPostedFileBase>()); }
            var UploadFiles = new List<HttpPostedFileBase>();
            var Names = FileNames.Split(',').Select(x => x.Trim()).ToArray();
            foreach(var name in Names)
            {
                UploadFiles.Add(Files.DistinctBy(x=>x.FileName).FirstOrDefault(x=>x.FileName.Equals(name)));
            }
            return await Task.FromResult(UploadFiles);
        }

        #region PrivateMethods
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
                var tableWidth = new int[] { 3, 10, 5, 7, 6, 10 };

                var paragraph = new Paragraph("ContactHub", FontFactory.GetFont("Times New Roman", 16))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                var pdfCell = new PdfPCell(paragraph);
                var paragraph2 = new Paragraph($"Username - Contact details", FontFactory.GetFont("Times New Roman", 14))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                var pdfSubCell = new PdfPCell(paragraph2);

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

                for (var i = 0; i < Contacts.Count; i++)
                {
                    pdfTable.AddCell((i + 1).ToString());
                    pdfTable.AddCell(Contacts[i].FullName);
                    pdfTable.AddCell(Contacts[i].Gender);
                    pdfTable.AddCell(Contacts[i].Dob);
                    pdfTable.AddCell(Contacts[i].Phone);
                    pdfTable.AddCell(Contacts[i].EmailAddress);
                }

                var pdfWriter = PdfWriter.GetInstance(pdfDoc, fileStream);
                pdfDoc.Open();
                pdfDoc.Add(paragraph);
                pdfDoc.Add(paragraph2);
                pdfDoc.Add(Chunk.NEWLINE);
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                isFileCreated = true;
            }
            return await Task.FromResult(isFileCreated);
        }
        private async static Task<bool> CreateCsvFile(string filePath, List<ContactDetails> Contacts)
        {
            var isFileCreated = default(bool);
            var HeaderValues = new[] { "Name", "Gender", "DOB", "Phone", "Email" };
            using (var fileStream = new FileStream(filePath,FileMode.OpenOrCreate,FileAccess.ReadWrite))
            {
                using (var csvWriter = new StreamWriter(fileStream))
                {
                    isFileCreated = true;
                }
            }
            return await Task.FromResult(isFileCreated);
        }
        private async static Task<StringBuilder> ReadPdfFile(string filePath)
        {
            var pdfText = new StringBuilder();
            using (var pdfReader = new PdfReader(filePath))
            {
                for (int page = 1; page < pdfReader.NumberOfPages; page++)
                {
                    try
                    {
                        ITextExtractionStrategy pdfExtractor = new SimpleTextExtractionStrategy();
                        var text = PdfTextExtractor.GetTextFromPage(pdfReader, page, pdfExtractor);
                        pdfText.Append(text);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            return await Task.FromResult(pdfText);
        }
        private async static Task<bool> SendEmail(string SendTo, string FilePath,string AttachmentPath)
        {
            var isMailSent = default(bool);
            using (var smtp = new SmtpClient())
            {
                var credential = await GetMailingCredential(FilePath);
                var Mail = new MailMessage(credential.From,SendTo,credential.Subject,credential.Body);
                if (!string.IsNullOrEmpty(AttachmentPath))
                {
                    var Attachment = new Attachment(AttachmentPath);
                    Mail.Attachments.Add(Attachment);
                }
                var mailingCredentials = new NetworkCredential() {
                    UserName = credential.Username,
                    Password = credential.Password
                };
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Host = credential.SmtpHost;
                smtp.EnableSsl = credential.SmtpEnableSsl;
                smtp.Port = credential.SmtpPort;
                try {
                    smtp.Send(Mail);
                    isMailSent = true;
                }
                catch (Exception ex) {
                    isMailSent = false;
                }
            }
            return await Task.FromResult(isMailSent);
        }
        private async static Task<EmailCredentialViewModel> GetMailingCredential(string FilePath)
        {
            var mailingCredential = new EmailCredentialViewModel();
            var readData = File.ReadAllText(FilePath);
            var credentialData = JsonConvert.DeserializeObject<dynamic>(readData);
            foreach(var item in credentialData)
            {
                mailingCredential.Username = item.Username;
                mailingCredential.Password = item.Password;
                mailingCredential.SmtpHost = item.Smtp_HostOutlook;
                mailingCredential.SmtpPort = item.Smtp_Port;
                mailingCredential.SmtpEnableSsl = item.Smtp_EnableSsl;
                mailingCredential.From = item.From;
                mailingCredential.Subject = item.Subject;
                mailingCredential.Body = item.Body;
            }
            return await Task.FromResult(mailingCredential);
        }
        #endregion
    }
}