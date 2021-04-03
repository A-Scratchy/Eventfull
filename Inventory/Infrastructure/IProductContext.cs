using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Inventory.Infrastructure
{
    public interface IProductContext
    {
        DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry<Product> Entry<Product>([NotNull] Product entity) where Product : class;
    }
}