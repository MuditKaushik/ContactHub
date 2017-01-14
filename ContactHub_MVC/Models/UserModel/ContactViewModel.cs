using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactHub_MVC.Models.UserModel
{
    public class ContactViewModel
    {
        public IEnumerable<ContactDetails> ContactList { get; set; }
    }
    public class ContactDetails
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Display(Name ="EmailAddress",ResourceType =typeof(MessageResource))]
        public string EmailAddress { get; set; }
        [Display(Name = "Dob", ResourceType = typeof(MessageResource))]
        public string Dob { get; set; }
        [Display(Name = "PhoneNumber", ResourceType = typeof(MessageResource))]
        public string Phone { get; set; }
        [Display(Name = "Gender", ResourceType = typeof(MessageResource))]
        public string Gender { get; set; }
        [Display(Name = "FullName",ResourceType = typeof(MessageResource))]
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
    }
}