using System.ComponentModel.DataAnnotations.Schema;

namespace DataLibrary.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        //[ForeignKey(nameof(User.Id))]
        public string UserId { get; set; }
    }
}
