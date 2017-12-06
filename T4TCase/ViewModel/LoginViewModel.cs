using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace T4TCase.ViewModel
{
    public class LoginViewModel
    {
        [Required, MaxLength(256), Display(Name = "Username")]
        public string UserName { get; set; }


        [Required, MinLength(6), MaxLength(50), Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

    }
}
