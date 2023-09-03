using System.ComponentModel.DataAnnotations;

namespace my_Ecommerce_App.ViewModels
{
    public class LoginUser
    {


        [Required]
        public string? UserNameOrEmail { get; set; }

        [Required]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}