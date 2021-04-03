using Microsoft.EntityFrameworkCore;
using People.Domain;

namespace People.Infrastructure
{
    public class PeopleContext: DbContext, IPeopleContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("People_TestDb");
            
            // optionsBuilder.UseSqlServer( @"Server=(localdb)\mssqllocaldb;Database=Inventory;Integrated Security=True");
        } 
    }
}