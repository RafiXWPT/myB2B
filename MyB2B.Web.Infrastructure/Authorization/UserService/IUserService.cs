using MyB2B.Domain.Identity;
using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Authorization.UserService
{
    public interface IUserService
    {
        Result<AuthData> Authenticate(string username, string password);
        Result<AuthData> Register(string username, string email, string password, string confirmPassword);
        Result<ApplicationUser> GetById(int id);
        void Update(ApplicationUser user);
        void Delete(int id);
    }
}
