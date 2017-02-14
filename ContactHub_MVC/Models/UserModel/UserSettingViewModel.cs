using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ContactHub_MVC.CommonData.Constants;

namespace ContactHub_MVC.Models.UserModel
{
    public class UserSettingViewModel
    {
        public ChangePassword ChangeUserPassword { get; set; }
        public ChangeInformation ChangeUserInformation { get; set; }
        public DeactivateAccount DeactivateUserAccount { get; set; }
    }
    public class ChangePassword
    {
        [Display(ResourceType =typeof(MessageResource),Name ="OldPassword")]
        [Required(ErrorMessageResourceType =typeof(MessageResource),ErrorMessageResourceName = "RequiredFeilds")]
        public string OldPassword { get; set; }

        [Display(ResourceType = typeof(MessageResource), Name = "NewPassword")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string NewPassword { get; set; }

        [Display(ResourceType = typeof(MessageResource), Name = "ConfirmPassword")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessageResourceType =typeof(MessageResource),ErrorMessageResourceName = "PasswordNotMatch")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangeInformation
    {
        [Display(ResourceType = typeof(MessageResource), Name = "UserName")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string Username { get; set; }

        [Display(ResourceType = typeof(MessageResource), Name = "EmailAddress")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        [RegularExpression(ContactHubConstants.RegularExpressionConstants.EmailRegularexpression,ErrorMessageResourceType =typeof(MessageResource),ErrorMessageResourceName = "InvalidEmail")]
        public string EmailAddress { get; set; }
    }
    public partial class DeactivateAccount
    {
        [Display(ResourceType = typeof(MessageResource), Name = "Reason")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public int Reason { get; set; }

        [Display(ResourceType = typeof(MessageResource), Name = "OtherReason")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "RequiredFeilds")]
        public string Others { get; set; }
    }
    public partial class DeactivateAccount
    {
        public IEnumerable<SelectListItem> ReasonList { get; set; }
    }
}
