using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Controllers
{
    public abstract class BaseController : PrincipalController
    {
        protected string CurrentUserEndpointAddress => ControllerContext?.HttpContext?.Request?.Host.Value;
        protected string RequestToken => Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

        protected JsonResult GetJsonResult<T>(Result<T> result)
        {
            return result.IsFail
                ? Json(new {success = false, errorMessage = result.Error})
                : Json(new {success = true, result = result.Value});
        }
    }
}