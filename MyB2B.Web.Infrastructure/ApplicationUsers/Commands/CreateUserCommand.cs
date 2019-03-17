using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Identity;
using MyB2B.Domain.Results;
using MyB2B.Web.Infrastructure.Actions.Commands;

namespace MyB2B.Web.Infrastructure.ApplicationUsers.Commands
{
    public class CreateUserCommand : Command<ApplicationUser>
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
            var user = ApplicationUser.Create(command.Username, command.Hash, command.Salt, command.Email);
            _context.Users.Add(user);
            _context.SaveChanges();
            command.Output = Result.Ok(user);
        }
    }
}
