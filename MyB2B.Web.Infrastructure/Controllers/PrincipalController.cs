using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Web.Infrastructure.ApplicationUsers;

namespace MyB2B.Web.Infrastructure.Controllers
{
    public abstract class PrincipalController : Controller
    {
        protected string CurrentUserEndpointAddress => ControllerContext?.HttpContext?.Request?.Host.Value;
        protected string RequestToken => HttpContext.GetAuthorizationTokenFromRequest();
        public new ApplicationPrincipal User => HttpContext.ExtractApplicationPrincipal();
    }
}