using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyB2B.Domain.Identity;
using MyB2B.Server.Common;
using MyB2B.Web.Infrastructure.Dependency;

namespace MyB2B.Web.Infrastructure.ApplicationUsers
{
    public class UserEndpointMismatchException : SecurityTokenValidationException
    {
        public UserEndpointMismatchException() : base("Endpoint saved in token mismatch current user address.") { }
    }

    public class ApplicationPrincipal : ClaimsPrincipal, IApplicationPrincipal
    {
        private ApplicationPrincipal() { }
        public ApplicationPrincipal(IPrincipal principal) : base(principal) { }
        public ApplicationPrincipal(IIdentity identity) : base(identity) { }

        public int UserId => Convert.ToInt32(GetClaimValueOrDefault(ApplicationClaimType.UserId, "-1"));
        public string FirstName => GetClaimValueOrDefault(ApplicationClaimType.UserFirstName, "Guest");
        public string LastName => GetClaimValueOrDefault(ApplicationClaimType.UserLastName, "Guest");
        public bool IsConfirmed => Convert.ToBoolean(GetClaimValueOrDefault(ApplicationClaimType.UserIsConfirmed, "false"));

        public static ApplicationPrincipal GuestPrincipal() => new ApplicationPrincipal();

        public SecurityToken ValidateToken(string token, string userEndpointAddress)
        {
            SecurityTokenPrincipalExtractor.ExtractFromToken(token, out var tokenSource);
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

        private string GetClaimValueOrDefault(string claimType, string defaultValue = null)
        {
            var claim = FindFirst(claimType);
            return claim != null ? claim.Value : defaultValue ?? string.Empty;
        }
    }

    public static class SecurityTokenPrincipalExtractor
    {
        public static string GetAuthorizationTokenFromRequest(this HttpContext httpContext) => httpContext?.Request?.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

        public static ApplicationPrincipal ExtractFromToken(string token, out SecurityToken tokenSource)
        {
            var securityTokenSecret = DependencyContainer.Container.GetInstance<IConfiguration>().GetValue<string>("Security:Token:Secret");
            var tokenHandler = new JwtSecurityTokenHandler();
            var validation = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityTokenSecret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var tokenPrincipal = tokenHandler.ValidateToken(token, validation, out SecurityToken extractedTokenSource);
            tokenSource = extractedTokenSource;
            return new ApplicationPrincipal(tokenPrincipal);
        }
    }

    public static class ApplicationPrincipalExtensions
    {
        public static IApplicationPrincipal ExtractApplicationUserInterface(this IPrincipal principal) => ExtractApplicationPrincipal(principal);
        public static IApplicationPrincipal ExtractApplicationUserInterface(this IIdentity identity) => ExtractApplicationPrincipal(identity);
        public static IApplicationPrincipal ExtractApplicationUserInterface(this HttpContext currentContext) => ExtractApplicationPrincipal(currentContext);

        public static ApplicationPrincipal ExtractApplicationPrincipal(this IPrincipal principal) => new ApplicationPrincipal(principal);
        public static ApplicationPrincipal ExtractApplicationPrincipal(this IIdentity identity) => new ApplicationPrincipal(identity);
        public static ApplicationPrincipal ExtractApplicationPrincipal(this HttpContext currentContext)
        {
            var bearerToken = currentContext.GetAuthorizationTokenFromRequest();
            return bearerToken == null ? 
                ApplicationPrincipal.GuestPrincipal() 
                : 
                SecurityTokenPrincipalExtractor.ExtractFromToken(bearerToken, out _);
        }
    }
}
