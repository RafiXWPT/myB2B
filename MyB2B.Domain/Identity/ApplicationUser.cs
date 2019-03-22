using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyB2B.Domain.Companies;
using MyB2B.Domain.Results;

namespace MyB2B.Domain.Identity
{
    public enum UserStatus
    {
        NotVerified,
        Verified
    }


    public interface IApplicationPrincipal
    {
        int UserId { get; }
        string FirstName { get; }
        string LastName { get; }
        bool IsConfirmed { get; }
    }

    public class ApplicationUser : ApplicationEntity
    {
        [Required]
        public string Username { get; private set; }
        [Required]
        public byte[] PasswordHash { get; private set; }
        [Required]
        public byte[] PasswordSalt { get; private set; }
        [Required]
        public string Email { get; private set; }
        [Required]
        public virtual Company UserCompany { get; private set; }

        public virtual List<ApplicationRole> Roles { get; private set; }

        public UserStatus Status { get; private set; }

        public static Result<ApplicationUser> Create(string username, byte[] pwdHash, byte[] salt, string email)
        {
            return Result.Ok(new ApplicationUser
            {
                Username = username,
                PasswordHash = pwdHash,
                PasswordSalt = salt,
                Email = email,
                UserCompany = Company.Create().Value,
                Status = UserStatus.NotVerified
            });
        }
    }
}
