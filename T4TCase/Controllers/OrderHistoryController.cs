using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using T4TCase.Data;
using T4TCase.ViewModel;

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

        public IActionResult History()
        {
            //get list of Orders from the Customer
            var customer = _context.Customer.First(customerID => customerID.UserName == User.Identity.Name);
            var orders = _context.Order.Where(objOrder => objOrder.Customer == customer)
                .GroupJoin(_context.OrderItem, order => order.OrderID, orderitem => orderitem.OrderID, (order, orderitem) => new { order, orderitem })
                .OrderByDescending(o => o.order.Date)
                .ToList();

            var items = _context.Item.ToList();
            var OrderHistory = new List<OrderListViewModel>();

            foreach (var order in orders)
            {
                //add OrderItems in VM
                var itemList = new List<ItemViewModel>();
                foreach (var item in order.order.OrderItems)
                {
                    ItemViewModel vm = new ItemViewModel
                    {
                        ItemID = item.ItemID,
                        Name = item.Item.Name,
                        Description = item.Item.Description,
                        Price = item.Item.Price,
                        Aantal = item.Amount
                    };
                    itemList.Add(vm);
                }

                //add orders in VM
                var OrderVM = new OrderListViewModel
                {
                    Itemvms = itemList,
                    Description = order.order.Description,
                    TotalPrice = order.order.TotalPrice,
                    Date = order.order.Date
                };
                OrderHistory.Add(OrderVM);
            }
            return View(OrderHistory);
        }
    }
}
