using System;
using System.Collections.Generic;
using System.Text;

namespace MyB2B.Web.Controllers.Logic.Invoice.Models
{
    public class InvoiceDto
    {
        public string InvoiceNumber { get; set; }
        public byte[] InvoiceContent { get; set; }
    }
}
