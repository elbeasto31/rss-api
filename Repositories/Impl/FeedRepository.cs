using Microsoft.EntityFrameworkCore;
using RssApi.Models;
using RssApi.Repositories.Abstractions;

namespace RssApi.Repositories.Impl
{
    public class FeedRepository : IFeedRepository
    {
        private RssDbContext DbContext { get; }
        public FeedRepository(RssDbContext appDbContext)
        {
            DbContext = appDbContext;
        }

        public Task<List<Feed>> GetUserFeeds(string userName)
            => DbContext.Feeds
                .Include(x => x.News)
                .Include(x => x.User)
                .Where(x => x.User.UserName == userName)
                .ToListAsync();
    }
}