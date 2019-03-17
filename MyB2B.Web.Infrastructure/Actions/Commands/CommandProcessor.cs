using System;
using System.Threading.Tasks;

namespace MyB2B.Web.Infrastructure.Actions.Commands
{
    public interface ICommandProcessor
    {
        void Execute<TCommand>(TCommand command) where TCommand: CommandBase;
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : CommandBase;
    }

    public class CommandProcessor : ICommandProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Execute<TCommand>(TCommand command) where TCommand : CommandBase
        {
            var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;
            if (handler == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand), false);
            }

            handler.Execute(command);
        }

        public async Task ExecuteAsync<TCommand>(TCommand command) where TCommand : CommandBase
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
