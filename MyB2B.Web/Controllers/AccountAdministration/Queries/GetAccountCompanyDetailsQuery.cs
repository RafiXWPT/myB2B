using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyB2B.Domain.Companies;
using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Results;
using MyB2B.Web.Infrastructure.Actions;
using MyB2B.Web.Infrastructure.Actions.Queries;

namespace MyB2B.Web.Controllers.AccountAdministration.Queries
{
    public class GetAccountCompanyDetailsQuery : Query<AccountCompanyViewModel>
    {
        public int UserId { get; }

        public GetAccountCompanyDetailsQuery(int userId)
        {
            UserId = userId;
        }
    }

    public class GetAccountCompanyDetailsQueryHandler : QueryHandler<GetAccountCompanyDetailsQuery, AccountCompanyViewModel>
    {
        public GetAccountCompanyDetailsQueryHandler(MyB2BContext context) : base(context)
        {
        }

        public override Result<AccountCompanyViewModel> Query(GetAccountCompanyDetailsQuery query)
        {
            var account = _context.Users.Find(query.UserId);
            if (account == null)
            {
                return Result.Fail<AccountCompanyViewModel>("There is no user with that id.");
            }

            return Result.Ok(account.UserCompany == null ? new AccountCompanyViewModel() : new AccountCompanyViewModel
            {
                CompanyName = account.UserCompany.Name,
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
