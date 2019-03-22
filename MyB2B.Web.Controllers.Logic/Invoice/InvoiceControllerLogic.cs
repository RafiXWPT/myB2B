using System;
using System.Collections.Generic;
using System.Text;
using MyB2B.SampleObjects;
using MyB2B.Server.Documents.Generators;
using MyB2B.Web.Controllers.Logic.Invoice.Models;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Queries;

namespace MyB2B.Web.Controllers.Logic.Invoice
{
    public static class FakeInvoiceDb
    {
        public static byte[] GeneratedInvoice { get; set; }
    }

    public class InvoiceControllerLogic : ControllerLogic
    {
        private readonly IInvoiceGenerator _invoiceGenerator;

        public InvoiceControllerLogic(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor, IInvoiceGenerator invoiceGenerator) : base(commandProcessor, queryProcessor)
        {
            _invoiceGenerator = invoiceGenerator;
        }

        public void GenerateInvoice(int invoiceId)
        {
            var invoice = Samples.SampleInvoice(new Random().NextDouble() < 0.5 ? null : "pdfTemplate.png");
            FakeInvoiceDb.GeneratedInvoice = _invoiceGenerator.Generate(invoice);
        }

        public InvoiceDto DownloadInvoice(int invoiceId)
        {
            return new InvoiceDto {InvoiceNumber = "test_invoice", InvoiceContent = FakeInvoiceDb.GeneratedInvoice};
        }
    }
}
