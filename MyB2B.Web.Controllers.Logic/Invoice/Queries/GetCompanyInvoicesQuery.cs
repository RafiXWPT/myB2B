using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Results;
using MyB2B.Web.Controllers.Logic.Invoice.Models;
using MyB2B.Web.Infrastructure.Actions.Queries;
using System.Collections.Generic;
using System.Linq;

namespace MyB2B.Web.Controllers.Logic.Invoice.Queries
{
    public class GetCompanyInvoicesQuery : Query<List<CompanyInvoiceListDto>>
    {
        public int CompanyId { get; set; }

        public GetCompanyInvoicesQuery(int companyId)
        {
            CompanyId = companyId;
        }
    }

    public class GetCompanyInvoicesQueryHandler : QueryHandler<GetCompanyInvoicesQuery, List<CompanyInvoiceListDto>>
    {
        public GetCompanyInvoicesQueryHandler(MyB2BContext context) : base(context)
        {
        }

        public override Result<List<CompanyInvoiceListDto>> Query(GetCompanyInvoicesQuery query)
        {
            var company = _context.Companies.First(c => c.Id == query.CompanyId);
            var companyInvoices = company.Invoices.Select(i => new CompanyInvoiceListDto
            {
                Id = i.Id,
                Number = i.Number,
                BuyerName = i.BuyerName,
                GeneratedAt = i.GeneratedAt,
                TotalGrossAmount = i.TotalGrossAmount
            }).ToList();

            return Result.Ok(companyInvoices);
        }
    }
}
