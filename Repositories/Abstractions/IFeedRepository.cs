using RssApi.Models;

namespace RssApi.Repositories.Abstractions
{
    public interface IFeedRepository
    {
         Task<List<Feed>> GetUserFeeds(string userName);
    }
}