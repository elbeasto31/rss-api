using Microsoft.EntityFrameworkCore;
using RssApi.Models;
using RssApi.Repositories.Abstractions;

namespace RssApi.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {

        private RssDbContext DbContext { get; }
        public UserRepository(RssDbContext appDbContext)
        {
            DbContext = appDbContext;
        }

        public async Task SaveUser(User user)
        {
            await DbContext.Users.AddAsync(user);
            await DbContext.SaveChangesAsync();
        }

        public Task<bool> UserExists(string userName)
            => DbContext.Users
                .AnyAsync(x => x.UserName == userName);

        public async Task UpdateUser(User user)
        {
            DbContext.Users.Update(user);
            await DbContext.SaveChangesAsync();
        }

        public Task<User?> GetUser(string userName)
            => DbContext.Users
                .FirstOrDefaultAsync(x => x.UserName == userName);
    }
}