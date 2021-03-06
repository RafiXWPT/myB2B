using System.ComponentModel.DataAnnotations;

namespace MyB2B.Domain.Companies
{
    public class CompanyClient : AuditableImmutableEntity
    {
        [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public string CompanyName { get; set; }

        [MaxLength(40)]
        public string CompanyNip { get; set; }
        
        public virtual Address Address { get; set; }

        public bool IsCompany => string.IsNullOrEmpty(CompanyNip);
    }
}