using System;

namespace Courses.Domain.Exceptions
{
    public class ModuleNotRegisteredOnCourseException : Exception
    {
        public ModuleNotRegisteredOnCourseException() : base("Module has not been registered to this course")
        {
        }
    }
}