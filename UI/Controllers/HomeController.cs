using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
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
            // Check if the user is logged in using the session value
            var userLoggedIn = HttpContext.Session.GetString("UserLoggedIn");

            if (string.IsNullOrEmpty(userLoggedIn) || userLoggedIn != "true")
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }


}
