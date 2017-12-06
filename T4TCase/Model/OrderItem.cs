using System.ComponentModel.DataAnnotations;

namespace T4TCase.Model
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        public int ItemID { get; set; }
        public int OrderID { get; set; }
        [Required]
        public int Amount { get; set; }

        public Item Item { get; set; }
        public Order Order { get; set; }
    }
}
