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
using MyB2B.Web.Controllers.Logic.Authentication.Commands;
using MyB2B.Web.Controllers.Logic.Authentication.Models;
using MyB2B.Web.Controllers.Logic.Authentication.Queries;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Commands.Extensions;
using MyB2B.Web.Infrastructure.Actions.Queries;

namespace MyB2B.Web.Controllers.Logic.Authentication
{
    public class AuthenticationLogic : ControllerLogic
    {
        private readonly string _serverSecurityTokenSecret;

        public AuthenticationLogic(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor, IConfiguration configuration) : base(commandProcessor, queryProcessor)
        {
            _serverSecurityTokenSecret = configuration.GetValue<string>("Security:Token:Secret");
        }

        public Result<AuthenticationDataDto> RefreshToken(int userId, string userEndpoint)
        {
            var user = QueryProcessor.Query(new GetUserByIdQuery(userId));
            if (user.IsFail)
                return Result.Fail<AuthenticationDataDto>("There is no user in database.");


            return Result.Ok(GenerateAuthData(user.Value, userEndpoint));
        }

        public Result<AuthenticationDataDto> Authenticate(string username, string password, string userEndpointAddress)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return Result.Fail<AuthenticationDataDto>("username or password is empty.");

            var queryResult = QueryProcessor.Query(new GetUserByUsernameQuery(username));
            if (queryResult.IsFail)
                return Result.Fail<AuthenticationDataDto>("There is no user with that username.");

            var userFromDatabase = queryResult.Value;

            if (!VerifyPasswordHash(password, userFromDatabase.PasswordHash, userFromDatabase.PasswordSalt))
                return Result.Fail<AuthenticationDataDto>("given password is incorrect");

            return Result.Ok(GenerateAuthData(userFromDatabase, userEndpointAddress));
        }

        public Result<AuthenticationDataDto> Register(string username, string email, string password, string confirmPassword, string userEndpointAddress)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
                return Result.Fail<AuthenticationDataDto>("Form data is incorrect.");

            if (password != confirmPassword)
                return Result.Fail<AuthenticationDataDto>("Passwords must be the same.");

            var queryResult = QueryProcessor.Query(new GetUserByUsernameQuery(username));
            if (queryResult.IsOk)
                return Result.Fail<AuthenticationDataDto>("There is already user with that name.");

            queryResult = QueryProcessor.Query(new GetUserByEmailQuery(email));
            if (queryResult.IsOk)
                return Result.Fail<AuthenticationDataDto>("There is already registered account on that e-mail.");

            CreatePasswordHash(password, out byte[] hash, out byte[] salt);

            var createUserResult = CommandProcessor.Execute(new CreateUserCommand(username, hash, salt, email))
                .GetCommandResult();

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
                CompanyId = user.UserCompany.Id,
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
                    CreateClaim(ApplicationClaimType.UserCompanyId, user.UserCompany.Id),
                    CreateClaim(ApplicationClaimType.UserCompanyName, user.UserCompany.Name),
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