using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using T4TCase.Data;
using T4TCase.Method;
using T4TCase.Model;
using T4TCase.ViewModel;

namespace T4TCase.Controllers
{
    public class OrderController : Controller
    {
        private readonly DatabaseContext _context;

        public OrderController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Order()
        {
            //create OrderViewModel with all items and Customer if logged in
            var items = _context.Item.ToList();
            var itemList = new List<ItemViewModel>();

            foreach (var item in items)
            {
                ItemViewModel vm = new ItemViewModel { ItemID = item.ItemID, Name = item.Name, Description = item.Description, Price = item.Price };
                itemList.Add(vm);
            }
            var orderVM = new OrderViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var user = _context.Customer.First(x => x.UserName == User.Identity.Name);
                orderVM = new OrderViewModel { Itemvms = itemList, Customer = user };
            }
            else orderVM = new OrderViewModel { Itemvms = itemList };

            return View(orderVM);
        }

        [HttpPost]
        public ActionResult Confirm(OrderViewModel orderVM)
        {
            //check if the Customer orderd something
            int orderItemAmount = 0;
            foreach (var item in orderVM.Itemvms)
            {
                orderItemAmount += item.Aantal;
            }
            if (orderItemAmount <= 0) return RedirectToAction("Order", "Order");

            //compare Customer if logged in and compare
            var customer = new Customer();
            if (User.Identity.IsAuthenticated)
            {
                customer = _context.Customer.First(x => x.UserName == User.Identity.Name);
                Functions.CompareCustomer(_context, customer, orderVM.Customer);
            }
            else customer = orderVM.Customer;

            //count price so you can add the Order in the db
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

            //add Order to db
            var order = new Order { Customer = customer, Date = System.DateTime.Now, Description = orderVM.Description, TotalPrice = totalPrice };
            _context.Order.Add(order);
            _context.SaveChanges();

            //add OrderItems with info from the Order
            foreach (var item in orderVM.Itemvms)
            {
                if (item.Aantal > 0)
                {
                    _context.OrderItem.Add(new OrderItem { OrderID = order.OrderID, ItemID = item.ItemID, Amount = item.Aantal });
                }
            }
            _context.SaveChanges();

            //send mail
            Functions.SendMail(customer.Email, "Order Comfirmed", "We have recived your order. if you want to change it you have 10 to 15 minutes");

            return View(orderVM);
        }
    }
}
