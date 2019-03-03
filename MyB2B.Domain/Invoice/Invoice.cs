using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyB2B.Domain.Invoice
{
    public class Invoice : AuditableImmutableEntity
    {
        [MaxLength(255)]
        public string Number { get; set; }

        public DateTime Generated { get; set; }

        public InvoiceStatus Status { get; set; }

        public List<InvoicePosition> Items { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal TotalNetAmount { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal TotalTaxAmount { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal TotalGrossAmount { get; set; }

        public double InvoiceDiscount { get; set; }

        public byte[] Template { get; set; }
    }
}