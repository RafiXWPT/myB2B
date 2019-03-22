using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Actions.Commands
{
    public abstract class CommandBase { }
    public abstract class OutputCommand<TOutput> : CommandBase
    {
        public Result<TOutput> Output { get; set; }
    }

    public abstract class Command : CommandBase
    {
        public Result Output { get; set; }
    }
}
