using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.EntityFramework.Extensions;
using MyB2B.Domain.Results;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Commands.Extensions;
using System.Linq;

namespace MyB2B.Web.Controllers.Logic.AccountAdministration.Commands
{
    public class UpdateAccountCompanyDetailsCommand : Command
    {
        public int UserId { get; }
        public string CompanyName { get; set; }
        public string ShortCode { get; set; }
        public string CompanyNip { get; set; }
        public string CompanyRegon { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }

        public UpdateAccountCompanyDetailsCommand(int userId, string companyName, string shortCode, string companyNip, string companyRegon, string country, string city, string zipCode, string street, string number)
        {
            UserId = userId;
            CompanyName = companyName;
            ShortCode = shortCode;
            CompanyNip = companyNip;
            CompanyRegon = companyRegon;
            Country = country;
            City = city;
            ZipCode = zipCode;
            Street = street;
            Number = number;
        }      
    }

    public class UpdateAccountCompanyDetailsCommandHandler : CommandHandler<UpdateAccountCompanyDetailsCommand>
    {
        public UpdateAccountCompanyDetailsCommandHandler(MyB2BContext context) : base(context)
        {
        }

        public override void Execute(UpdateAccountCompanyDetailsCommand command)
        {
            var dbUser = _context.Users.Find(command.UserId);

            if (dbUser == null)
                return;

            Result.Ok(dbUser)
                .OnSuccess(user => user.UserCompany)
                .OnSuccess(company => company.UpdateNameAndShortCode(command.CompanyName, command.ShortCode))
                .OnSuccess(company => company.UpdateNipAndRegon(command.CompanyNip, command.CompanyRegon))
                .OnSuccess(company => company.UpdateAddress(command.Country, command.City, command.ZipCode, command.Street, command.Number))
                .SaveContext(_context)
                .FinishCommand(command);
        }
    }
}
