using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Identity;
using MyB2B.Domain.Results;
using MyB2B.Web.Infrastructure.Actions;
using MyB2B.Web.Infrastructure.Actions.Queries;

namespace MyB2B.Web.Infrastructure.Authorization.UserService.Queries
{
    public class GetUserByNameQuery : Query<ApplicationUser>
    {
        public string Username { get; }

        public GetUserByNameQuery(string username)
        {
            Username = username;
        }
    }

    public class GetUserByNameQueryHandler : QueryHandler<GetUserByNameQuery, ApplicationUser>
    {
        public GetUserByNameQueryHandler(MyB2BContext context) : base(context)
        {
        }

        public override Result<ApplicationUser> Query(GetUserByNameQuery query)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == query.Username);
            return user == null ? Result.Fail<ApplicationUser>("There is no user with that username") : Result.Ok(user);
        }
    }
}
