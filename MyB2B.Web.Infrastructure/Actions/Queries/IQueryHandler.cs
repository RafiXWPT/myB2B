using System;
using System.Collections.Generic;
using System.Text;

namespace myB2B.Web.Infrastructure.Actions.Queries
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : Query<TResult> 
        where TResult: class
    {
        ActionResult<TResult> Query(TQuery query);
    }
}
