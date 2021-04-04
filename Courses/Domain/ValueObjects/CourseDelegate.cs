using System;
using System.Collections.Generic;

namespace Courses.Domain.ValueObjects
{
    public class CourseDelegate : ValueObject
    {
        public CourseDelegate(Guid contactId, Guid accountId)
        {
            ContactId = contactId;
            AccountId = accountId;
        }

        public Guid ContactId { get; private set; }
        public Guid AccountId { get; private set; }
        public bool? Attended { get; private set; }
        public bool? Passed { get; private set; }

        public void PassDelegate()
        {
            Passed = true;
        }

        public void FailDelegate()
        {
            Passed = false;
        }

        public void MarkAttendance(bool attended)
        {
            Attended = attended;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ContactId;
            yield return AccountId;
        }
    }
}