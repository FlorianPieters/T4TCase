using System.ComponentModel.DataAnnotations;


namespace T4TCase.ViewModel
{
    public class RegisterViewModel
    {
        [Required, MaxLength(256), Display(Name = "Username")]
        public string UserName {get; set;}

        [Required, EmailAddress, MaxLength(256), Display(Name = "Email adress")]
        public string Email { get; set; }

        [Required, MinLength(6), MaxLength(50), Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MinLength(6), MaxLength(50), Display(Name = "Confirm Password"), DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }


        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }

        public int Age { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string PhoneNumer { get; set; }
        [Required]
        public string City { get; set; }
    }


}
