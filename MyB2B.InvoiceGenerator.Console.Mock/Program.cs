using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using MyB2B.Domain;
using MyB2B.Domain.Companies;
using MyB2B.Domain.Invoices;
using MyB2B.SampleObjects;
using MyB2B.Server.Documents.Generators;

namespace MyB2B.InvoiceGenerator.Console.Mock
{
    class Program
    {
        static void Main(string[] args)
        {
            var templateName = "pdfTemplate.png";
            var useTemplate = false;

            var invoiceGenerator = new PdfInvoiceGenerator();

            var invoice = Samples.SampleInvoice(useTemplate ? templateName : null);

            var generatedBytes = invoiceGenerator.Generate(invoice);
            System.IO.File.WriteAllBytes("invoice.pdf", generatedBytes);
        }
    }
}
