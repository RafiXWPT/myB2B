using System;
using System.Collections.Generic;
using System.Text;
using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Actions.Queries
{
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : Query<TResult>
        where TResult : class
    {
        protected readonly MyB2BContext _context;

        protected QueryHandler(MyB2BContext context)
        {
            _context = context;
        }

        public abstract Result<TResult> Query(TQuery query);
    }
}
