using System;

namespace Courses.Domain
{
    public class Module
    {
        public Guid ModuleId { get; set; }
        public int LengthInHours { get; set; }
        public string Name { get; set; }
    }
}