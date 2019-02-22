﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyB2B.Domain.Identity;
using MyB2B.Domain.Results;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Queries;
using MyB2B.Web.Infrastructure.Authorization.UserService.Commands;
using MyB2B.Web.Infrastructure.Authorization.UserService.Queries;

namespace MyB2B.Web.Infrastructure.Authorization.UserService
{
    public static class ApplicationClaimType
    {
        public const string UserId = "USER_ID";
        public const string UserRole = "USER_ROLE";
        public const string UserEndpointAddress = "USER_ENDPOINT_ADDRESS";
        public const string UserFirstName = "USER_FIRST_NAME";
        public const string UserLastName = "USER_LAST_NAME";
        public const string UserCompanyName = "USER_COMPANY_NAME";
        public const string UserLastLoginDate = "USER_LAST_LOG_IN_DATE";
        public const string UserIsConfirmed = "USER_IS_CONFIRMED";
    }

    public class UserService : IUserService
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;
        private readonly string _serverSecurityTokenSecret;

        public UserService(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor, IConfiguration configuration)
        {
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcessor;
            _serverSecurityTokenSecret = configuration.GetValue<string>("Security:Token:Secret");
        }

        public Result<AuthData> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return Result.Fail<AuthData>("username or password is empty.");

            var queryAction = _queryProcessor.Query(new GetUserByNameQuery(username));
            if (!queryAction.Success)
                return Result.Fail<AuthData>("there is no user with that username");

            var userFromDatabase = queryAction.Result;

            if (!VerifyPasswordHash(password, userFromDatabase.PasswordHash, userFromDatabase.PasswordSalt))
                return Result.Fail<AuthData>("given password is incorrect");

            var authData = new AuthData
            {
                UserId = userFromDatabase.Id,
                Token = GenerateAuthenticationToken(userFromDatabase)
            };

            return Result.Ok(authData);
        }

        public Result<AuthData> Register(string username, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
                return Result.Fail<AuthData>("Form data is incorrect");

            if (password != confirmPassword)
                return Result.Fail<AuthData>("Passwords must be the same");

            var queryAction = _queryProcessor.Query(new GetUserByNameQuery(username));
            if (queryAction.Success)
                return Result.Fail<AuthData>("There is already user with that name");

            queryAction = _queryProcessor.Query(new GetUserByEmailQuery(email));
            if (queryAction.Success)
                return Result.Fail<AuthData>("There is already registered account on that e-mail");

            CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            var command = new CreateUserCommand(username, hash, salt, email);
            _commandProcessor.Execute(command);

            var databaseUser = command.Output.Result as ApplicationUser;

            var authData = new AuthData
            {
                UserId = databaseUser.Id,
                Token = GenerateAuthenticationToken(databaseUser)
            };

            return Result.Ok(authData);
        }

        public ApplicationUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        private static Claim CreateClaim<T>(string claimKey, T claimValue) => new Claim(claimKey, claimValue.ToString());

        private static void CreatePasswordHash(string plainPassword, out byte[] hash, out byte[] salt)
        {
            if (plainPassword == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(plainPassword)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                hash = hmac.Key;
                salt = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
            }
        }

        private static bool VerifyPasswordHash(string plainPassword, byte[] storedHash, byte[] storedSalt)
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

        private string GenerateAuthenticationToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_serverSecurityTokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ApplicationIdentity(new[]
                {
                    CreateClaim(ApplicationClaimType.UserId, user.Id),
                    CreateClaim(ApplicationClaimType.UserEndpointAddress, "test-localhost"),
                    CreateClaim(ApplicationClaimType.UserCompanyName, "-"),
                    CreateClaim(ApplicationClaimType.UserLastLoginDate, DateTime.Now.AddHours(-1)),
                    CreateClaim(ApplicationClaimType.UserIsConfirmed, false)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}