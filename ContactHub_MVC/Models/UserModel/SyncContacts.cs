using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ContactHub_MVC.CommonData.Constants;

namespace ContactHub_MVC.Models.UserModel
{
    public partial class SyncContacts
    {
        [Display(Name = "PhoneNumber",ResourceType =typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource),ErrorMessageResourceName = "RequiredFeilds")]
        [StringLength(maximumLength: 10, ErrorMessageResourceName = "PhoneNumberLength", ErrorMessageResourceType = typeof(MessageResource), MinimumLength = 10)]
        [RegularExpression(ContactHubConstants.RegularExpressionConstants.NumericNumberOnly,ErrorMessageResourceType =typeof(MessageResource),ErrorMessageResourceName = "NumericNumber")]
        public string PhoneNumber { get; set; }
        [Display(Name = "ContactMode", ResourceType = typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string ContactMode { get; set; }
        [Display(Name = "ContactMode", ResourceType = typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string DialCode { get; set; }
    }
    public partial class SyncContacts
    {
        public IList<ContactDetails> ContactList { get; set; }
        public IEnumerable<SelectListItem> ContactModeList { get; set; }
        public IEnumerable<SelectListItem> DialCodeList { get; set; }
        public IEnumerable<int> ContactIds { get; set; }
    }
}
