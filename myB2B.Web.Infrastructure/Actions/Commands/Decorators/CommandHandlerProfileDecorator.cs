using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace myB2B.Web.Infrastructure.Actions.Commands.Decorators
{
    public class CommandhandlerProfileDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : Command
    {
        private readonly ICommandHandler<TCommand> _inner;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public CommandhandlerProfileDecorator(ICommandHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public void Execute(TCommand command)
        {
            _stopwatch.Start();

            _inner.Execute(command);

            _stopwatch.Stop();

            //_logger.Trace($"Czas: {sw.ElapsedMilliseconds}ms");
        }

        public async Task ExecuteAsync(TCommand command)
        {
            _stopwatch.Start();

            await _inner.ExecuteAsync(command);

            _stopwatch.Stop();
        }
    }
}
