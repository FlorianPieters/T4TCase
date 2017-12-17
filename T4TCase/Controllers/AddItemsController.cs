using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using T4TCase.Data;
using T4TCase.ViewModel;

namespace T4TCase.Controllers
{
    // only acces for Users logged in with a Admin Role
    [Authorize(Roles = "Admin")]
    public class AddItemsController : Controller
    {
        private readonly DatabaseContext _context;

        public AddItemsController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddItems()
        {
            //return list of all items from db
            var items = _context.Item.ToList();
            AddItemViewModel vm = new AddItemViewModel { ExistingItems = items };
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddItems(AddItemViewModel vm, string edit, string delete, string add)
        {
            //check which button is clicked (edit, delete or add)
            if (!string.IsNullOrEmpty(edit))
            {
                //get item and change it with data from the VM
                int itemID = Convert.ToInt32(edit);
                var item = _context.Item.First(x => x.ItemID == itemID);
                var itemVM = vm.ExistingItems.First(x => x.ItemID == itemID);
                item.Name = itemVM.Name;
                item.Description = itemVM.Description;
                item.Price = itemVM.Price;
                _context.SaveChanges();
            }

            else if (!string.IsNullOrEmpty(delete))
            {
                //delete item
                var item = vm.ExistingItems.First(x => x.ItemID == Convert.ToInt32(delete));
                _context.Item.Remove(item);

                vm.ExistingItems.Remove(item);
                _context.SaveChanges();
            }

            else if (!string.IsNullOrEmpty(add))
            {
                //add item
                _context.Item.Add(vm.CreateItem);
                _context.SaveChanges();
                var item = _context.Item.First(x => x.Name == vm.CreateItem.Name);
                vm.ExistingItems.Add(item);
            }
            return View(vm);
        }
    }
}
