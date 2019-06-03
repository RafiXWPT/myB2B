using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Web.Controllers.Logic;
using MyB2B.Web.Controllers.Logic.AccountAdministration;
using MyB2B.Web.Controllers.Logic.AccountAdministration.Models;

namespace MyB2B.Web.Controllers.AccountAdministration
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountAdministrationController : LogicController<AccountAdministrationControllerLogic>
    {
        public AccountAdministrationController(AccountAdministrationControllerLogic logic) : base(logic)
        {
        }

        [HttpGet("get-user-company")]
        public IActionResult GetUserCompany()
        {
            return GetJsonResult(Logic.GetAccountCompanyData(User.UserId));
        }

        [HttpPost("update-company")]
        public IActionResult UpdateCompany([FromBody] AccountCompanyDataDto dataDto)
        {
            return GetJsonResult(Logic.UpdateCompanyData(User.UserId, dataDto));
        }
    }
}
