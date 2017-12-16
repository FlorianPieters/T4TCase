using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using T4TCase.Model;
using T4TCase.Data;
using T4TCase.ViewModel;
using T4TCase.Method;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T4TCase.Controllers
{
    [Authorize]
    public class ChangeOrderController : Controller
    {
        private readonly DatabaseContext _context;

        public ChangeOrderController(DatabaseContext context)
        {
            _context = context;
        }


        // GET: /<controller>/
        [HttpGet]
        public IActionResult Change()
        {
            var customer = _context.Customer.First(x => x.UserName == User.Identity.Name);
            try
            {
                var orders = _context.Order.Last(x => x.Customer == customer);

                var orderItems = _context.OrderItem
                .Where(x => x.Order == orders)
                .Join(_context.Item, orderItem => orderItem.ItemID, Item => Item.ItemID, (orderItem, item) => new { OrderItem = orderItem, item })
                .ToList();

                var items = _context.Item.ToList();
                var ItemList = new List<ItemViewModel>();
                var OrderItemList = new List<ItemViewModel>();

                foreach (var orderItemsItem in orderItems)
                {
                    ItemViewModel vm = new ItemViewModel { ItemID = orderItemsItem.item.ItemID, Name = orderItemsItem.item.Name, Description = orderItemsItem.item.Description, Price = orderItemsItem.item.Price, Aantal = orderItemsItem.OrderItem.Amount };
                    OrderItemList.Add(vm);
                }

                foreach (var itemsItem in items)
                {
                    ItemViewModel vm = new ItemViewModel { ItemID = itemsItem.ItemID, Name = itemsItem.Name, Description = itemsItem.Description, Price = itemsItem.Price };
                    ItemList.Add(vm);
                }

                ItemList.RemoveAll(x => OrderItemList.Any(y => y.ItemID == x.ItemID));
                ItemList.AddRange(OrderItemList);
                var sortedItemlist = ItemList.OrderBy(x => x.ItemID).ToList();

                var lastOrder = new OrderViewModel { Itemvms = sortedItemlist, Customer = customer, Description = orders.Description, Date = orders.Date, TotalPrice = orders.TotalPrice };

                if (lastOrder.Date.AddMinutes(10) <= DateTime.Now)
                {
                    return RedirectToAction("Error", "ChangeOrder");
                }
                return View(lastOrder);
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
                return Content(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult Change(OrderViewModel orderVM)
        {

            if (orderVM.Date.AddMinutes(15) <= DateTime.Now)
            {
                return RedirectToAction("Error", "ChangeOrder");
            }
            var customer = _context.Customer.First(x => x.UserName == User.Identity.Name);
            Functions.CompareCustomer(_context, customer, orderVM.Customer);

            var order = _context.Order.Last( x =>  x.CustomerID == customer.CustomerID);
            Functions.CompareOrder(_context, order, orderVM);

          //  Functions.SendMail(customer.Email, "Order changed", "We have changed your order");
            return View(orderVM);
        }
                
        public ActionResult Error(string exMessage)
        {
            return View();
        }       
    }
}
 