using System.Collections.Generic;

namespace myB2B.Domain.Identity
{
    public enum UserStatus
    {
        NotVerified,
        Verified
    }

    public class ApplicationUser : ApplicationEntity
    {
        public string Username { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public string Email { get; private set; }
        public Company.Company UserCompany { get; private set; }

        public UserStatus Status { get; private set; }

        public List<ApplicationRole> Roles { get; private set; }

        public static ApplicationUser Create(string username, byte[] pwdHash, byte[] salt, string email)
        {
            return new ApplicationUser
            {
                Username = username,
                PasswordHash = pwdHash,
                PasswordSalt = salt,
                Email = email,
                Status = UserStatus.NotVerified
            };
        }
    }
}
