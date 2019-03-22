using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyB2B.Domain.Companies;

namespace MyB2B.Domain.Invoices
{
    public class Invoice : AuditableImmutableEntity
    {
        [MaxLength(255)]
        public string Number { get; set; }

        public string DealerName { get; set; }
        public string DealerCompany { get; set; }
        public string DealerNip { get; set; }
        public virtual Address DealerAddress { get; set; }

        public string BuyerName { get; set; }
        public string BuyerCompany { get; set; }
        public string BuyerNip { get; set; }
        public virtual Address BuyerAddress { get; set; }

        public DateTime Generated { get; set; }

        public InvoiceStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime PaymentToDate { get; set; }
        public string PaymentBankAccount { get; set; }
        public string PaymentBankName { get; set; }

        public virtual List<InvoicePosition> Items { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal TotalNetAmount { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal TotalTaxAmount { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal TotalGrossAmount { get; set; }

        [Column(TypeName = "decimal(12,5)")]
        public decimal PaidAmount { get; set; }

        public double InvoiceDiscount { get; set; }

        public byte[] Template { get; set; }
    }
}