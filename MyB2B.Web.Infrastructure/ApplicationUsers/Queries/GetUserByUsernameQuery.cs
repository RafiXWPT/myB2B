using System.Linq;
using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Identity;
using MyB2B.Domain.Results;
using MyB2B.Web.Infrastructure.Actions.Queries;

namespace MyB2B.Web.Infrastructure.ApplicationUsers.Queries
{
    public class GetUserByUsernameQuery : Query<ApplicationUser>
    {
        public string Username { get; }

        public GetUserByUsernameQuery(string username)
        {
            Username = username;
        }
    }

    public class GetUserByUsernameQueryHandler : QueryHandler<GetUserByUsernameQuery, ApplicationUser>
    {
        public GetUserByUsernameQueryHandler(MyB2BContext context) : base(context)
        {
        }

        public override Result<ApplicationUser> Query(GetUserByUsernameQuery query)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == query.Username);
            return user == null ? Result.Fail<ApplicationUser>("There is no user with that username") : Result.Ok(user);
        }
    }
}
