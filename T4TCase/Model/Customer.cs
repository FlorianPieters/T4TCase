using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T4TCase.Model
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required, StringLength(50)]
        public string UserName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, Range(10, 100)]
        public int Age { get; set; }

        [Required, EmailAddress, StringLength(50)]
        public string Email { get; set; }

        [Required, Phone, StringLength(10)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(50)]
        public string Address { get; set; }

        [Required, StringLength(50)]
        public string City { get; set; }


        public User user { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
