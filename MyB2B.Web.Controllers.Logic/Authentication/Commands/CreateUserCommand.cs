using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.EntityFramework.Extensions;
using MyB2B.Domain.Identity;
using MyB2B.Domain.Results;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Commands.Extensions;

namespace MyB2B.Web.Controllers.Logic.Authentication.Commands
{
    public class CreateUserCommand : OutputCommand<ApplicationUser>
    {
        public string Username { get; }
        public byte[] Hash { get; }
        public byte[] Salt { get; }
        public string Email { get; }

        public CreateUserCommand(string username, byte[] hash, byte[] salt, string email)
        {
            Username = username;
            Hash = hash;
            Salt = salt;
            Email = email;
        }
    }

    public class CreateUserCommandHandler : CommandHandler<CreateUserCommand>
    {
        public CreateUserCommandHandler(MyB2BContext context) : base(context)
        {
        }

        public override void Execute(CreateUserCommand command)
        {
            ApplicationUser.Create(command.Username, command.Hash, command.Salt, command.Email)
                .OnSuccess(user => { _context.Users.Add(user); })
                .SaveContext(_context)
                .FinishCommand(command);
        }
    }
}
