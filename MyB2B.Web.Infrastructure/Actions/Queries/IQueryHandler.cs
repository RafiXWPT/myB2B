using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Actions.Queries
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : Query<TResult> 
        where TResult: class
    {
        Result<TResult> Query(TQuery query);
    }
}
