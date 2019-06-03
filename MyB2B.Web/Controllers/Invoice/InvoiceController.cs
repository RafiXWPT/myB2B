using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Web.Controllers.Logic;
using MyB2B.Web.Controllers.Logic.Invoice;

namespace MyB2B.Web.Controllers.Invoice
{
    [Route("api/[controller]")]
    public class InvoiceController : LogicController<InvoiceControllerLogic>
    {
        public InvoiceController(InvoiceControllerLogic logic) : base(logic)
        {
        }

        [Authorize]
        [HttpPost("generate-invoice-test")]
        public IActionResult GenerateInvoiceTest(int id)
        {
            Logic.GenerateInvoice(id);
            return Json(new {identifier = Guid.NewGuid()});
        }

        [Authorize]
        [HttpGet("download-invoice-test")]
        public IActionResult DownloadInvoiceTest(int id)
        {
            var invoice = Logic.DownloadInvoice(id);
            return File(invoice.InvoiceContent, "application/pdf", $"{invoice.InvoiceNumber}.pdf");
        }
    }
}
