using DataLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
