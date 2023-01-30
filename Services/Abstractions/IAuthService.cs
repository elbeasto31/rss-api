using RssApi.Models.RequestModels;
using RssApi.Repositories.Abstractions;

namespace RssApi.Services.Abstractions
{
    public interface IAuthService
    {
        IUserRepository Users { get; }
        Task<string> SignIn(string userName, string password);
        Task<string> Register(RegisterModel registerModel);
    }
}