using System.ComponentModel.DataAnnotations.Schema;
using MyB2B.Domain.Companies;

namespace MyB2B.Domain.Invoices
{
    public class InvoicePosition : AuditableImmutableEntity
    {
        public virtual CompanyProduct Product { get; set; }

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