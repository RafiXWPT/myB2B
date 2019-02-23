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
    public class GetUserByIdQuery : Query<ApplicationUser>
    {
        public int UserId { get; }

        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }

    public class GetUserByIdQueryHandler : QueryHandler<GetUserByIdQuery, ApplicationUser>
    {
        public GetUserByIdQueryHandler(MyB2BContext context) : base(context)
        {
        }

        public override ActionResult<ApplicationUser> Query(GetUserByIdQuery query)
        {
            return _context.Users.FirstOrDefault(u => u.Id == query.UserId).WithAnyActionResult();
        }
    }
}
