using System.Diagnostics;
using System.Threading.Tasks;

namespace MyB2B.Web.Infrastructure.Actions.Commands.Decorators
{
    public class CommandHandlerProfileDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : CommandBase
    {
        private readonly ICommandHandler<TCommand> _inner;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public CommandHandlerProfileDecorator(ICommandHandler<TCommand> inner)
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
    }

    public class AsyncCommandHandlerProfileDecorator<TCommand> : IAsyncCommandHandler<TCommand> where TCommand : CommandBase
    {
        private readonly IAsyncCommandHandler<TCommand> _inner;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public AsyncCommandHandlerProfileDecorator(IAsyncCommandHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public async Task ExecuteAsync(TCommand command)
        {
            _stopwatch.Start();

            await _inner.ExecuteAsync(command);

            _stopwatch.Stop();
        }
    }
}
