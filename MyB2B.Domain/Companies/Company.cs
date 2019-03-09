using System.Collections.Generic;

namespace MyB2B.Domain.Companies
{
    public class Company : AuditableImmutableEntity
    {
        public string Name { get; set; }
        public string Nip { get; set; }
        public string Regon { get; set; }
        public string ShortCode { get; set; }
        public Address Address { get; set; }
        public List<CompanyProduct> Products { get; set; }
        public List<CompanyClient> Clients { get; set; }
    }
}