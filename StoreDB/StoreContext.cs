using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreDB.Models;

namespace StoreDB
{
    public class StoreContext : DbContext
    {
        public DbSet<Location> Locations {get; set;}
        public DbSet<Customer> Customers {get; set;}
        public DbSet<Cart> Carts {get; set;}
        public DbSet<InvItem> InvItems {get; set;}
        public DbSet<CartItem> CartItems {get; set;}
        public DbSet<OrderItem> OrderItems {get; set;}
        public DbSet<Product> Products {get; set;}
        public DbSet<Order> Orders {get; set;}
        public DbSet<Address> Addresses {get; set;}

        public StoreContext(){}

        public StoreContext(DbContextOptions<StoreContext> options) : base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if( !optionsBuilder.IsConfigured )
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("StoreDB"); //refers to the specific string in appsettings.json
                optionsBuilder.UseNpgsql(connectionString); //connects the database
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //composite key for InvItems: productId and locationId
            modelBuilder.Entity<InvItem>().HasKey(i => new { i.ProductId, i.LocationId });
            //composite key for CartItems: productId and cartId
            modelBuilder.Entity<CartItem>().HasKey(c => new { c.ProductId, c.CartId });
            //composite key for OrderItems: productId and orderId
            modelBuilder.Entity<OrderItem>().HasKey(o => new { o.ProductId, o.OrderId });
        }
    }
}