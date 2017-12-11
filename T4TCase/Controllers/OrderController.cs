using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T4TCase.ViewModel;
using T4TCase.Model;
using T4TCase.Data;
using Microsoft.AspNetCore.Identity;
using T4TCase.Method;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T4TCase.Controllers
{
    public class OrderController : Controller
    {
        private readonly DatabaseContext _context;
       // private readonly UserManager<User> _userManager;

        public OrderController(DatabaseContext context)
        {
            _context = context;
           // _userManager = userManager;
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
                var user =_context.Customer.First(x => x.UserName == User.Identity.Name);                
                Ordervm = new OrderViewModel { itemvms = ItemList, customer = user };
            }
            else
            {
                Ordervm = new OrderViewModel { itemvms = ItemList };
            }

            return View(Ordervm);
        }

        [HttpPost]
        public ActionResult Confirm(OrderViewModel Ordervm)
        {
            int OrderItemAmount = 0;
            foreach (var item in Ordervm.itemvms)
            {
                OrderItemAmount += item.Aantal;
            }
            if (OrderItemAmount <= 0)
            {
                return RedirectToAction("Order", "Order");
          
            ModelState.AddModelError("", "Wrong user information.");
            }
            var customer = new Customer();
            if (User.Identity.IsAuthenticated)
            {
                customer = _context.Customer.First(x => x.UserName == User.Identity.Name);
                Functions.CompareCustomer(_context, customer, Ordervm.customer);
            }
            else
            {
                customer = Ordervm.customer;
            }

            decimal TotalPrice = 0;
            foreach (var item in Ordervm.itemvms)
            {
                if (item.Aantal > 0)
                {
                    decimal Price = item.Price;
                    Price = Price * item.Aantal;
                    TotalPrice += Price;
                }
            }

            var order = new Order { Customer = customer, Date = System.DateTime.Now, Description = Ordervm.description, TotalPrice = TotalPrice};
            _context.Order.Add(order);
            _context.SaveChanges();

            foreach (var item in Ordervm.itemvms)
            {
                if (item.Aantal > 0)
                {
                    var test = order.OrderID;
                    _context.OrderItem.Add(new OrderItem { OrderID = order.OrderID, ItemID = item.ItemID, Amount = item.Aantal });
                }
            }
            _context.SaveChanges();

            
            return View(Ordervm);
        }

    }
}
