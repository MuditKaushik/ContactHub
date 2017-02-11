using System.ComponentModel.DataAnnotations;
using ContactHub_MVC.CommonData.Constants;
namespace ContactHub_MVC.Models.AccountModel
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "EmailAddress",ResourceType =typeof(MessageResource))]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings =false, ErrorMessageResourceType = typeof(MessageResource),ErrorMessageResourceName = "RequiredFeilds" )]
        [RegularExpression(ContactHubConstants.RegularExpressionConstants.EmailRegularexpression, ErrorMessageResourceType = typeof(MessageResource),ErrorMessageResourceName = "InvalidEmail")]
        public string EmailAddress { get; set; }
    }
}
