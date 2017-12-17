using System;
using System.Collections.Generic;

namespace T4TCase.ViewModel
{
    //VM for OrderHistoryView --> list of Orders
    public class OrderListViewModel
    {
        public List<ItemViewModel> Itemvms { get; set; }

        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
