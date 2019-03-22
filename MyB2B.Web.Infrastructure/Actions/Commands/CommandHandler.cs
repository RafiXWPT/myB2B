using MyB2B.Domain.EntityFramework;

namespace MyB2B.Web.Infrastructure.Actions.Commands
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : CommandBase
    {
        protected readonly MyB2BContext _context;

        protected CommandHandler(MyB2BContext context)
        {
            _context = context;
        }

        public abstract void Execute(TCommand command);
    }
}