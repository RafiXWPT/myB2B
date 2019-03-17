using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyB2B.Domain.Identity;
using MyB2B.Domain.Results;
using MyB2B.Server.Common;
using MyB2B.Web.Controllers.Logic.Authentication.Models;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Queries;
using MyB2B.Web.Infrastructure.ApplicationUsers.Services;

namespace MyB2B.Web.Controllers.Logic.Authentication
{
    public class AuthenticationControllerLogic : ControllerLogic
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly string _serverSecurityTokenSecret;

        public AuthenticationControllerLogic(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor, IConfiguration configuration, IApplicationUserService applicationUserService) :base(commandProcessor, queryProcessor)
        {
            _applicationUserService = applicationUserService;
            _serverSecurityTokenSecret = configuration.GetValue<string>("Security:Token:Secret");
        }

        public Result<AuthenticationDataDto> RefreshToken(int userId, string userEndpoint)
        {
            var user = _applicationUserService.GetById(userId);
            if (user.IsFail)
                return Result.Fail<AuthenticationDataDto>("there is no user in database");


            return Result.Ok(GenerateAuthData(user.Value, userEndpoint));
        }

        public Result<AuthenticationDataDto> Authenticate(string username, string password, string userEndpointAddress)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return Result.Fail<AuthenticationDataDto>("username or password is empty.");

            var queryResult = _applicationUserService.GetByUsername(username);
            if (queryResult.IsFail)
                return Result.Fail<AuthenticationDataDto>("there is no user with that username");

            var userFromDatabase = queryResult.Value;

            if (!VerifyPasswordHash(password, userFromDatabase.PasswordHash, userFromDatabase.PasswordSalt))
                return Result.Fail<AuthenticationDataDto>("given password is incorrect");

            return Result.Ok(GenerateAuthData(userFromDatabase, userEndpointAddress));
        }

        public Result<AuthenticationDataDto> Register(string username, string email, string password, string confirmPassword, string userEndpointAddress)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
                return Result.Fail<AuthenticationDataDto>("Form data is incorrect");

            if (password != confirmPassword)
                return Result.Fail<AuthenticationDataDto>("Passwords must be the same");

            var queryResult = _applicationUserService.GetByUsername(username);
            if (queryResult.IsOk)
                return Result.Fail<AuthenticationDataDto>("There is already user with that name");

            queryResult = _applicationUserService.GetByEmail(email);
            if (queryResult.IsOk)
                return Result.Fail<AuthenticationDataDto>("There is already registered account on that e-mail");

            CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            var createUserResult = _applicationUserService.Create(username, hash, salt, email);

            var databaseUser = createUserResult.Value;

            return Result.Ok(GenerateAuthData(databaseUser, userEndpointAddress));
        }

        private Claim CreateClaim<T>(string claimKey, T claimValue) => new Claim(claimKey, claimValue.ToString());

        private AuthenticationDataDto GenerateAuthData(ApplicationUser user, string userEndpoint)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = GenerateAuthenticationToken(tokenHandler, user, userEndpoint);

            return new AuthenticationDataDto
            {
                UserId = user.Id,
                Token = tokenHandler.WriteToken(token),
                ValidFrom = token.ValidFrom,
                ValidTo = token.ValidTo
            };
        }

        private void CreatePasswordHash(string plainPassword, out byte[] hash, out byte[] salt)
        {
            if (plainPassword == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(plainPassword)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
            }
        }

        private bool VerifyPasswordHash(string plainPassword, byte[] storedHash, byte[] storedSalt)
        {
            if (plainPassword == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(plainPassword)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
                if (computedHash.Where((t, i) => t != storedHash[i]).Any())
                {
                    return false;
                }
            }

            return true;
        }

        private SecurityToken GenerateAuthenticationToken(JwtSecurityTokenHandler tokenHandler, ApplicationUser user, string userEndpoint)
        {
            var key = Encoding.ASCII.GetBytes(_serverSecurityTokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    CreateClaim(ApplicationClaimType.UserId, user.Id),
                    CreateClaim(ApplicationClaimType.UserEndpointAddress, userEndpoint),
                    CreateClaim(ApplicationClaimType.UserCompanyName, "-"),
                    CreateClaim(ApplicationClaimType.UserLastLoginDate, DateTime.Now.AddHours(-1)),
                    CreateClaim(ApplicationClaimType.UserIsConfirmed, user.Status == UserStatus.Verified)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            return tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        }
    }
}