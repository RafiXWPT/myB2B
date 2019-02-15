using System.Threading.Tasks;

namespace myB2B.Web.Infrastructure.Actions.Queries
{
    public interface IAsyncQueryHandler<in TQuery, TResult>
        where TQuery : Query<TResult>
        where TResult : class
    {
        Task<ActionResult<TResult>> QueryAsync(TQuery query);
    }
}