using System.ServiceModel.Syndication;
using System.Xml;
using RssApi.Models;
using RssApi.Repositories.Abstractions;
using RssApi.Services.Abstractions;

namespace RssApi.Services.Impl
{
    public class FeedService : IFeedService
    {
        public IFeedRepository Feeds { get; }
        public IUserRepository Users { get; }

        public FeedService(IFeedRepository feedRepository, IUserRepository userRepository)
        {
            Feeds = feedRepository;
            Users = userRepository;
        }

        public async Task AddFeed(string url, string userName)
        {
            var feed = ParseFeed(url);
            var user = await Users.GetUser(userName);

            user.Feeds.Add(feed);
            await Users.UpdateUser(user);
        }

        public async Task<List<Feed>> GetActiveFeeds(string userName)
        {
            var userFeeds = await Feeds.GetUserFeeds(userName);

            return userFeeds.Where(x => x.News.Any(y => !y.IsRead)).ToList();
        }

        private Feed ParseFeed(string url)
        {
            using var reader = XmlReader.Create(url);
            var rssFeed = SyndicationFeed.Load(reader);

            var feed = new Feed
            {
                Title = rssFeed.Title.Text,
                Link = url,
            };

            foreach (var item in rssFeed.Items)
            {
                feed.News.Add(new()
                {
                    Title = item.Title.Text,
                    Author = item.Authors.FirstOrDefault()?.Name ?? string.Empty,
                    Link = item.Links.FirstOrDefault()?.Uri.ToString() ?? string.Empty,
                    PublishDate = item.PublishDate
                });
            }

            return feed;
        }
    }
}