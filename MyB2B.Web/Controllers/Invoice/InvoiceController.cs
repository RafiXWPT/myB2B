using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Web.Controllers.Logic;
using MyB2B.Web.Controllers.Logic.Invoice;

namespace MyB2B.Web.Controllers.Invoice
{
    [Authorize]
    [Route("api/[controller]")]
    public class InvoiceController : LogicController<InvoiceLogic>
    {
        public InvoiceController(InvoiceLogic logic) : base(logic)
        {
        }

        [HttpGet("{id}")]
        public IActionResult Invoice(int id)
        {
            return Json(Logic.GetInvoiceDetails(id));
        }

        [HttpGet("company-invoices/{id}")]
        public IActionResult CompanyInvoices(int id)
        {
            return Json(Logic.GetCompanyInvoices(id));
        }

        
        [HttpPost("generate-invoice-test")]
        public IActionResult GenerateInvoiceTest(int id)
        {
            Logic.GenerateInvoice(id);
            return Json(new {identifier = Guid.NewGuid()});
        }

        [HttpGet("download-invoice-test")]
        public IActionResult DownloadInvoiceTest(int id)
        {
            var invoice = Logic.DownloadInvoice(id);
            return File(invoice.InvoiceContent, "application/pdf", $"{invoice.InvoiceNumber}.pdf");
        }
    }
}
