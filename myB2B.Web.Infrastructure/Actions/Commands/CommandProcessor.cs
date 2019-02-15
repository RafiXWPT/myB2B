using System;
using System.Threading.Tasks;

namespace myB2B.Web.Infrastructure.Actions.Commands
{
    public interface ICommandProcessor
    {
        void Execute<TCommand>(TCommand command) where TCommand: Command;
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : Command;
    }

    public class CommandProcessor : ICommandProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Execute<TCommand>(TCommand command) where TCommand : Command
        {
            var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;
            if (handler == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand), false);
            }

            handler.Execute(command);
        }

        public async Task ExecuteAsync<TCommand>(TCommand command) where TCommand : Command
        {
            var handler = _serviceProvider.GetService(typeof(IAsyncCommandHandler<TCommand>)) as IAsyncCommandHandler<TCommand>;
            if (handler == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand), true);
            }

            await handler.ExecuteAsync(command);
        }
    }
}
