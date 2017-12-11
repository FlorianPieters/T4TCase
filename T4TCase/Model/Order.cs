using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T4TCase.Model
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
