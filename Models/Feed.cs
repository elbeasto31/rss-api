using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RssApi.Models
{
    public class Feed
    {
        [Key] public int FeedId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        [JsonIgnore] public virtual User User { get; set; }
        [JsonIgnore] public virtual List<News> News { get; set; } = new();
    }
}