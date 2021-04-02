using System.Threading;
using System.Threading.Tasks;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure
{
    public interface IInventoryContext
    {
        DbSet<Product> Products { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}