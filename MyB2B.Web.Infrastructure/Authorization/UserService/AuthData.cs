using System;

namespace MyB2B.Web.Infrastructure.Authorization.UserService
{
    public class AuthData
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}