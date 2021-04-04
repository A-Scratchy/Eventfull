using System;
using Courses.Domain;
using Faker;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure
{
    public class CoursesContext : DbContext, ICoursesContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Test_Courses");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CourseConfiguration().Configure(modelBuilder.Entity<Course>());

            DateTime RandomDate() => new DateTime(Faker.Number.RandomNumber(2021,2022), Faker.Number.RandomNumber(1, 12),
                Faker.Number.RandomNumber(1, 28));

            
            modelBuilder.Entity<Course>().HasData(
                new Course { Capacity = Faker.Number.RandomNumber(5, 30), CourseDate = RandomDate(), CourseId = Guid.NewGuid() },
                new Course { Capacity = Faker.Number.RandomNumber(5, 30), CourseDate = RandomDate(), CourseId = Guid.NewGuid() },
                new Course { Capacity = Faker.Number.RandomNumber(5, 30), CourseDate = RandomDate(), CourseId = Guid.NewGuid() },
                new Course { Capacity = Faker.Number.RandomNumber(5, 30), CourseDate = RandomDate(), CourseId = Guid.NewGuid() }
                );

            modelBuilder.Entity<Module>().HasData(
                new Module { ModuleId = Guid.NewGuid(), LengthInHours = Faker.Number.RandomNumber(0,5), Name = Faker.Education.Major() },
                new Module { ModuleId = Guid.NewGuid(), LengthInHours = Faker.Number.RandomNumber(0,5), Name = Faker.Education.Major() },
                new Module { ModuleId = Guid.NewGuid(), LengthInHours = Faker.Number.RandomNumber(0,5), Name = Faker.Education.Major() },
                new Module { ModuleId = Guid.NewGuid(), LengthInHours = Faker.Number.RandomNumber(0,5), Name = Faker.Education.Major() },
                new Module { ModuleId = Guid.NewGuid(), LengthInHours = Faker.Number.RandomNumber(0,5), Name = Faker.Education.Major() }
            );

        }
    }
}