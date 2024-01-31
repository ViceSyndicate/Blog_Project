using DataLibrary.DataAccess;
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
    }
}
