using System;
using System.Net.Mail;

namespace People.Domain
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ContactNumber { get; set; }
        public string Email { get; set; }
    }

}