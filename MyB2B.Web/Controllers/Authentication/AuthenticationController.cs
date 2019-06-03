using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyB2B.Web.Controllers.Logic;
using MyB2B.Web.Controllers.Logic.Authentication;
using MyB2B.Web.Controllers.Logic.Authentication.Models;
using MyB2B.Web.Infrastructure.ApplicationUsers;
using MyB2B.Web.Infrastructure.Controllers;

namespace MyB2B.Web.Controllers.Authentication
{
    public class TestPostDataDto
    {
        public string Data { get; set; }
    }

    [Authorize]
    [Route("api/[controller]")]
    public class AuthenticationController : LogicController<AuthenticationControllerLogic>
    {
        public AuthenticationController(AuthenticationControllerLogic logic) : base(logic)
        {
        }

        [HttpGet("refresh-token")]
        public IActionResult RefreshToken()
        {
            var token = RequestToken;
            if (token == null)
                return Json("");

            try
            {
                var userToken = User.ValidateToken(token, CurrentUserEndpointAddress);

                var expirationTime = userToken.ValidTo - DateTime.UtcNow;
                if (expirationTime.TotalMinutes < 15)
                {
                    return Json(new { ShouldRefresh = true, AuthData = Logic.RefreshToken(User.UserId, CurrentUserEndpointAddress).Value });
                }

                return Json(new { ShouldRefresh = false });
            }
            catch (UserEndpointMismatchException)
            {
                return Json(new { ForceTokenInvalidate = true });
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDataDto loginData)
        {
            return GetJsonResult(Logic.Authenticate(loginData.Username, loginData.Password, CurrentUserEndpointAddress));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDataDto registerData)
        {
            return GetJsonResult(Logic.Register(registerData.Username, registerData.Email, registerData.Password, registerData.ConfirmPassword, CurrentUserEndpointAddress));
        }

        [HttpGet("get-test-token")]
        public IActionResult GetTestToken()
        {
            return Json(new {Status = "OK"});
        }

        [HttpPost("post-test-token")]
        public IActionResult PostTestToken([FromBody] TestPostDataDto data)
        {
            return Json(new { Status = "OK" });
        }
    }
}
