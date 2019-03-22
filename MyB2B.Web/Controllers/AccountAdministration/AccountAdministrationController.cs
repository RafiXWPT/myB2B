using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Domain.Results;
using MyB2B.Web.Controllers.Logic.AccountAdministration;
using MyB2B.Web.Controllers.Logic.AccountAdministration.Models;
using MyB2B.Web.Infrastructure.Controllers;

namespace MyB2B.Web.Controllers.AccountAdministration
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountAdministrationController : BaseController
    {
        private readonly AccountAdministrationControllerLogic _controllerLogic;

        public AccountAdministrationController(AccountAdministrationControllerLogic controllerLogic)
        {
            _controllerLogic = controllerLogic;
        }

        [HttpGet("get-user-company")]
        public IActionResult GetUserCompany()
        {
            return GetJsonResult(_controllerLogic.GetAccountCompanyData(User.UserId));
        }

        [HttpPost("update-company")]
        public IActionResult UpdateCompany([FromBody] AccountCompanyDataDto dataDto)
        {
            return GetJsonResult(_controllerLogic.UpdateCompanyData(User.UserId, dataDto));
        }
    }
}
