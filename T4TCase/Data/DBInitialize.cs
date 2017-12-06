using System;
using System.Linq;
using T4TCase.Model;


namespace T4TCase.Data
{
    public class DBInitialize
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customer.Any() && context.Item.Any() && context.Order.Any() && context.OrderItem.Any())
            {
                return;
            }

        context.Customer.AddRange(
                new Customer { LastName = "Pieters", FirstName = "Florian", Age = 25, Mail = "florian.pieters@hotmail.com", PhoneNumer = "0479/56.88.12",  Address = "Ringlaan 89 Bus 5", City = "2610 WILRIJK" },
                new Customer { LastName = "Sweeck", FirstName = "Thomas", Age = 25, Mail = "thomas.sweeck@hotmail.com", PhoneNumer = "0479/56.88.13", Address = "Gasthuisstraat 11", City = "2550 WAARLOOS" },
                new Customer { LastName = "Bollaerts", FirstName = "Sven", Age = 25, Mail = "sven.bollaerts@hotmail.com", PhoneNumer = "0479/56.99.12", Address = "Oranjestraat 66", City = "2010 ANTWERPEN" }
                );
        context.Item.AddRange(
                new Item { Name = "Broodje Kaas", Description = "Broodje met kaas", Price = "2,00" },
                new Item { Name = "Broodje Hesp", Description = "Broodje met hesp", Price = "2,00" },
                new Item { Name = "Broodje Kaashesp", Description = "Broodje met kaas en hesp", Price = "2,50" },
                new Item { Name = "Broodje Martino", Description = "Broodje met preparé, augurken, ajuin en martino saus", Price = "2,50" }
                );
        context.User.Add(
                new User {  UserName = "florian", PasswordHash = "flo123", Email = "florian.pieters@hotmail.com"/*, CustomerID = 1*/ }

                );
            context.OrderItem.AddRange(
                    new OrderItem {OrderID = 1, ItemID = 1, Amount = 1},
                    new OrderItem {OrderID = 1, ItemID = 4, Amount = 2 }
                    );
            context.Order.Add(
                    new Order {CustomerID = 1, Date = new DateTimeOffset(2011, 6, 10, 15, 24, 16, TimeSpan.Zero) }
                    );

            context.SaveChanges();
        }

    }
}
