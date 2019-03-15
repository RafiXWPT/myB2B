using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Actions.Commands
{
    public abstract class Command
    {
        public Result<object> Output { get; set; } = Result.Ok(new object());
    }
}
