using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHub_MVC.CommonData.Constants
{
    public class ContactHubConstants
    {
        /*----------------Constants Related to data-----------------------------*/
        public struct DataPathConstants
        {
            public const string CountryFileXmlPath = @"~/CommonData/Files/countries.xml";
            public const string CountryFileJsonPath = @"~/CommonData/Files/contries.json";
            public const string ContactListPath = @"~/CommonData/Files/data.json";
            public const string MailingCredential = @"~/CommonData/Files/MailingCredentials.json";
            public const string TempFilePath = @"~/TempFile";
            public const string DownloadFileMethod = @"/User/Download";
        }
        /*------------Constants related to files------------------*/
        public struct FileAttributesConstants
        {
            public const string FileContentType = @"application/octet-stream";
            public const string TextFileExtension = ".txt";
            public const string PdfFileExtension = ".pdf";
            public const string CsvFileExtension = ".csv";
        }
        /*---------------------Constants related to Regularexpression------------------*/
        public struct RegularExpressionConstants
        {
            public const string EmailRegularexpression = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])$";
            public const string NumericNumberOnly = "^[0-9]*$";
        }
    }
}
