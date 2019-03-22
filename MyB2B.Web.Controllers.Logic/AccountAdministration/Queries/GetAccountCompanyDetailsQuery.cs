using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Results;
using MyB2B.Web.Controllers.Logic.AccountAdministration.Models;
using MyB2B.Web.Infrastructure.Actions.Queries;

namespace MyB2B.Web.Controllers.Logic.AccountAdministration.Queries
{
    public class GetAccountCompanyDetailsQuery : Query<AccountCompanyDataDto>
    {
        public int UserId { get; }

        public GetAccountCompanyDetailsQuery(int userId)
        {
            UserId = userId;
        }
    }

    public class GetAccountCompanyDetailsQueryHandler : QueryHandler<GetAccountCompanyDetailsQuery, AccountCompanyDataDto>
    {
        public GetAccountCompanyDetailsQueryHandler(MyB2BContext context) : base(context)
        {
        }

        public override Result<AccountCompanyDataDto> Query(GetAccountCompanyDetailsQuery query)
        {
            var account = _context.Users.Find(query.UserId);
            if (account == null)
            {
                return Result.Fail<AccountCompanyDataDto>("There is no user with that id.");
            }

            return Result.Ok(account.UserCompany == null ? new AccountCompanyDataDto() : new AccountCompanyDataDto
            {
                CompanyName = account.UserCompany.Name,
                ShortCode = account.UserCompany.ShortCode,
                CompanyNip = account.UserCompany.Nip,
                CompanyRegon = account.UserCompany.Regon,
                Country = account.UserCompany.Address.Country,
                City = account.UserCompany.Address.City,
                ZipCode = account.UserCompany.Address.ZipCode,
                Street = account.UserCompany.Address.Street,
                Number = account.UserCompany.Address.Number
            });
        }
    }
}
