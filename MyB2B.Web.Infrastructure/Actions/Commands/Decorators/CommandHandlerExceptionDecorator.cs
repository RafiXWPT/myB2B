using System;
using System.Threading.Tasks;

namespace MyB2B.Web.Infrastructure.Actions.Commands.Decorators
{
    public class CommandHandlerExceptionDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : Command
    {
        private readonly ICommandHandler<TCommand> _inner;

        public CommandHandlerExceptionDecorator(ICommandHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public void Execute(TCommand command)
        {
            try
            {
                _inner.Execute(command);
            }
            catch (Exception ex)
            {
                LogCommandException(command, ex);
                throw;
            }
        }

        protected void LogCommandException(TCommand command, Exception exception)
        {

        }
    }

    public class AsyncCommandHandlerExceptionDecorator<TCommand> : IAsyncCommandHandler<TCommand> where TCommand : Command
    {
        private readonly IAsyncCommandHandler<TCommand> _inner;

        public AsyncCommandHandlerExceptionDecorator(IAsyncCommandHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public async Task ExecuteAsync(TCommand command)
        {
            try
            {
                await _inner.ExecuteAsync(command);
            }
            catch (Exception ex)
            {
                LogCommandException(command, ex);
                throw;
            }
        }

        protected void LogCommandException(TCommand command, Exception exception)
        {

        }
    }
}