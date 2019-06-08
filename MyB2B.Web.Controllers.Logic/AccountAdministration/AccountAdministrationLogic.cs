using System;
using System.Collections.Generic;
using System.Text;
using MyB2B.Domain.Results;
using MyB2B.Web.Controllers.Logic.AccountAdministration.Commands;
using MyB2B.Web.Controllers.Logic.AccountAdministration.Models;
using MyB2B.Web.Controllers.Logic.AccountAdministration.Queries;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Commands.Extensions;
using MyB2B.Web.Infrastructure.Actions.Queries;

namespace MyB2B.Web.Controllers.Logic.AccountAdministration
{
    public class AccountAdministrationLogic : ControllerLogic
    {
        public AccountAdministrationLogic(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor) : base(commandProcessor, queryProcessor)
        {
        }

        public Result<AccountCompanyDataDto> GetAccountCompanyData(int userId)
        {
            return QueryProcessor.Query(new GetAccountCompanyDetailsQuery(userId));
        }

        public Result UpdateCompanyData(int userId, AccountCompanyDataDto dataDto)
        {
            return CommandProcessor.Execute(new UpdateAccountCompanyDetailsCommand(userId,
                dataDto.CompanyName,
                dataDto.ShortCode,
                dataDto.CompanyNip,
                dataDto.CompanyRegon,
                dataDto.Country,
                dataDto.City,
                dataDto.ZipCode,
                dataDto.Street,
                dataDto.Number))
                .GetCommandResult();
        }

        public Result UpdateProfileData(AccountProfileDataDto dataDto)
        {
            return Result.Ok();
        }

        public Result UpdateSecurityData(AccountSecurityDataDto dataDto)
        {
            return Result.Ok();
        }
    }
}
