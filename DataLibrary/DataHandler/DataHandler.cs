using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.DataHandler
{
    public class DataHandler
    {
        EFBlogContext dbContext = new EFBlogContext();

        public List<Models.Post> GetUserPosts(string userId)
        {
            //List<Models.Post> usersPosts = new List<Models.Post>();
            List<Models.Post> usersPosts = dbContext.Posts.Where(p => p.UserId == userId).ToList();
            return usersPosts;
        }

        public List<Models.Post> GetAllPosts()
        {
            var posts = from s in dbContext.Posts
                        select s;
            List<Models.Post> postsToList = posts.ToList();
            return postsToList;

        }
        public void AddPost(Models.Post post)
        {
            if (post != null)
            {
                dbContext.Posts.Add(post);
                dbContext.SaveChanges();
                return;
            }
        }
        public User GetUser(string id)
        {
            User user = dbContext.Users.Where(u => u.Id == id).FirstOrDefault();
            return user;
        }
    }
}
