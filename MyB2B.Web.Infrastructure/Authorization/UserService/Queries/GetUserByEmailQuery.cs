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
    public class GetUserByEmailQuery : Query<ApplicationUser>
    {
        public string Email { get; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }

    public class GetUserByEmailQueryHandler : QueryHandler<GetUserByEmailQuery, ApplicationUser>
    {
        public GetUserByEmailQueryHandler(MyB2BContext context) : base(context)
        {
        }

        public override ActionResult<ApplicationUser> Query(GetUserByEmailQuery query)
        {
            return _context.Users.FirstOrDefault(u => u.Email == query.Email).WithAnyActionResult("There is no user with that email");
        }
    }
}
