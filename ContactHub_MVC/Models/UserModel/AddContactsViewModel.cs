using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactHub_MVC.Models.UserModel
{
    public partial class AddContactsViewModel
    {
        [Display(Name = "FirstName",ResourceType =typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string FirstName { get; set; }

        [Display(Name ="MiddleName",ResourceType =typeof(MessageResource))]
        public string MiddleName { get; set; }

        [Display(Name ="LastName",ResourceType =typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string LastName { get; set; }

        [Display(Name ="NickName",ResourceType =typeof(MessageResource))]
        public string NickName { get; set; }

        [Display(Name ="Dob",ResourceType =typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string Dob { get; set; }

        [Display(Name ="EmailAddress",ResourceType =typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string EmailAddress { get; set; }

        [Display(Name ="PhoneNumber",ResourceType =typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string Phone { get; set; }
    }
    public partial class AddContactsViewModel
    {
        public IEnumerable<ContactDetails> Contacts { get; set; }
    }
}