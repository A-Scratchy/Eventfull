using System;

namespace Courses.Domain.Exceptions
{
    public class AlreadyRegisteredOnCourseException : Exception
    {
        public AlreadyRegisteredOnCourseException()
            : base("Delegate is already registered on this course")
        {
        }
    }
}