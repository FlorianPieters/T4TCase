namespace T4TCase.ViewModel
{
    //VM for 1 OrderItem --> create a list of this in OrderVM/OrderListVM
    public class ItemViewModel
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Aantal { get; set; }
    }
}
