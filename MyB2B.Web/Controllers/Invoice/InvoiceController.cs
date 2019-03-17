using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Web.Controllers.Logic.Invoice;
using MyB2B.Web.Infrastructure.Controllers;

namespace MyB2B.Web.Controllers.Invoice
{
    [Route("api/[controller]")]
    public class InvoiceController : BaseController
    {
        private readonly InvoiceControllerLogic _controllerLogic;

        public InvoiceController(InvoiceControllerLogic controllerLogic)
        {
            _controllerLogic = controllerLogic;
        }

        [Authorize]
        [HttpPost("generate-invoice-test")]
        public IActionResult GenerateInvoiceTest(int id)
        {
            _controllerLogic.GenerateInvoice(id);
            return Json(new {identifier = Guid.NewGuid()});
        }

        [Authorize]
        [HttpGet("download-invoice-test")]
        public IActionResult DownloadInvoiceTest(int id)
        {
            var invoice = _controllerLogic.DownloadInvoice(id);
            return File(invoice.InvoiceContent, "application/pdf", $"{invoice.InvoiceNumber}.pdf");
        }
    }
}
