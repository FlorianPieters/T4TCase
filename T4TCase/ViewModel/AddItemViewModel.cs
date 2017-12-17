using System.Collections.Generic;
using T4TCase.Model;

namespace T4TCase.ViewModel
{
    //VM for AddItemsView
    public class AddItemViewModel
    {
        public List<Item> ExistingItems { get; set; }
        public Item CreateItem { get; set; }
    }
}
