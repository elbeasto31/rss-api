using System.ComponentModel.DataAnnotations;

namespace RssApi.Models
{
    public class News
    {
        [Key] public int NewsId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }
        public bool IsRead { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public virtual Feed Feed { get; set; }
    }
}