using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace myB2B.Web.Infrastructure.Actions.Queries
{
    public interface IQueryProcessor
    {
        TResult Query<TResult>(Query<TResult> query);

        Task<TResult> QueryAsync<TResult>(Query<TResult> query);
    }

    public class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResult Query<TResult>(Query<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                throw new QueryHandlerNotFoundException(handlerType, false);
            }
            return handler.Query((dynamic)query);
        }

        public async Task<TResult> QueryAsync<TResult>(Query<TResult> query)
        {
            var handlerType = typeof(IAsyncQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                throw new QueryHandlerNotFoundException(handlerType, true);
            }
            return await handler.QueryAsync((dynamic)query);
        }
    }
}
