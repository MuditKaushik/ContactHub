using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactHub_MVC.Models.AccountModel
{
    public class SignupViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public CountryList CountryList { get; set; }
    }
    public class CountryList
    {
        public ICollection<SelectListItem> Countries { get; set; }
    }
}