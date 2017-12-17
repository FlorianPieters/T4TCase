using System.ComponentModel.DataAnnotations;

namespace T4TCase.ViewModel
{
    //VM for LoginView
    public class LoginViewModel
    {
        [Required, StringLength(50), Display(Name = "Username")]
        public string UserName { get; set; }

        [Required, MinLength(6), MaxLength(50), Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
