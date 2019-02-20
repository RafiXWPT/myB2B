using System.ComponentModel.DataAnnotations.Schema;
using myB2B.Domain.Company;

namespace myB2B.Domain.Invoice
{
    public class InvoicePosition : AuditableImmutableEntity
    {
        public CompanyProduct Product { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal TotalNetAmount { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal TotalTaxAmount { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal TotalGrossAmount { get; set; }

        public double ProductDiscount { get; set; }
    }
}