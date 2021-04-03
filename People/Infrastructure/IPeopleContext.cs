using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using People.Domain;

namespace People.Infrastructure
{
    public interface IPeopleContext
    {
        DbSet<Person> People { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        EntityEntry<Person> Entry<Person>([NotNull] Person entity) where Person : class;
    }
}