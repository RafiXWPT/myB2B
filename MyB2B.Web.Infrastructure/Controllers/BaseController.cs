using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Controllers
{
    public abstract class BaseController : PrincipalController
    {
        protected JsonResult GetJsonResult(Result result)
        {
            return result.IsFail
                ? Json(new { success = false, errorMessage = result.Error })
                : Json(new { success = true });
        }

        protected JsonResult GetJsonResult<T>(Result<T> result)
        {
            return result.IsFail
                ? Json(new {success = false, errorMessage = result.Error})
                : Json(new {success = true, data = result.Value});
        }
    }
}