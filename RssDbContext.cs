using Microsoft.EntityFrameworkCore;
using RssApi.Models;

namespace RssApi
{
    public class RssDbContext : DbContext
    {
        public RssDbContext(DbContextOptions options) : base(options) { Database.EnsureCreated(); }
        public DbSet<News> News { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Feed> Feeds { get; set; }
    }
}