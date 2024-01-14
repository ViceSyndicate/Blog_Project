using Blog_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataLibrary;
using DataLibrary.BusinessLogic;
using DataLibrary.Model;

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
            List<DataLibrary.Model.User> users = new List<DataLibrary.Model.User>();

            foreach (var row in data)
            {
                users.Add(new DataLibrary.Model.User
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
                UserProcessor.CreateUser(
                    model.Username, 
                    model.Password);
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
