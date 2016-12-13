using System.Web.Mvc;
using System.Collections.Generic;
using ContactHub_MVC.CommonData.Constants;
using System.ComponentModel.DataAnnotations;

namespace ContactHub_MVC.Models.AccountModel
{
    public class SignupViewModel
    {
        [Display(Name = "FirstName",ResourceType = typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource),ErrorMessageResourceName = "RequiredFeilds")]
        public string FirstName { get; set; }

        [Display(Name = "LastName",ResourceType = typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string LastName { get; set; }

        [Display(Name = "EmailAddress",ResourceType = typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        [RegularExpression(ContactHubConstants.EmailRegularexpression,ErrorMessageResourceType = typeof(MessageResource),ErrorMessageResourceName = "InvalidEmail")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name = "UserName",ResourceType =typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string Username { get; set; }

        [Display(Name = "Password",ResourceType = typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessageResourceType = typeof(MessageResource),ErrorMessageResourceName = "PasswordNotMatch")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Country",ResourceType = typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string Country { get; set; }

        public CountryList CountryList { get; set; }
    }
    public class CountryList
    {
        public ICollection<SelectListItem> Countries { get; set; }
    }
}