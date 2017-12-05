using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace T4TCase.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string Logon { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        public int CustomerID { get; set; }


        public Customer Customer { get; set; }
    }
}
