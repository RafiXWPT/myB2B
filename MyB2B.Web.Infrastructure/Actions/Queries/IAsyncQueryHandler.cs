using System.Threading.Tasks;
using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Actions.Queries
{
    public interface IAsyncQueryHandler<in TQuery, TResult>
        where TQuery : Query<TResult>
        where TResult : class
    {
        Task<Result<TResult>> QueryAsync(TQuery query);
    }
}