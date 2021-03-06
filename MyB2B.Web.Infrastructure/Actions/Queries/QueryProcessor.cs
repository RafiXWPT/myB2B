﻿using System;
using System.Threading.Tasks;
using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Actions.Queries
{
    public interface IQueryProcessor
    {
        Result<TResult> Query<TResult>(Query<TResult> query) where TResult: class;

        Task<Result<TResult>> QueryAsync<TResult>(Query<TResult> query) where TResult: class;
    }

    public class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Result<TResult> Query<TResult>(Query<TResult> query) where TResult: class
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                throw new QueryHandlerNotFoundException(handlerType, false);
            }
            return handler.Query((dynamic)query);
        }

        public async Task<Result<TResult>> QueryAsync<TResult>(Query<TResult> query) where TResult : class
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
