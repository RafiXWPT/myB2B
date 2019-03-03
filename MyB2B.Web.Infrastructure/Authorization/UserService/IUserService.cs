using MyB2B.Domain.Identity;
using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Authorization.UserService
{
    public interface IUserService
    {
        Result<AuthData> RefreshToken(int userId, string userEndpoint);
        Result<AuthData> Authenticate(string username, string password, string userEndpointAddress);
        Result<AuthData> Register(string username, string email, string password, string confirmPassword, string userEndpointAddress);
        Result<ApplicationUser> GetById(int id);
        void Update(ApplicationUser user);
        void Delete(int id);
    }
}
