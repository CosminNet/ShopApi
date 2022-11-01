using Microsoft.EntityFrameworkCore;
using WebShop.DataAccess.Models;

namespace WebShop.DataAccess
{
    public class WebShopContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public WebShopContext(DbContextOptions<WebShopContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>();
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<Basket>(); 
        }
    }
}
