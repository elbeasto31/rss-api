using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RssApi
{
    public static class AuthOptions
    {
        private const string SecretKey = "kzJFYkqqcVEeghXNgQbZyV3GATXxjBKETmR0cLIhSDbZ9qDcgSfUJ7GukAchNWM";
        public const string Audience = "https://my-resource.com";
        public const string Issuer = "https://github.com/elbeasto31";
        public const int LifeTime = 5;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
            => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
    }
}