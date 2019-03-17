using System;
using System.Collections.Generic;
using System.Text;
using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Results;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.ApplicationUsers.Services;

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
        private readonly IApplicationUserService _applicationUserService;

        public UpdateAccountCompanyDetailsCommandHandler(MyB2BContext context, IApplicationUserService applicationUserService) : base(context)
        {
            _applicationUserService = applicationUserService;
        }

        public override void Execute(UpdateAccountCompanyDetailsCommand command)
        {
            _applicationUserService.GetById(command.UserId)
                .OnSuccess(user => user.UserCompany)
                .OnSuccess(company => company.UpdateNameAndShortCode(command.CompanyName, command.ShortCode))
                .OnSuccess(company => company.UpdateNipAndRegon(command.CompanyNip, command.CompanyRegon))
                .OnSuccess(company => company.UpdateAddress(command.Country, command.City, command.ZipCode, command.Street, command.Number))
                .OnSuccess(_ => _context.SaveChanges())
                .OnFailure(err => command.Output = Result.Fail(err));
        }
    }
}
