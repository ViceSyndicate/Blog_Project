using Blog_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataLibrary;
using DataLibrary.BusinessLogic;
using DataLibrary.Models;

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

        public ActionResult ViewUsers()
        {
            ViewBag.Message = "User List";
            // I create a list of DataLibrary users because I want the GUID value.
            var data = UserProcessor.LoadUsers();
            List<DataLibrary.Models.User> users = new List<DataLibrary.Models.User>();

            foreach (var row in data)
            {
                users.Add(new DataLibrary.Models.User
                {
                    Id = row.Id,
                    Username = row.Username,
                });
            }

            return View(users);
        }

        public IActionResult SignUp()
        {
            ViewBag.Message = "Sign Up Page";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(Models.User model)
        {
            if (ModelState.IsValid)
            {
                /*
                UserProcessor.CreateUser(
                    model.Username, 
                    model.Password);
                */
                DataLibrary.Models.User user = new DataLibrary.Models.User();
                user.Id = Guid.NewGuid().ToString();
                user.Username = model.Username;
                user.Password = model.Password;
                using DataLibrary.DataAccess.EFBlogContext context = new DataLibrary.DataAccess.EFBlogContext();
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        // Do User logged in validation
        public IActionResult Post()
        {
            ViewBag.Message = "Post Blog Page";

            return View();
        }
        // TODO
        // Add Logged in verification
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
    }
}
