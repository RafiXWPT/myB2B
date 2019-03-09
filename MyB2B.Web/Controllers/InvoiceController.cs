using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Domain.Invoices;
using MyB2B.SampleObjects;
using MyB2B.Server.Documents.Generators;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Queries;

namespace MyB2B.Web.Controllers
{
    public static class FakeInvoiceDb
    {
        public static byte[] GeneratedInvoice { get; set; }
    }

    [Route("api/[controller]")]
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceGenerator _invoiceGenerator;

        public InvoiceController(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor, IInvoiceGenerator invoiceGenerator) : base(commandProcessor, queryProcessor)
        {
            _invoiceGenerator = invoiceGenerator;
        }

        [Authorize]
        [HttpPost("generate-invoice-test")]
        public IActionResult GenerateInvoiceTest(int id)
        {
            var invoice = Samples.SampleInvoice(new Random().NextDouble() < 0.5 ? null : "pdfTemplate.png");

            FakeInvoiceDb.GeneratedInvoice = _invoiceGenerator.Generate(invoice);
            return Json(new {identifier = Guid.NewGuid()});
        }

        [Authorize]
        [HttpGet("download-invoice-test")]
        public IActionResult DownloadInvoiceTest(Guid identifier)
        {
            return File(FakeInvoiceDb.GeneratedInvoice, "application/pdf");
        }
    }
}
