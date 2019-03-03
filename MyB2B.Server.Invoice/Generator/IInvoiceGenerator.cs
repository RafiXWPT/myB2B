using System;
using System.Collections.Generic;
using System.Text;

namespace MyB2B.Server.Invoice.Generator
{
    public interface IInvoiceGenerator
    {
        byte[] Generate(Domain.Invoice.Invoice invoice)
    }
}
