using Orders.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Orders.Infrastructure
{
    public class OrdersContext : DbContext, IOrdersContext
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Orders_TestDb");

            // optionsBuilder.UseSqlServer( @"Server=(localdb)\mssqllocaldb;Database=Orders;Integrated Security=True");
        }
        
    }
}