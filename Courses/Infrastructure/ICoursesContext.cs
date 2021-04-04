using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Courses.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Courses.Infrastructure
{
    public interface ICoursesContext
    {
        DbSet<Course> Courses { get; set; }
        DbSet<Module> Modules { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
        EntityEntry<Course> Entry<Course>([NotNull] Course entity) where Course : class;
    }
}