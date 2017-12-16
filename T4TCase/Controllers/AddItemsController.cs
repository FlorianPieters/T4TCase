using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T4TCase.Data;
using T4TCase.Model;
using T4TCase.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T4TCase.Controllers
{
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
            var items = _context.Item.ToList();
       

            AddItemViewModel vm = new AddItemViewModel { ExistingItems = items };
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddItems(AddItemViewModel vm, string edit, string delete, string add)
        {
            if (!string.IsNullOrEmpty(edit))
            {
                int itemID = Convert.ToInt32(edit);
                var item = _context.Item.First(x => x.ItemID == itemID);
                var itemVM = vm.ExistingItems.First(x=>x.ItemID == itemID);
                item.Name = itemVM.Name;
                item.Description = itemVM.Description;
                item.Price = itemVM.Price;
                _context.SaveChanges();
            }
            else if (!string.IsNullOrEmpty(delete))
            {
                var item = vm.ExistingItems.First(x => x.ItemID == Convert.ToInt32(delete));
                _context.Item.Remove(item);

                vm.ExistingItems.Remove(item);
                _context.SaveChanges();
            }
            else if (!string.IsNullOrEmpty(add))
            {
                _context.Item.Add(vm.CreateItem);
                _context.SaveChanges();
                var item =_context.Item.First(x=>x.Name == vm.CreateItem.Name);
                vm.ExistingItems.Add(item);
            }
           
            return View(vm);
           
        }
    }
}
