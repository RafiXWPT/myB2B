using MyB2B.Domain.Invoices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyB2B.Web.Controllers.Logic.Invoice.Models
{
    public class InvoiceDetailsDto
    {
        public string Number { get; set; }
        public string DealerName { get; set; }
        public string DealerCompany { get; set; }
        public string DealerNip { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCompany { get; set; }
        public string BuyerNip { get; set; }
        public DateTime GeneratedAt { get; set; }
        public InvoiceStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime PaymentToDate { get; set; }
        public List<InvoiceItemDto> Items { get; set; }
        public decimal TotalGrossAmount { get; set; }
    }
}
