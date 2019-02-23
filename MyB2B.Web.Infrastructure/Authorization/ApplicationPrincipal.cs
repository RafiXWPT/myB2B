using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyB2B.Web.Infrastructure.Authorization.UserService;
using MyB2B.Web.Infrastructure.Dependency;
using SimpleInjector;

namespace MyB2B.Web.Infrastructure.Authorization
{
    public class ApplicationPrincipal : ClaimsPrincipal
    {
        public ApplicationPrincipal(IPrincipal principal) :base(principal) { }
        public ApplicationPrincipal(IIdentity identity) : base(identity) { }

        public int UserId => Convert.ToInt32(GetClaimValueOrDefault(ApplicationClaimType.UserId, "0"));
        public string FirstName => GetClaimValueOrDefault(ApplicationClaimType.UserFirstName, "-");
        public string LastName => GetClaimValueOrDefault(ApplicationClaimType.UserLastName, "-");
        public bool IsConfirmed => Convert.ToBoolean(GetClaimValueOrDefault(ApplicationClaimType.UserIsConfirmed));

        private string GetClaimValueOrDefault(string claimType, string defaultValue = null)
        {
            var claim = FindFirst(claimType);
            return claim != null ? claim.Value : defaultValue ?? string.Empty;
        }

        public SecurityToken ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityTokenSecret = DependencyContainer.Container.GetInstance<IConfiguration>().GetValue<string>("Security:Token:Secret");
            var validation = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityTokenSecret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            tokenHandler.ValidateToken(token, validation, out SecurityToken tokenSource);
            return tokenSource;
        }
    }
}
