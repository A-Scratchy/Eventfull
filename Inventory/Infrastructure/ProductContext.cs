using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure
{
    public class ProductContext : DbContext, IProductContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Inventory_TestDb");

            // optionsBuilder.UseSqlServer( @"Server=(localdb)\mssqllocaldb;Database=Inventory;Integrated Security=True");
        }
    }
}