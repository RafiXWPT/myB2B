using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Actions.Commands.Extensions
{
    public static class CommandExtensions
    {
        public static Result<TCommandResult> GetCommandResult<TCommandResult>(this OutputCommand<TCommandResult> command) 
            => command.Output;

        public static Result GetCommandResult(this Command command)
            => command.Output;
    }
}
