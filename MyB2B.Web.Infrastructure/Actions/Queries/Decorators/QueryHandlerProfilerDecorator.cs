using System.Diagnostics;
using System.Threading.Tasks;

namespace MyB2B.Web.Infrastructure.Actions.Queries.Decorators
{
    public class QueryHandlerProfilerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> 
        where TQuery : Query<TResult>
        where TResult: class
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratedHandler;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public QueryHandlerProfilerDecorator(IQueryHandler<TQuery, TResult> decoratedHandler)
        {
            _decoratedHandler = decoratedHandler;
        }

        public ActionResult<TResult> Query(TQuery query)
        {
            _stopwatch.Start();

            var result = _decoratedHandler.Query(query);

            _stopwatch.Stop();

            //_logger.Trace($"Czas: {sw.ElapsedMilliseconds}ms");

            return result;
        }
    }

    public class AsyncQueryHandlerProfilerDecorator<TQuery, TResult> : IAsyncQueryHandler<TQuery, TResult>
        where TQuery : Query<TResult>
        where TResult : class
    {
        private readonly IAsyncQueryHandler<TQuery, TResult> _decoratedHandler;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public AsyncQueryHandlerProfilerDecorator(IAsyncQueryHandler<TQuery, TResult> decoratedHandler)
        {
            _decoratedHandler = decoratedHandler;
        }

        public async Task<ActionResult<TResult>> QueryAsync(TQuery query)
        {
            _stopwatch.Start();

            var result = await _decoratedHandler.QueryAsync(query);

            _stopwatch.Stop();

            //_logger.Trace($"Czas: {sw.ElapsedMilliseconds}ms");

            return result;
        }
    }
}
