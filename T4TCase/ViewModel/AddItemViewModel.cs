using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T4TCase.Model;

namespace T4TCase.ViewModel
{
    public class AddItemViewModel
    {
        public List<Item> ExistingItems { get; set; }
        public Item CreateItem { get; set; }
    }
}
