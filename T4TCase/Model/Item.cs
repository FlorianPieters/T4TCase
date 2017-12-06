using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T4TCase.Model
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
