using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace T4TCase.Model
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public int Age { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string PhoneNumer { get; set; }
        [Required]
        public string City { get; set; }


        public User user { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
