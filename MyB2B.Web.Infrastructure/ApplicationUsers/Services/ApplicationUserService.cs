using System;
using System.Collections.Generic;
using System.Text;
using MyB2B.Domain.Identity;
using MyB2B.Domain.Results;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Queries;
using MyB2B.Web.Infrastructure.ApplicationUsers.Commands;
using MyB2B.Web.Infrastructure.ApplicationUsers.Queries;

namespace MyB2B.Web.Infrastructure.ApplicationUsers.Services
{
    public interface IApplicationUserService
    {
        Result<ApplicationUser> Create(string username, byte[] passwordHash, byte[] passwordSalt, string email);
        Result<ApplicationUser> GetById(int id);
        Result<ApplicationUser> GetByUsername(string username);
        Result<ApplicationUser> GetByEmail(string email);
        Result<ApplicationUser> Update(ApplicationUser user);
        Result<ApplicationUser> Delete(ApplicationUser user);
    }

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public ApplicationUserService(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcessor;
        }

        public Result<ApplicationUser> Create(string username, byte[] passwordHash, byte[] passwordSalt, string email)
        {
            var command = new CreateUserCommand(username, passwordHash, passwordSalt, email);
            _commandProcessor.Execute(command);
            return command.Output;
        }

        public Result<ApplicationUser> GetById(int id)
        {
            return _queryProcessor.Query(new GetUserByIdQuery(id));
        }

        public Result<ApplicationUser> GetByUsername(string username)
        {
            return _queryProcessor.Query(new GetUserByUsernameQuery(username));
        }

        public Result<ApplicationUser> GetByEmail(string email)
        {
            return _queryProcessor.Query(new GetUserByEmailQuery(email));
        }

        public Result<ApplicationUser> Update(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Result<ApplicationUser> Delete(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
