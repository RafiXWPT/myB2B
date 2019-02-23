using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using MyB2B.Web.Infrastructure.Authorization.UserService;

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
    }
}
