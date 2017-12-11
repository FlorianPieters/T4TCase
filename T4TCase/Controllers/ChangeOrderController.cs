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
            var lastOrder = GetLastOrder();

            if (lastOrder.date.AddMinutes(10) <= DateTime.Now)
            {
               return RedirectToAction("Error", "ChangeOrder");
            }
            return View(lastOrder);
        }


        [HttpPost]
        public IActionResult Change(OrderViewModel Ordervm)
        {

            if (Ordervm.date.AddMinutes(15) <= DateTime.Now)
            {
                return RedirectToAction("Error", "ChangeOrder");
            }
            var customer = _context.Customer.First(x => x.UserName == User.Identity.Name);
            Functions.CompareCustomer(_context, customer, Ordervm.customer);

            var order = _context.Order.Last( x =>  x.CustomerID == customer.CustomerID);
            Functions.CompareOrder(_context, order, Ordervm);


            return View(Ordervm);
        }

        public ActionResult Error()
        {
            return Content("To late");
        }


        public OrderViewModel GetLastOrder()
        {

            var customer = _context.Customer.First(x => x.UserName == User.Identity.Name);
            var orders = _context.Order.Last(x => x.Customer == customer);

            var OrderItems = _context.OrderItem
                .Where(x => x.Order == orders)
                .Join(_context.Item, orderItem => orderItem.ItemID, Item => Item.ItemID, (orderItem, item) => new { OrderItem = orderItem, item })
                .ToList();

            var Items = _context.Item.ToList();
            var ItemList = new List<ItemViewModel>();
            var OrderItemList = new List<ItemViewModel>();

            foreach (var item in OrderItems)
            {
                ItemViewModel vm = new ItemViewModel { ItemID = item.item.ItemID, Name = item.item.Name, Description = item.item.Description, Price = item.item.Price , Aantal = item.OrderItem.Amount};
                OrderItemList.Add(vm);
            }

            foreach (var item in Items)
            {
                ItemViewModel vm = new ItemViewModel { ItemID = item.ItemID, Name = item.Name, Description = item.Description, Price = item.Price };
                ItemList.Add(vm);
            }

            ItemList.RemoveAll(x => OrderItemList.Any(y => y.ItemID == x.ItemID));
            ItemList.AddRange(OrderItemList);
            var SortedItemlist =ItemList.OrderBy(x=>x.ItemID).ToList();

            var lastOrder = new OrderViewModel { itemvms = SortedItemlist, customer = customer, description = orders.Description, date = orders.Date };
            return lastOrder;
        }
    }
}
 