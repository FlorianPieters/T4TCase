using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T4TCase.Model;

namespace T4TCase.ViewModel
{
    public class ItemViewModel
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Aantal { get; set; }
    }
}
