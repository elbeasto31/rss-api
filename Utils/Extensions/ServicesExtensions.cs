using RssApi.Repositories.Abstractions;
using RssApi.Repositories.Impl;
using RssApi.Services.Abstractions;
using RssApi.Services.Impl;

namespace RssApi.Utils.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFeedRepository, FeedRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFeedService, FeedService>();
            services.AddScoped<INewsService, NewsService>();
        }
    }
}