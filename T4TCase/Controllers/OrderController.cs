using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T4TCase.ViewModel;
using T4TCase.Model;
using T4TCase.Data;
using Microsoft.AspNetCore.Identity;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T4TCase.Controllers
{
    public class OrderController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public OrderController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Order()
        {
            var Items = _context.Item.ToList();
            var ItemList = new List<ItemViewModel>();

            foreach (var Item in Items)
            {

                ItemViewModel vm = new ItemViewModel {ItemID = Item.ItemID, Name = Item.Name, Description = Item.Description, Price = Item.Price};
                ItemList.Add(vm);

            }
            var Ordervm = new OrderViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var user = _context.Customer.First(x => x.UserName == User.Identity.Name);
                Ordervm = new OrderViewModel { itemvms = ItemList, customer = user };
            }
            else
            {
                Ordervm = new OrderViewModel { itemvms = ItemList };
            }

            return View(Ordervm);
        }

        [HttpPost]
        public async Task<ActionResult> Order(OrderViewModel Ordervm)
        {
            

            return View(Ordervm);
        }
    }
}
