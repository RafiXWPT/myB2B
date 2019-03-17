using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyB2B.Server.Common;
using MyB2B.Web.Infrastructure.Dependency;

namespace MyB2B.Web.Infrastructure.ApplicationUsers
{
    public class UserEndpointMismatchException : SecurityTokenValidationException
    {
        public UserEndpointMismatchException() : base("Endpoint saved in token mismatch current user address.") { }
    }

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

        public SecurityToken ValidateToken(string token, string userEndpointAddress)
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
            ValidateUserEndpoint(tokenSource as JwtSecurityToken, userEndpointAddress);


            return tokenSource;
        }

        public void ValidateUserEndpoint(JwtSecurityToken token, string userEndpointAddress)
        {
            if (token.Claims.First(c => c.Type == ApplicationClaimType.UserEndpointAddress).Value != userEndpointAddress)
            {
                throw new UserEndpointMismatchException();
            }
        }
    }
}
