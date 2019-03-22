using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Actions.Commands.Extensions
{
    public static class CommandResultExtensions
    {
        public static void FinishCommand<TValue>(this Result<TValue> result, Command command)
        {
            if (result.IsOk)
            {
                command.Output = Result.Ok(result.Value);
            }
            else if (result.IsFail)
            {
                command.Output = Result.Fail(result.Error);
            }
        }

        public static void FinishCommand<TValue>(this Result<TValue> result, OutputCommand<TValue> command)
        {
            if (result.IsOk)
            {
                command.Output = Result.Ok(result.Value);
            }
            else if (result.IsFail)
            {
                command.Output = Result.Fail<TValue>(result.Error);
            }
        }
    }
}
