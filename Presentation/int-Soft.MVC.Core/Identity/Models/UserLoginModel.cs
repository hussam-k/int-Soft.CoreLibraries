using System.ComponentModel.DataAnnotations;
using intSoft.Res.DisplayNames;

namespace intSoft.MVC.Core.Identity.Models
{
    public class UserLoginModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username", ResourceType = typeof(DisplayNames))]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(DisplayNames))]
        public string Password { get; set; }

    }
}
