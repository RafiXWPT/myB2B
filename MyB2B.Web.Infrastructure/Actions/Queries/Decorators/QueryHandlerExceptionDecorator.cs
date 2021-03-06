﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Actions.Queries.Decorators
{
    public class QueryHandlerExceptionDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : Query<TResult>
        where TResult: class
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratedHandler;

        public QueryHandlerExceptionDecorator(IQueryHandler<TQuery, TResult> decoratedHandler)
        {
            _decoratedHandler = decoratedHandler;
        }

        [DebuggerStepThrough]
        public Result<TResult> Query(TQuery query)
        {
            try
            {
                return _decoratedHandler.Query(query);
            }
            catch (Exception ex)
            {
                LogQueryException(query, ex);
                throw;
            }
        }

        protected void LogQueryException(TQuery query, Exception exception)
        {

        }
    }

    public class AsyncQueryHandlerExceptionDecorator<TQuery, TResult> : IAsyncQueryHandler<TQuery, TResult>
        where TQuery : Query<TResult>
        where TResult : class
    {
        private readonly IAsyncQueryHandler<TQuery, TResult> _decoratedHandler;

        public AsyncQueryHandlerExceptionDecorator(IAsyncQueryHandler<TQuery, TResult> decoratedHandler)
        {
            _decoratedHandler = decoratedHandler;
        }

        [DebuggerStepThrough]
        public async Task<Result<TResult>> QueryAsync(TQuery query)
        {
            try
            {
                return await _decoratedHandler.QueryAsync(query);
            }
            catch (Exception ex)
            {
                LogQueryException(query, ex);
                throw;
            }
        }

        protected void LogQueryException(TQuery query, Exception exception)
        {

        }
    }
}
