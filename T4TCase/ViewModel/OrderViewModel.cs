using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T4TCase.Model;

namespace T4TCase.ViewModel
{
    public class OrderViewModel
    {
        public List<ItemViewModel> itemvms { get; set; }
        public Customer customer { get; set; }
        public string description { get; set; }
    }
}
