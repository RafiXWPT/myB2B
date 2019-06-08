using System.ComponentModel.DataAnnotations;
using MyB2B.Domain.Results;

namespace MyB2B.Domain
{
    public class Address : ApplicationEntity
    {
        [MaxLength(255)]
        public string Country { get; set; } = "";

        [MaxLength(255)]
        public string City { get; set; } = "";

        [MaxLength(10)]
        public string ZipCode { get; set; } = "";

        [MaxLength(255)]
        public string Street { get; set; } = "";

        [MaxLength(10)]
        public string Number { get; set; } = "";

        public string GetStreetAndNumber() => $"{Street} {Number}";

        public string GetZipAndCity() => $"{ZipCode} {City}";

        public Result<Address> Update(string country, string city, string zipCode, string street, string number)
        {
            Country = country;
            City = city;
            ZipCode = zipCode;
            Street = street;
            Number = number;

            return Result.Ok(this);
        }
    }
}