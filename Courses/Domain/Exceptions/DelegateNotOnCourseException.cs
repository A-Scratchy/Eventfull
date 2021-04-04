using System;

namespace Courses.Domain.Exceptions
{
    public class DelegateNotOnCourseException : Exception
    {
        
        public DelegateNotOnCourseException()
            : base(String.Format("Delegate is not registered on this course"))
        {
        }
    }
}