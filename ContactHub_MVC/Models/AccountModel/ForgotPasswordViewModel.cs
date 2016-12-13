﻿using System.ComponentModel.DataAnnotations;
using ContactHub_MVC.CommonData.Constants;
namespace ContactHub_MVC.Models.AccountModel
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings =false, ErrorMessageResourceType = typeof(MessageResource),ErrorMessageResourceName = "RequiredFeilds" )]
        [RegularExpression(ContactHubConstants.EmailRegularexpression, ErrorMessageResourceType = typeof(MessageResource),ErrorMessageResourceName = "InvalidEmail")]
        public string EmailAddress { get; set; }
    }
}
