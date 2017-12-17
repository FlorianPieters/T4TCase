using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using T4TCase.Data;
using T4TCase.Method;
using T4TCase.Model;
using T4TCase.ViewModel;

namespace T4TCase.Controllers
{
    //only acces when logged in
    [Authorize]
    public class ChangeOrderController : Controller
    {
        private readonly DatabaseContext _context;

        public ChangeOrderController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Change()
        {
            //get Customer
            var customer = _context.Customer.First(x => x.UserName == User.Identity.Name);
            try
            {
                //get last Order
                var orders = _context.Order.Last(x => x.Customer == customer);

                //get OrderItems from the last Order and join Items in OrderItems
                var orderItems = _context.OrderItem
                .Where(x => x.Order == orders)
                .Join(_context.Item, orderItem => orderItem.ItemID, Item => Item.ItemID, (orderItem, item) => new { OrderItem = orderItem, item })
                .ToList();

                var items = _context.Item.ToList();
                var itemList = new List<ItemViewModel>();
                var orderItemList = new List<ItemViewModel>();

                //create Items in the ItemVM for each OrderItem in the last Order
                foreach (var orderItemsItem in orderItems)
                {
                    ItemViewModel vm = new ItemViewModel { ItemID = orderItemsItem.item.ItemID, Name = orderItemsItem.item.Name, Description = orderItemsItem.item.Description, Price = orderItemsItem.item.Price, Aantal = orderItemsItem.OrderItem.Amount };
                    orderItemList.Add(vm);
                }

                //create Items in the ItemVm for each Item in the db
                foreach (var itemsItem in items)
                {
                    ItemViewModel vm = new ItemViewModel { ItemID = itemsItem.ItemID, Name = itemsItem.Name, Description = itemsItem.Description, Price = itemsItem.Price };
                    itemList.Add(vm);
                }

                //sort a list of the ItemVM with all OrderItems for the last order and all Items from the db
                itemList.RemoveAll(x => orderItemList.Any(y => y.ItemID == x.ItemID));
                itemList.AddRange(orderItemList);
                var sortedItemlist = itemList.OrderBy(x => x.ItemID).ToList();

                //create OrderVM
                var lastOrder = new OrderViewModel { Itemvms = sortedItemlist, Customer = customer, Description = orders.Description, Date = orders.Date, TotalPrice = orders.TotalPrice };

                //check if the last order isn't older then 10 min
                if (lastOrder.Date.AddMinutes(10) <= DateTime.Now) return RedirectToAction("Error", "ChangeOrder");

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
            //check if the Order is older then 15 min
            if (orderVM.Date.AddMinutes(15) <= DateTime.Now) return RedirectToAction("Error", "ChangeOrder");

            //get Customer and last Order and compare them with the VM
            var customer = _context.Customer.First(x => x.UserName == User.Identity.Name);
            Functions.CompareCustomer(_context, customer, orderVM.Customer);
            var order = _context.Order.Last(x => x.CustomerID == customer.CustomerID);
            Functions.CompareOrder(_context, order, orderVM);

            //send mail
            Functions.SendMail(customer.Email, "Order changed", "We have changed your order");
            return View(orderVM);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
