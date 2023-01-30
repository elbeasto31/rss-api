using RssApi.Models;

namespace RssApi.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task SaveUser(User user);
        Task<bool> UserExists(string userName);
        Task<User?> GetUser(string userName);
        Task UpdateUser(User user);
    }
}