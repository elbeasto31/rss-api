using Microsoft.EntityFrameworkCore;
using RssApi.Models;
using RssApi.Repositories.Abstractions;

namespace RssApi.Repositories.Impl
{
    public class NewsRepository : INewsRepository
    {
        private RssDbContext DbContext { get; }
        public NewsRepository(RssDbContext appDbContext)
        {
            DbContext = appDbContext;
        }

        public Task<List<News>> GetUserNews(string userName) 
            => DbContext.News
                .Include(x => x.Feed.User)
                .Where(x => x.Feed.User.UserName == userName)
                .ToListAsync();
                
        public Task UpdateNews(News newInstance)
        {
            DbContext.News.Update(newInstance);
            return DbContext.SaveChangesAsync();
        }

        public Task<News?> GetNewsById(int id) 
            => DbContext.News
                .Include(x => x.Feed.User)
                .SingleOrDefaultAsync(x => x.NewsId == id);

    }
}