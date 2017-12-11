using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T4TCase.ViewModel
{
    public class OrderListViewModel
    {
        public List<ItemViewModel> itemvms { get; set; }
        
        public string description { get; set; }
        public DateTime date { get; set; }
        public decimal totalprice { get; set; }
    }
}
