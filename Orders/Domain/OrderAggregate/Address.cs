using System;

namespace Orders.Domain.Aggregates
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string PostCode { get; private set; }

        public Address()
        {
        }

        public Address(string street, string city, string state, string country, string zipcode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            PostCode = zipcode;
        }
    }
}