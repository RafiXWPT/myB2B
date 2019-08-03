using System;
using System.Collections.Generic;
using System.Text;

namespace MyB2B.Web.Controllers.Logic.Invoice.Models
{
    public class InvoiceItemDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public double Discount { get; set; }
    }
}
