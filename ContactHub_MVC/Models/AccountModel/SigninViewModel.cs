using System.ComponentModel.DataAnnotations;
namespace ContactHub_MVC.Models.AccountModel
{
    public class SigninViewModel
    {
        [Display(Name = "UserName",ResourceType = typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource),ErrorMessageResourceName = "RequiredFeilds")]
        public string Username { get; set; }
        [Display(Name = "Password",ResourceType = typeof(MessageResource))]
        [Required(ErrorMessageResourceType = typeof(MessageResource),ErrorMessageResourceName = "RequiredFeilds")]
        public string Password { get; set; }
    }
}
