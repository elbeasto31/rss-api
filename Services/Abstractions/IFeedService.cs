using RssApi.Models;
using RssApi.Repositories.Abstractions;

namespace RssApi.Services.Abstractions
{
    public interface IFeedService
    {
        IFeedRepository Feeds { get; }
        Task<List<Feed>> GetActiveFeeds(string userName);
        Task AddFeed(string url, string userName);
    }
}