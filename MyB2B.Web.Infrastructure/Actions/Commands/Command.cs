using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Actions.Commands
{
    public abstract class Command { }
    public abstract class Command<TOutput> : Command
    {
        public Result<TOutput> Output { get; set; } = Result.Ok<TOutput>(default);
    }
}
