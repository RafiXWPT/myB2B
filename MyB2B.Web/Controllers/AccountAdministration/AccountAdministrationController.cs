using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Domain.Results;
using MyB2B.Web.Controllers.Logic.AccountAdministration.Models;
using MyB2B.Web.Infrastructure.Controllers;

namespace MyB2B.Web.Controllers.AccountAdministration
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountAdministrationController : BaseController
    {
        [HttpPost("update-company")]
        public IActionResult UpdateCompany([FromBody] AccountCompanyDataDto dataDto)
        {
            return GetJsonResult(Result.Ok(new {Status = "OK"}));
        }
    }
}
