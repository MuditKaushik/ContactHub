using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactHub_MVC.Models.SignupModel
{
    public class SignupViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Country { get; set; }
        public CountryList CountryList { get; set; }
    }
    public class CountryList
    {
        public ICollection<SelectListItem> Countries { get; set; }
    }
}