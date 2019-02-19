using myB2B.Domain;

namespace myB2B.Web.Infrastructure.Authorization
{
    public class ApplicationIdentity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

        public int CompanyId { get; set; }
    }
}
