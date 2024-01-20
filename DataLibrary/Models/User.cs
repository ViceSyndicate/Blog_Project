using Microsoft.AspNetCore.Identity;

namespace DataLibrary.Models
{
    public class User : IdentityUser
    {
        //[Key]
        //public string Id { get; set; }
        //public string Username { get; set; }
        //public string Password { get; set; }
        public ICollection<Post> Posts { get; set; } = null!;
        public DateTime Created { get; set; }
    }
}
