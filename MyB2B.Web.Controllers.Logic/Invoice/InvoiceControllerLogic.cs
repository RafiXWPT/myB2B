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

    public class InvoiceControllerLogic
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandProcessor _commandProcessor;
        private readonly IInvoiceGenerator _invoiceGenerator;

        public InvoiceControllerLogic(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor, IInvoiceGenerator invoiceGenerator)
        {
            _queryProcessor = queryProcessor;
            _commandProcessor = commandProcessor;
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
