using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Domain.Results;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Queries;
using MyB2B.Web.Infrastructure.Authorization;
using MyB2B.Web.Infrastructure.Authorization.UserService;

namespace MyB2B.Web.Controllers
{
    public class LoginDataDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDataDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class TestPostDataDto
    {
        public string Data { get; set; }
    }

    public abstract class PrincipalController : Controller
    {
        public new ApplicationPrincipal User => new ApplicationPrincipal(base.User);
    }

    public abstract class BaseController : PrincipalController
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;

        protected BaseController(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcessor;
        }

        protected JsonResult GetJsonResult<T>(Result<T> result)
        {
            return result.IsFail
                ? Json(new {success = false, errorMessage = result.Error})
                : Json(new {success = true, result = result.Value});
        }
    }

    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor, IUserService userService) : base(commandProcessor, queryProcessor)
        {
            _userService = userService;
        }

        [HttpGet("refresh-token")]
        public IActionResult RefreshToken()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
            if (token == null)
                return Json("");

            var userToken = User.ValidateToken(token);
            var expirationTime = userToken.ValidTo - DateTime.UtcNow;
            if (expirationTime.TotalMinutes < 15)
            {
                return Json(new {ShouldRefresh = true, AuthData = _userService.RefreshToken(User.UserId).Value });
            }

            return Json(new {ShouldRefresh = false});
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDataDto loginData)
        {
            return GetJsonResult(_userService.Authenticate(loginData.Username, loginData.Password));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDataDto registerData)
        {
            return GetJsonResult(_userService.Register(registerData.Username, registerData.Email, registerData.Password, registerData.ConfirmPassword));
        }

        [HttpGet("get-test-token")]
        public IActionResult GetTestToken()
        {
            return Json(new { Status = "OK" });
        }

        [HttpPost("post-test-token")]
        public IActionResult PostTestToken([FromBody] TestPostDataDto data)
        {
            return Json(new { Status = "OK" });
        }
    }
}
