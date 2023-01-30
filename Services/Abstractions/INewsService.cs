using RssApi.Models;
using RssApi.Repositories.Abstractions;

namespace RssApi.Services.Abstractions
{
    public interface INewsService
    {
        INewsRepository News { get; }
        Task<List<News>> GetNewsFromDate(string userName, DateTimeOffset date);
        Task ReadNews(string userName, int id);
    }
}