using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T4TCase.Model;
using T4TCase.Data;
using T4TCase.ViewModel;

namespace T4TCase.Method
{
    public class Functions
    {
        public static void CompareCustomer(DatabaseContext context, Customer a, Customer b)
        {
            
            if (a.LastName != b.LastName
                    || a.FirstName != b.FirstName
                    || a.Email != b.Email
                    || a.PhoneNumer != b.PhoneNumer
                    || a.Age != b.Age
                    || a.Address != b.Address
                    || a.City != b.City)
            {
                a.LastName = b.LastName;
                a.FirstName = b.FirstName;
                a.Email = b.Email;
                a.PhoneNumer = b.PhoneNumer;
                a.Age = b.Age;
                a.Address = b.Address;
                a.City = b.City;
                context.SaveChanges();
            }
        }
        public static void CompareOrder(DatabaseContext context,Order a, OrderViewModel b)
        {
            decimal TotalPrice = 0;
            foreach (var item in b.itemvms)
            {
                if (item.Aantal > 0)
                {
                    decimal Price = item.Price;
                    Price = Price * item.Aantal;
                    TotalPrice += Price;

                }
            }
            if (a.Description != b.description || a.TotalPrice!=b.totalprice)
            {
                a.Description = b.description;
                a.TotalPrice = TotalPrice;
                context.SaveChanges();
            }

            foreach (var item in b.itemvms)
            {
                if (item.Aantal > 0)
                {
                   // var test = a.OrderID;
                   // var i = context.OrderItem.Any(x=>x.OrderID == a.OrderID && x.ItemID == item.ItemID);
                    if (context.OrderItem.Any(x => x.OrderID == a.OrderID && x.ItemID == item.ItemID))
                    {
                        var i = context.OrderItem.First(x => x.OrderID == a.OrderID && x.ItemID == item.ItemID);
                        if (i.Amount != item.Aantal)
                        {
                            i.Amount = item.Aantal;
                        }

                        //context.SaveChanges();
                    }
                    else
                    {
                        context.OrderItem.Add(new OrderItem { OrderID = a.OrderID, ItemID = item.ItemID, Amount = item.Aantal });
                    }
                }
                
            }

            context.SaveChanges();

        }
    }
}
