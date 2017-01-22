using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHub_MVC.CommonData.Constants
{
    public class ContactHubConstants
    {
        public const string EmailRegularexpression = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])$";
        public const string CountryFileXmlPath = @"~/CommonData/Files/countries.xml";
        public const string CountryFileJsonPath = @"~/CommonData/Files/contries.json";
        public const string ContactListPath = @"~/CommonData/Files/data.json";
    }
}
