using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace myB2B.Web.Infrastructure.Actions.Commands.Decorators
{
    public class CommandHandlerLogDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : Command
    {
        private readonly ICommandHandler<TCommand> _inner;

        public CommandHandlerLogDecorator(ICommandHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public void Execute(TCommand command)
        {
            LogCommand(command);

            _inner.Execute(command);
        }

        public async Task ExecuteAsync(TCommand command)
        {
            LogCommand(command);

            await _inner.ExecuteAsync(command);
        }

        protected void LogCommand(TCommand command)
        {

        }
    }
}
