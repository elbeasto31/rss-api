using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RssApi.Models.RequestModels;
using RssApi.Repositories.Abstractions;
using RssApi.Services.Abstractions;
using RssApi.Utils;
using RssApi.Utils.Exceptions;

namespace RssApi.Services.Impl
{
    public class AuthService : IAuthService
    {
        public IUserRepository Users { get; }

        public AuthService(IUserRepository userRepository)
        {
            Users = userRepository;
        }

        public async Task<string> Register(RegisterModel registerModel)
        {
            var userName = registerModel.UserName;
            var password = registerModel.Password;
            var confirmPassword = registerModel.ConfirmPassword;

            if (password != confirmPassword)
                throw new PasswordsMatchingException(password, confirmPassword);

            if (await Users.UserExists(userName))
                throw new ExistingUserException(Messages.UserAlreadyExists);

            await Users.SaveUser(new()
            {
                UserName = userName,
                Password = password
            });

            return await SignIn(userName, password);
        }

        public async Task<string> SignIn(string userName, string password)
        {
            if(!(await Users.UserExists(userName)))
                throw new NotFoundException(Messages.UserNotFound);

            var jwt = new JwtSecurityToken(
                    notBefore: DateTime.UtcNow,
                    audience: AuthOptions.Audience,
                    issuer: AuthOptions.Issuer,
                    claims: new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, userName) },
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LifeTime)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}