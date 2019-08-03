using System;
using System.Collections.Generic;
using System.Text;

namespace MyB2B.Web.Controllers.Logic.Invoice.Models
{
    public class CompanyInvoiceListDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string BuyerName { get; set; }
        public DateTime GeneratedAt { get; set; }
        public decimal TotalGrossAmount { get; set; }
    }
}
