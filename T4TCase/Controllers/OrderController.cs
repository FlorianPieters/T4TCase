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
            var items = _context.Item.ToList();
            var itemList = new List<ItemViewModel>();

            foreach (var item in items)
            {

                ItemViewModel vm = new ItemViewModel {ItemID = item.ItemID, Name = item.Name, Description = item.Description, Price = item.Price};
                itemList.Add(vm);

            }
            var orderVM = new OrderViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var user =_context.Customer.First(x => x.UserName == User.Identity.Name);                
                orderVM = new OrderViewModel { Itemvms = itemList, Customer = user };
            }
            else
            {
                orderVM = new OrderViewModel { Itemvms = itemList };
            }

            return View(orderVM);
        }

        [HttpPost]
        public ActionResult Confirm(OrderViewModel orderVM)
        {
            int orderItemAmount = 0;
            foreach (var item in orderVM.Itemvms)
            {
                orderItemAmount += item.Aantal;
            }
            if (orderItemAmount <= 0)
            {
                return RedirectToAction("Order", "Order");
          
                ModelState.AddModelError("", "Wrong user information.");
            }
            var customer = new Customer();
            if (User.Identity.IsAuthenticated)
            {
                customer = _context.Customer.First(x => x.UserName == User.Identity.Name);
                Functions.CompareCustomer(_context, customer, orderVM.Customer);
            }
            else
            {
                customer = orderVM.Customer;
            }

            decimal totalPrice = 0;
            foreach (var item in orderVM.Itemvms)
            {
                if (item.Aantal > 0)
                {
                    decimal price = item.Price;
                    price = price * item.Aantal;
                    totalPrice += price;
                }
            }

            var order = new Order { Customer = customer, Date = System.DateTime.Now, Description = orderVM.Description, TotalPrice = totalPrice};
            _context.Order.Add(order);
            _context.SaveChanges();

            foreach (var item in orderVM.Itemvms)
            {
                if (item.Aantal > 0)
                {
                    var test = order.OrderID;
                    _context.OrderItem.Add(new OrderItem { OrderID = order.OrderID, ItemID = item.ItemID, Amount = item.Aantal });
                }
            }
            _context.SaveChanges();
           // Functions.SendMail(customer.Email, "Order Comfirmed", "We have recived your order. if you want to change it you have 10 to 15 minutes");
            
            return View(orderVM);
        }

    }
}
