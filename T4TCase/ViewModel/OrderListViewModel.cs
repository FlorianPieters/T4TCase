using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T4TCase.ViewModel
{
    public class OrderListViewModel
    {
        public List<ItemViewModel> Itemvms { get; set; }
        
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
