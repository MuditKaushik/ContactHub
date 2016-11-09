using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactHub_MVC.Models.SignupModel
{
    public class SignupViewModel
    {
        public string Country { get; set; }
        public CountryList CountryList { get; set; }
    }
    public class CountryList
    {
        public ICollection<SelectListItem> Countries { get; set; }
    }
}