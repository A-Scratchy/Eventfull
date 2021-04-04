using System;
using System.Collections.Generic;
using System.Linq;
using Courses.Domain.Exceptions;
using Courses.Domain.ValueObjects;

namespace Courses.Domain
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public DateTime CourseDate { get; set; }
        public ICollection<CourseModule> CourseModules { get; private set; } = new List<CourseModule>();
        public int Capacity { get; set; }
        public ICollection<CourseDelegate> CourseDelegates { get; private set; } = new List<CourseDelegate>();

        public void AddDelegate(Guid personId, Guid accountId)
        {
            var courseDelegate = new CourseDelegate(personId, accountId);

            if (CourseDelegates.Contains(courseDelegate))
            {
                throw new AlreadyRegisteredOnCourseException();
            }

            CourseDelegates.Add(courseDelegate);
        }

        public void RemoveDelegate(Guid personId, Guid accountId)
        {
            var courseDelegate = new CourseDelegate(personId, accountId);

            var existingDelegate = CourseDelegates.FirstOrDefault(c => c == courseDelegate);

            if (existingDelegate == null)
            {
                throw new DelegateNotOnCourseException();
            }

            CourseDelegates.Remove(courseDelegate);
        }

        public void AddModule(Guid moduleId, string moduleName, DateTime startTime, int lengthInHours, Guid? instructorId = null)
        {
            if (lengthInHours < 0.25)
            {
                throw new AddModuleException("Length in hours must be greater than 0.25");
            }
            
            var courseModule = new CourseModule(moduleId, moduleName, startTime, lengthInHours, instructorId);

            if (CourseModules.Contains(courseModule))
            {
                throw new AddModuleException("This module has already been registered on this course");
            }

            if (CourseModules.Any(m => startTime >= m.ModuleStartTime && startTime <= m.ModuleStartTime.AddHours(m.LengthInHours)))
            {
                throw new AddModuleException("There is already a module on this course within this time range");
            }

            CourseModules.Add(courseModule);
        }

        public void RemoveCourseModule(Guid moduleId)
        {
            var courseModule = new CourseModule(moduleId);

            var existingCourseModule = CourseModules.FirstOrDefault(c => c == courseModule);

            if (existingCourseModule == null)
            {
                throw new ModuleNotRegisteredOnCourseException();
            }

            CourseModules.Remove(courseModule);
        }
    }
}