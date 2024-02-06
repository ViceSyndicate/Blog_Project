using Blog_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataLibrary;
using DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using DataLibrary.DataAccess;
using System.Security.Claims;
using DataLibrary.DataHandler;
using PagedList;
using Blog_Project.Areas.Identity.Pages;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

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
        public IActionResult AllPosts(string sortOrder, int? pageNumber)
        {
            DataHandler dataHandler = new DataHandler();
            List<Post> allPosts = dataHandler.GetAllPosts();
            int pageSize = 5;
            int currentPageNumber = 0;
            if(pageNumber != null) { currentPageNumber = (int)pageNumber; }

            // PaginatedList<Post> paginatedPosts = await PaginatedList<Post>.Create(userPosts, pageNumber ?? 1, pageSize);
            if (pageNumber == null) { pageNumber = 0; }
            var paginatedPosts = PaginatedList<Post>.Create(allPosts, currentPageNumber, pageSize);
            string paginatedPostType = paginatedPosts.GetType().Name;
            return View(allPosts);
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

            //return View(dataHandler.GetUserPosts);
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

                // We need to set newPost.UserId to logged in users id.
                newPost.UserId = userId;

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
    }
}
