using RssApi.Models;
using RssApi.Repositories.Abstractions;
using RssApi.Services.Abstractions;
using RssApi.Utils;
using RssApi.Utils.Exceptions;

namespace RssApi.Services.Impl
{
    public class NewsService : INewsService
    {
        public INewsRepository News { get; }

        public NewsService(INewsRepository newsRepository)
        {
            News = newsRepository;
        }

        public async Task<List<News>> GetNewsFromDate(string userName, DateTimeOffset date)
        {
            var news = await News.GetUserNews(userName);

            return news.Where(x => x.PublishDate > date).ToList();
        }

        public async Task ReadNews(string userName, int id)
        {
            var news = await News.GetNewsById(id);

            if (news is null || news.Feed.User.UserName != userName)
                throw new NotFoundException(Messages.NewsNotFound);

            news.IsRead = true;
            await News.UpdateNews(news);
        }
    }
}