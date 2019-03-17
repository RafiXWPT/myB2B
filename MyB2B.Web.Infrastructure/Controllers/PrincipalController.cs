using Microsoft.AspNetCore.Mvc;
using MyB2B.Web.Infrastructure.ApplicationUsers;

namespace MyB2B.Web.Infrastructure.Controllers
{
    public abstract class PrincipalController : Controller
    {
        public new ApplicationPrincipal User => new ApplicationPrincipal(base.User);
    }
}