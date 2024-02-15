using Blog_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DataLibrary.DataHandler;

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
        public async Task<IActionResult> AllPosts(string sortOrder, int? pageNumber)
        {
            // Unused
            ViewData["CurrentSort"] = sortOrder;

            ViewBag.Message = "Post Blog Page";

            DataHandler dataHandler = new DataHandler();
            List<Post> posts = dataHandler.GetAllPosts();

            int pageSize = 5;

            PaginatedList<Post> paginatedPosts = await PaginatedList<Post>.Create(posts, pageNumber ?? 1, pageSize);
            return View(paginatedPosts);
        }

        [Authorize]
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Posts(string sortOrder, int? pageNumber)
        {
            // Unused
            ViewData["CurrentSort"] = sortOrder;

            ViewBag.Message = "Post Blog Page";

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            DataHandler dataHandler = new DataHandler();
            List<Post> userPosts = dataHandler.GetUserPosts(userId);

            /*
            Used in PaginatedList<Post>.CreateAsync
            DataLibrary.DataAccess.EFBlogContext _context = new DataLibrary.DataAccess.EFBlogContext();
            var posts = _context.Posts.Where(p => p.UserId == userId);
            string postsType = posts.GetType().ToString();
            // "Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1[DataLibrary.Models.Post]"
            PaginatedList<Post> paginatedPosts = await PaginatedList<Post>.CreateAsync(posts, pageNumber ?? 1, pageSize);
            */

            int pageSize = 5;
            
            PaginatedList<Post> paginatedPosts = await PaginatedList<Post>.Create(userPosts, pageNumber ?? 1, pageSize);
            return View(paginatedPosts);
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
        public IActionResult Post(Models.VMPost model)
        {
            if (ModelState.IsValid)
            {
                DataLibrary.Models.Post newPost = new Post();

                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var userId = claim.Value;
                if (userId == null)
                {
                    // Couldn't find logged in user!
                    return Error();
                }

                newPost.UserId = userId;
                newPost.Title = model.Title;
                newPost.Content = model.Content;

                DataHandler dataHandler = new DataHandler();
                dataHandler.AddPost(newPost);

                return RedirectToAction("Index");
            }
            return View(); // send user to posts or something.
        }
    }
}
