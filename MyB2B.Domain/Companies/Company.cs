using System.Collections.Generic;
using MyB2B.Domain.Results;

namespace MyB2B.Domain.Companies
{
    public class Company : AuditableImmutableEntity
    {
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public string Nip { get; set; }
        public string Regon { get; set; }
        public Address Address { get; set; }
        public List<CompanyProduct> Products { get; set; }
        public List<CompanyClient> Clients { get; set; }

        public Result<Company> UpdateNameAndShortCode(string name, string shortCode)
        {
            return Result.Ok(this);
        }

        public Result<Company> UpdateNipAndRegon(string nip, string regon)
        {
            return Result.Ok(this);
        }

        public Result<Company> UpdateAddress(string country, string city, string zipCode, string street, string number)
        {
            return Result.Ok(this);
        }
    }
}