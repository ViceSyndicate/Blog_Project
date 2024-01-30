using Blog_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataLibrary;
using DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using DataLibrary.DataAccess;

namespace Blog_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpGet]
        public IActionResult Post()
        {
            
            ViewBag.Message = "Post Blog Page";

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post(DataLibrary.Models.Post model)
        {
            if (ModelState.IsValid)
            {
                DataLibrary.Models.Post newPost = new Post();

                // We need to set newPost.UserId to logged in users id.
                newPost.UserId = model.UserId;

                newPost.Title = model.Title;
                newPost.Content = model.Content;
                //newPost.Created = DateTime.Now;
                // I need to set the UserId value to the logged in users somehow...
                EFBlogContext dbContext = new EFBlogContext();
                dbContext.Add(newPost);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(); // send user to posts or something.
        }
        // TODO
        // Add Logged in verification
        /*
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post(Models.VMPost model)
        {
            if (ModelState.IsValid)
            {
                DataLibrary.Models.Post post = new DataLibrary.Models.Post();
                post.Id = Guid.NewGuid().ToString();
                post.Title = model.Title;
                post.Content = model.Content;
                post.Created = DateTime.Now;
                // post.User = // Logged in users GUID
                using DataLibrary.DataAccess.EFBlogContext context = new DataLibrary.DataAccess.EFBlogContext();
                context.Posts.Add(post);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        */
    }
}
