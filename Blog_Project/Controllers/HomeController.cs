using Blog_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataLibrary;
using DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using DataLibrary.DataAccess;
using System.Security.Claims;

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
        public IActionResult Post(Models.VMPost model)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            if (userId == null) 
            {
                // Couldn't find logged in users id!
                return Error();
            }

            if (ModelState.IsValid)
            {
                DataLibrary.Models.Post newPost = new Post();
                newPost.UserId = userId;
                newPost.Title = model.Title;
                newPost.Content = model.Content;

                EFBlogContext dbContext = new EFBlogContext();
                dbContext.Add(newPost);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(); // send user to posts or something.
        }
    }
}
