using Courses.Domain;
using Courses.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Infrastructure
{
    public class CourseConfiguration: IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.OwnsMany<CourseDelegate>(c => c.CourseDelegates);
            builder.OwnsMany<CourseModule>(c => c.CourseModules);
        }
    }
}
