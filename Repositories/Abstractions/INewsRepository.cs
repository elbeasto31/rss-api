using RssApi.Models;

namespace RssApi.Repositories.Abstractions
{
    public interface INewsRepository
    {
        Task<List<News>> GetUserNews(string userName);
        Task<News?> GetNewsById(int id);
        Task UpdateNews(News newInstance);
    }
}