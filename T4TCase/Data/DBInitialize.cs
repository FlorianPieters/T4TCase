using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using T4TCase.Model;


namespace T4TCase.Data
{
    public class DBInitialize
    {

        
        public static  void Initialize(DatabaseContext context, UserManager<User> userManager)
        {
            
            context.Database.EnsureCreated();

            if (context.Customer.Any() && context.Item.Any() && context.User.Any())
            {
                return;
            }

            userManager.CreateAsync(new User { UserName = "florian" }, "Flo*123");
            userManager.CreateAsync(new User { UserName = "thomas" }, "Tho*123");
            userManager.CreateAsync(new User { UserName = "sven" }, "Sven*123");

            context.Customer.AddRange(
                new Customer { UserName = "florian", LastName = "Pieters", FirstName = "Florian", Age = 25, Email = "florian.pieters@hotmail.com", PhoneNumer = "0479/56.88.12", Address = "Ringlaan 89 Bus 5", City = "2610 WILRIJK" },
                new Customer { UserName = "thomas", LastName = "Sweeck", FirstName = "Thomas", Age = 25, Email = "thomas.sweeck@hotmail.com", PhoneNumer = "0479/56.88.13", Address = "Gasthuisstraat 11", City = "2550 WAARLOOS" },
                new Customer { UserName = "sven", LastName = "Bollaerts", FirstName = "Sven", Age = 25, Email = "sven.bollaerts@hotmail.com", PhoneNumer = "0479/56.99.12", Address = "Oranjestraat 66", City = "2010 ANTWERPEN" }
                );

            context.Item.AddRange(
                new Item { Name = "Broodje Kaas", Description = "Broodje met kaas", Price = 2 },
                new Item { Name = "Broodje Hesp", Description = "Broodje met hesp", Price = 2 },
                new Item { Name = "Broodje Kaashesp", Description = "Broodje met kaas en hesp", Price = 2.5m },
                new Item { Name = "Broodje Martino", Description = "Broodje met preparé, augurken, ajuin en martino saus", Price = 2.5m }
                );
            
        }
    }
}
