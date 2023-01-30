using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RssApi.Filters.Exceptions;
using RssApi.Services.Abstractions;

namespace RssApi.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private string UserName => User.Identity.Name; 
        public IFeedService FeedService { get; }

        public FeedController(IFeedService feedService)
        {
            FeedService = feedService;
        }

        [HttpGet]
        [Route("feeds/active")]
        // Getting all the feeds that contain unread news
        public async Task<IActionResult> GetActiveFeeds()
        {
            var feeds = await FeedService.GetActiveFeeds(UserName);
            return Ok(feeds);
        }

        [HttpPost]
        [FeedException]
        [Route("feed/add")]
        public async Task<IActionResult> AddFeed(string url)
        {
            await FeedService.AddFeed(url, UserName);
            return Ok();
        }
    }

}