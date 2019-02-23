using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Identity;
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

        public override ActionResult<ApplicationUser> Query(GetUserByNameQuery query)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == query.Username);
            if(user == null)
                return ActionResult<ApplicationUser>.Fail("There is no user with that username.");

            return ActionResult<ApplicationUser>.Done(user);
        }
    }
}
