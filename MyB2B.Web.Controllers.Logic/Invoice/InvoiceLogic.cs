using System;
using System.Collections.Generic;
using System.Text;
using MyB2B.Domain.Results;
using MyB2B.SampleObjects;
using MyB2B.Server.Documents.Generators;
using MyB2B.Web.Controllers.Logic.Authentication.Queries;
using MyB2B.Web.Controllers.Logic.Invoice.Models;
using MyB2B.Web.Controllers.Logic.Invoice.Queries;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Queries;

namespace MyB2B.Web.Controllers.Logic.Invoice
{
    public static class FakeInvoiceDb
    {
        public static byte[] GeneratedInvoice { get; set; }
    }

    public class InvoiceLogic : ControllerLogic
    {
        private readonly IInvoiceGenerator _invoiceGenerator;

        public InvoiceLogic(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor, IInvoiceGenerator invoiceGenerator) : base(commandProcessor, queryProcessor)
        {
            _invoiceGenerator = invoiceGenerator;
        }

        public Result<InvoiceDetailsDto> GetInvoiceDetails(int invoiceId)
        {
            return QueryProcessor.Query(new GetCompanyInvoiceDetailsByIdQuery(invoiceId, CurrentPrincipal.CompanyId));
        }

        public Result<List<CompanyInvoiceListDto>> GetCompanyInvoices(int companyId)
        {
            if(CurrentPrincipal.CompanyId != companyId)
            {
                return Result.Fail<List<CompanyInvoiceListDto>>("This company doesn't belongs to that user.");
            }

            return QueryProcessor.Query(new GetCompanyInvoicesQuery(companyId));
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
