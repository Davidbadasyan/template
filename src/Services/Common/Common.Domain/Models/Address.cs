using Common.Domain.SeedWork;

namespace Common.Domain.Models;

public class Address : ValueObject
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }

    private Address() { }

    public Address(string street, string city, string state, string country, string zipcode)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipcode;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return ZipCode;
    }

    public class AddressBuilder
    {
        private readonly Address _address;
        public AddressBuilder()
        {
            _address = new Address();
        }

        public AddressBuilder WithStreet(string street)
        {
            _address.Street = street;
            return this;
        }

        public AddressBuilder WithCity(string city)
        {
            _address.City = city;
            return this;
        }

        public AddressBuilder WithState(string state)
        {
            _address.State = state;
            return this;
        }

        public AddressBuilder WithCountry(string country)
        {
            _address.Country = country;
            return this;
        }

        public AddressBuilder WithZipCode(string zipcode)
        {
            _address.ZipCode = zipcode;
            return this;
        }

        public Address Build()
        {
            return _address;
        }
    }

}
