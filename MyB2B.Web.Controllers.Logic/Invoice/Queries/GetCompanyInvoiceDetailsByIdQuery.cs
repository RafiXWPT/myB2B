using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Results;
using MyB2B.Web.Controllers.Logic.Invoice.Models;
using MyB2B.Web.Infrastructure.Actions.Queries;
using System.Linq;

namespace MyB2B.Web.Controllers.Logic.Invoice.Queries
{
    public class GetCompanyInvoiceDetailsByIdQuery : Query<InvoiceDetailsDto>
    {
        public int InvoiceId { get; }
        public int UserCompanyId { get; }

        public GetCompanyInvoiceDetailsByIdQuery(int invoiceId, int userCompanyId)
        {
            InvoiceId = invoiceId;
            UserCompanyId = userCompanyId;
        }
    }

    public class GetCompanyInvoiceDetailsByIdQueryHandler : QueryHandler<GetCompanyInvoiceDetailsByIdQuery, InvoiceDetailsDto>
    {
        public GetCompanyInvoiceDetailsByIdQueryHandler(MyB2BContext context) : base(context)
        {
        }

        public override Result<InvoiceDetailsDto> Query(GetCompanyInvoiceDetailsByIdQuery query)
        {
            var userCompany = _context.Companies.First(c => c.Id == query.UserCompanyId);
            var invoice = userCompany.Invoices.FirstOrDefault(i => i.Id == query.InvoiceId);

            if(invoice == null)
            {
                return Result.Fail<InvoiceDetailsDto>("There is no invoice with that id in company.");
            }

            return Result.Ok(new InvoiceDetailsDto
            {
                Number = invoice.Number,
                DealerCompany = invoice.DealerCompany,
                DealerName = invoice.DealerName,
                DealerNip = invoice.DealerNip,
                BuyerCompany = invoice.BuyerCompany,
                BuyerName = invoice.BuyerName,
                BuyerNip = invoice.BuyerNip,
                GeneratedAt = invoice.GeneratedAt,
                PaymentMethod = invoice.PaymentMethod,
                PaymentToDate = invoice.PaymentToDate,
                Status = invoice.Status,
                TotalGrossAmount = invoice.TotalGrossAmount,
                Items = invoice.Items.Select(i => new InvoiceItemDto
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    TotalGrossAmount = i.TotalGrossAmount,
                    Discount = i.ProductDiscount
                }).ToList()
            });
        }
    }
}
