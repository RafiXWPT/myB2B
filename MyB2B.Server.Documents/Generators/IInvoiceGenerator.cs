using System;
using System.Collections.Generic;
using System.Text;
using MyB2B.Domain.Invoices;

namespace MyB2B.Server.Documents.Generators
{
    public interface IInvoiceGenerator
    {
        byte[] Generate(Invoice invoice);
    }
}
