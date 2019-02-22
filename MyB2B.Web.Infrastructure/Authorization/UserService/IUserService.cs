using MyB2B.Domain.Identity;
using MyB2B.Domain.Results;

namespace MyB2B.Web.Infrastructure.Authorization.UserService
{
    public interface IUserService
    {
        Result<AuthData> Authenticate(string username, string password);
        ApplicationUser GetById(int id);
        ApplicationUser Create(ApplicationUser user);
        void Update(ApplicationUser user);
        void Delete(int id);
    }
}
