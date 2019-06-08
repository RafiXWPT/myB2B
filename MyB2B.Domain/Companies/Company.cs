using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyB2B.Domain.Results;

namespace MyB2B.Domain.Companies
{
    public class Company : AuditableEntity
    {
        [MinLength(3)]
        [MaxLength(255)]
        public string Name { get; set; }

        [MinLength(2)]
        [MaxLength(10)]
        public string ShortCode { get; set; }

        public string Nip { get; set; }
        public string Regon { get; set; }

        public virtual Address Address { get; set; }
        public virtual List<CompanyProduct> Products { get; set; }
        public virtual List<CompanyClient> Clients { get; set; }

        public static Result<Company> Create()
        {
            return Result.Ok(new Company
            {
                Name = "",
                ShortCode = "",
                Nip = "",
                Regon = "",
                Address = new Address()
            });
        }

        public Result<Company> UpdateNameAndShortCode(string name, string shortCode)
        {
            var cleanName = name?.Trim();
            var cleanShortCode = shortCode?.Trim();

            if (string.IsNullOrEmpty(cleanName) || cleanName.Length > 255 || cleanName.Length < 3)
                return Result.Fail<Company>("Company name length must be between 3-255 characters long.");

            if (string.IsNullOrEmpty(cleanShortCode) || cleanShortCode.Length > 10 || cleanShortCode.Length < 2)
                return Result.Fail<Company>("Company short code length must be between 2-10 characters long.");

            Name = name;
            ShortCode = shortCode;
            return Result.Ok(this);
        }

        public Result<Company> UpdateNipAndRegon(string nip, string regon)
        {
            var cleanNip = nip?.Replace("-", "").Trim();
            var cleanRegon = regon?.Replace("-", "").Trim();

            if (string.IsNullOrEmpty(cleanNip) || cleanNip.Length != 10)
                return Result.Fail<Company>("Nip identifier must have 10 digits");

            if (string.IsNullOrEmpty(cleanRegon) || regon.Length != 9)
                return Result.Fail<Company>("Regon identifier must have 10 digits");

            Nip = cleanNip;
            Regon = cleanRegon;
            return Result.Ok(this);
        }

        public Result<Company> UpdateAddress(string country, string city, string zipCode, string street, string number)
        {
            return Address.Update(country, city, zipCode, street, number)
                .OnSuccess(_ => this)
                .OnFailure(err => Result.Fail<Company>(err));
        }
    }
}