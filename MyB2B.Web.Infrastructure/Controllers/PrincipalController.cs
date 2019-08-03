using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Web.Infrastructure.ApplicationUsers;
using MyB2B.Web.Infrastructure.Dependency;

namespace MyB2B.Web.Infrastructure.Controllers
{
    public abstract class PrincipalController : Controller
    {
        protected string CurrentUserEndpointAddress => ControllerContext?.HttpContext?.Request?.Host.Value;
        protected string RequestToken => HttpContext.GetAuthorizationTokenFromRequest();
        public new ApplicationPrincipal User => DependencyContainer.Container.GetInstance<IHttpContextAccessor>().HttpContext.ExtractApplicationPrincipal();
    }
}