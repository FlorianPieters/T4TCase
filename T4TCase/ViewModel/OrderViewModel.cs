using System;
using System.Collections.Generic;
using T4TCase.Model;

namespace T4TCase.ViewModel
{
    //VM for 1 order with Customer --> gets used in OrderView/ChangeOrderView
    public class OrderViewModel
    {
        public List<ItemViewModel> Itemvms { get; set; }
        public Customer Customer { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
