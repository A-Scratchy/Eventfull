using System;
using System.Collections.Generic;
using System.Transactions;

namespace Courses.Domain.ValueObjects
{
    public class CourseModule : ValueObject
    {
        public string ModuleName { get; set; }
        public Guid ModuleId { get; set; }
        public DateTime ModuleStartTime { get; set; }
        public Guid? Instructor { get; set; }
        public int LengthInHours { get; set; }

        public CourseModule()
        {
        }

        public CourseModule(Guid moduleId, string moduleName, DateTime moduleStartTime, int lengthInHours, Guid? instructorId = null)
        {
            ModuleId = moduleId;
            ModuleName = moduleName; 
            ModuleStartTime = moduleStartTime;
            Instructor = instructorId;
            LengthInHours = lengthInHours;
        }

        public CourseModule(Guid moduleId)
        {
            ModuleId = moduleId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ModuleId;
        }
    }
}