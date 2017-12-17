using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using T4TCase.Model;


namespace T4TCase.Data
{
    public class DBInitialize
    {
        public static void Initialize(DatabaseContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (context.Customer.Any() && context.Item.Any() && context.User.Any())
            {
                return;
            }

            //add Customers
            context.Customer.AddRange(
                new Customer { UserName = "florian", LastName = "Pieters", FirstName = "Florian", Age = 25, Email = "florian.pieters@hotmail.com", PhoneNumber = "0479568812", Address = "Ringlaan 89 Bus 5", City = "2610 - WILRIJK" },
                new Customer { UserName = "thomas", LastName = "Sweeck", FirstName = "Thomas", Age = 25, Email = "thomas.sweeck@hotmail.com", PhoneNumber = "0479568813", Address = "Gasthuisstraat 11", City = "2550 - WAARLOOS" },
                new Customer { UserName = "sven", LastName = "Bollaerts", FirstName = "Sven", Age = 25, Email = "sven.bollaerts@hotmail.com", PhoneNumber = "0479569912", Address = "Oranjestraat 66", City = "2010 - ANTWERPEN" }
                );

            //add Items
            context.Item.AddRange(
                new Item { Name = "Broodje Kaas", Description = "Broodje met kaas", Price = 2 },
                new Item { Name = "Broodje Hesp", Description = "Broodje met hesp", Price = 2 },
                new Item { Name = "Broodje Kaashesp", Description = "Broodje met kaas en hesp", Price = 2.5m },
                new Item { Name = "Broodje Martino", Description = "Broodje met preparé, augurken, ajuin en martino saus", Price = 2.5m }
                );
            //create Users with UserRoles
            var usersCreated = SetUsers(context, userManager, roleManager);
        }

        public static async Task<bool> SetUsers(DatabaseContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //create users
            await userManager.CreateAsync(new User { UserName = "thomas" }, "Tho*123");
            await userManager.CreateAsync(new User { UserName = "sven" }, "Sven*123");
            var florian = await userManager.CreateAsync(new User { UserName = "florian" }, "Flo*123");

            if (florian.Succeeded)
            {
                //create UserRoles
                await roleManager.CreateAsync(new IdentityRole { Name = "Member" });
                var asmin = await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

                if (asmin.Succeeded)
                {
                    var flo = context.User.First(x => x.UserName == "florian");
                    var tho = context.User.First(x => x.UserName == "thomas");
                    var sven = context.User.First(x => x.UserName == "sven");

                    //assign roles to users  -> florian = admin (additems)
                    await userManager.AddToRoleAsync(tho, "Member");
                    await userManager.AddToRoleAsync(sven, "Member");
                    var created = await userManager.AddToRoleAsync(flo, "Admin");

                    if (created.Succeeded) return true;
                    else return false;
                }
                else return false;
            }
            else return false;
        }
    }
}
