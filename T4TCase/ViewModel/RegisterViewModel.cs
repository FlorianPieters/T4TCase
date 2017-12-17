using System.ComponentModel.DataAnnotations;


namespace T4TCase.ViewModel
{
    //VM for RegisterView
    public class RegisterViewModel
    {
        [Required, StringLength(50), Display(Name = "Username")]
        public string UserName { get; set; }

        [Required, EmailAddress, StringLength(50), Display(Name = "Email adress")]
        public string Email { get; set; }

        [Required, MinLength(6), MaxLength(50), Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MinLength(6), MaxLength(50), Display(Name = "Confirm Password"), DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required, StringLength(50), Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required, StringLength(50), Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required, Range(10, 100), Display(Name = "Age")]
        public int Age { get; set; }

        [Required, Phone, StringLength(10), Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(50), Display(Name = "Address")]
        public string Address { get; set; }

        [Required, StringLength(50), Display(Name = "Postal code - City")]
        public string City { get; set; }
    }
}
