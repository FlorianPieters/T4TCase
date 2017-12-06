using Microsoft.EntityFrameworkCore;
using T4TCase.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace T4TCase.Data
{
    public class DatabaseContext: IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

            public DbSet<Customer> Customer { get; set; }
            public DbSet<Item> Item { get; set; }
            public DbSet<OrderItem> OrderItem { get; set; }
            public DbSet<Order> Order { get; set; }
            public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Customer>().ToTable("Customer");
        builder.Entity<Item>().ToTable("Item");
        builder.Entity<OrderItem>().ToTable("OrderItem");
        builder.Entity<Order>().ToTable("Order");
        builder.Entity<User>().ToTable("User");

        base.OnModelCreating(builder);

        }
    }
}
