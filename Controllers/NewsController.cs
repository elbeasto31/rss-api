using Microsoft.AspNetCore.Mvc;
using RssApi.Filters.Exceptions;
using RssApi.Services.Abstractions;

namespace RssApi.Controllers
{
    [Route("api/")]
    [ApiController]
    [ResponseCache(CacheProfileName = "Default")]
    public class NewsController : ControllerBase
    {
        private string UserName => User.Identity.Name; 
        public INewsService NewsService { get; }
        
        public NewsController(INewsService newsService)
        {
            NewsService = newsService;
        }

        [HttpGet]
        [Route("news")]
        public async Task<IActionResult> GetNews(DateTimeOffset date)
        {
            var news = await NewsService.GetNewsFromDate(UserName, date);
            return Ok(news);
        }

        [HttpPatch]
        [Route("news/read/{id}")]
        [NotFoundException]
        public async Task<IActionResult> ReadNews([FromRoute] int id)
        {
            await NewsService.ReadNews(UserName, id);
            return Ok();
        }
    }

}