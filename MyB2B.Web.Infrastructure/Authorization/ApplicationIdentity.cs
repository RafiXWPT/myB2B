using System.Collections.Generic;
using System.Security.Claims;

namespace MyB2B.Web.Infrastructure.Authorization
{
    public class ApplicationIdentity : ClaimsIdentity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

        public int CompanyId { get; set; }

        public ApplicationIdentity() { }
        public ApplicationIdentity(IEnumerable<Claim> claims) : base(claims) { }
    }
}
