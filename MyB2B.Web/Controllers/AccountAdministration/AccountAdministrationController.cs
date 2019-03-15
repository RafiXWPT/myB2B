using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Domain.Results;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Queries;

namespace MyB2B.Web.Controllers.AccountAdministration
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountAdministrationController : BaseController
    {
        public AccountAdministrationController(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor) : base(commandProcessor, queryProcessor)
        {

        }

        [HttpPost("update-company")]
        public IActionResult UpdateCompany([FromBody] AccountCompanyViewModel viewModel)
        {
            return GetJsonResult(Result.Ok(new {Status = "OK"}));
        }
    }
}
