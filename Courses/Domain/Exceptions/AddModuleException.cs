using System;

namespace Courses.Domain.Exceptions
{
    public class AddModuleException : Exception
    {
        public AddModuleException(string message): base(message)
        {
        } 
    }
}