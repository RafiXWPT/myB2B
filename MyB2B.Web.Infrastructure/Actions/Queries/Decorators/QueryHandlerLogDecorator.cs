using System.Diagnostics;
using System.Threading.Tasks;
using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Actions.Queries.Decorators
{
    public class QueryHandlerLogDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> 
        where TQuery : Query<TResult>
        where TResult: class
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratedHandler;

        public QueryHandlerLogDecorator(IQueryHandler<TQuery, TResult> decoratedHandler)
        {
            _decoratedHandler = decoratedHandler;
        }

        [DebuggerStepThrough]
        public Result<TResult> Query(TQuery query)
        {
            LogQuery(query);

            return _decoratedHandler.Query(query);
        }

        protected void LogQuery(TQuery query)
        {

        }
    }

    public class AsyncQueryHandlerLogDecorator<TQuery, TResult> : IAsyncQueryHandler<TQuery, TResult>
        where TQuery : Query<TResult>
        where TResult : class
    {
        private readonly IAsyncQueryHandler<TQuery, TResult> _decoratedHandler;

        public AsyncQueryHandlerLogDecorator(IAsyncQueryHandler<TQuery, TResult> decoratedHandler)
        {
            _decoratedHandler = decoratedHandler;
        }

        [DebuggerStepThrough]
        public async Task<Result<TResult>> QueryAsync(TQuery query)
        {
            LogQuery(query);

            return await _decoratedHandler.QueryAsync(query);
        }

        protected void LogQuery(TQuery query)
        {

        }
    }
}
