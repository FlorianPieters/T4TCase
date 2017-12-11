using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T4TCase.Data;
using T4TCase.Model;
using T4TCase.ViewModel;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace T4TCase.Controllers
{
    [Authorize]
    public class OrderHistoryController : Controller
    {
        private readonly DatabaseContext _context;

        public OrderHistoryController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult History()
        {

            var customer = _context.Customer.First(x => x.UserName == User.Identity.Name);
            var orders = _context.Order.Where(x => x.Customer == customer)
                .Join(_context.OrderItem, order => order.OrderID, orderitem=> orderitem.OrderID, (order, orderitem) => new { order, orderitem})
                .Join(_context.Item, x => x.orderitem.ItemID, y => y.ItemID, (x, y) => new { order = x.order, orderitem = x.orderitem, y =y})
                //.GroupJoin(_context.Item, a => a.orderitem.Join)
                .Select(order => new { Order = order})
                .ToList();

            var OrderHistory = new List<OrderListViewModel>();
           

            foreach (var order in orders)
            {

                var ItemList = new List<ItemViewModel>();
                foreach (var item in order.Order.order.OrderItems)
                {
                    
                    
                    ItemViewModel vm = new ItemViewModel { ItemID = item.ItemID, Name = item.Item.Name,
                                        Description = item.Item.Description, Price = item.Item.Price, Aantal = item.Amount };
                    ItemList.Add(vm);

                }

                var Ordervm = new OrderListViewModel
                {
                    itemvms = ItemList,
                    description = order.Order.order.Description,
                    totalprice = order.Order.order.TotalPrice,
                    date = order.Order.order.Date
                };
                
                OrderHistory.Add(Ordervm);
              //  ItemList.Clear();
            }




    

            return View(OrderHistory);
        }
    }
}
