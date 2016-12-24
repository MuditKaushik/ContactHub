using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContactHub_MVC.Models.UserModel
{
    public class SearchContactsViewModel
    {
        [Display(Name ="EmailAddress",ResourceType =typeof(MessageResource))]
        public string EmailAddress { get; set; }
        [Display(Name ="NickName",ResourceType =typeof(MessageResource))]
        public string NickName { get; set; }
        [Display(Name ="Dob",ResourceType =typeof(MessageResource))]
        public string DOB { get; set; }
        [Display(Name ="PhoneNumber",ResourceType =typeof(MessageResource))]
        public string Phone { get; set; }
    }
}