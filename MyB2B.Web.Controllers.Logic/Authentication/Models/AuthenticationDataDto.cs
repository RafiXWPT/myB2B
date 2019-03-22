using System;

namespace MyB2B.Web.Controllers.Logic.Authentication.Models
{
    public class AuthenticationDataDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}