using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myB2B.Domain
{
    public class CompanyProduct : AuditableImmutableEntity
    {
        [MaxLength(255)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal NetPrice { get; set; }
        public double VatRate { get; set; }
    }
}