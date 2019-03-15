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

        public override Result<ApplicationUser> Query(GetUserByEmailQuery query)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == query.Email);
            return user != null ? Result.Ok(user) : Result.Fail<ApplicationUser>("There is no user with that email");
        }
    }
}
