using System.ComponentModel.DataAnnotations;
using intSoft.Res.DisplayNames;
using intSoft.Res.Messages;

namespace intSoft.MVC.Core.Identity.Models
{
    public class UserRegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(DisplayNames))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(Messages), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(DisplayNames))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(DisplayNames))]
        [Compare("Password", ErrorMessageResourceName = "PasswordCompare", ErrorMessageResourceType = typeof(Messages))]
        public string ConfirmPassword { get; set; }
    }
}