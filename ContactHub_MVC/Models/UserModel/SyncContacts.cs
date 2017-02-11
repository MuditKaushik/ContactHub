using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContactHub_MVC.Models.UserModel
{
    public partial class SyncContacts
    {
        [Display(Name = "PhoneNumber",ResourceType =typeof(MessageResource))]
        [Required(ErrorMessageResourceType =typeof(MessageResource),ErrorMessageResourceName ="RequiredFeilds")]
        [StringLength(maximumLength:10,ErrorMessageResourceType =typeof(MessageResource),ErrorMessageResourceName ="PhoneNumberLength",MinimumLength = 10)]
        [RegularExpression("^[0-9]*$",ErrorMessageResourceType =typeof(MessageResource),ErrorMessageResourceName ="NumericValue")]
        public string PhoneNumber { get; set; }
        [Display(Name = "ContactMode", ResourceType = typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string ContactMode { get; set; }
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string DialCode { get; set; }
    }
    public partial class SyncContacts
    {
        public IList<ContactDetails> ContactList { get; set; }
        public IEnumerable<SelectListItem> ContactModeList { get; set; }
        public IEnumerable<SelectListItem> DialCodeList { get; set; }
        public IList<int> ContactIds { get; set; }
    }
}
