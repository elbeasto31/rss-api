using System.ComponentModel.DataAnnotations;

namespace RssApi.Models
{
    public class User
    {
        [Key] public string UserName { get; set; }
        public string Password { get; set; }
        public virtual List<Feed> Feeds { get; set; } = new();
    }
}