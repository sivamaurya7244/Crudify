using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        
        private readonly ILogger<DashBoardController> _logger;

        public DashBoardController(ILogger<DashBoardController> logger)
        {
            _logger = logger;
        }

        

        public IActionResult Index()
        {
            var userLoggedIn = HttpContext.Session.GetString("UserLoggedIn");

            if (string.IsNullOrEmpty(userLoggedIn) || userLoggedIn != "true")
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }

            return View();
        }

        public IActionResult Logout()
        {
            // Check if the user is logged in using the session value
            HttpContext.Session.SetString("UserLoggedIn", "false");
            return RedirectToAction("Login", "Login", new { area = "" });
        }

        public IActionResult Privacy()
        {
            var userLoggedIn = HttpContext.Session.GetString("UserLoggedIn");
            if (string.IsNullOrEmpty(userLoggedIn) || userLoggedIn != "true")
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            return View();
        }
    }
}
