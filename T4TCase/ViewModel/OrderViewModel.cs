using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T4TCase.Model;

namespace T4TCase.ViewModel
{
    public class OrderViewModel
    {
        public List<ItemViewModel> Itemvms { get; set; }
        public Customer Customer { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }        
    }
}
