using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Empolyee : Controller
    {
        public IActionResult Index()
        {
            var userLoggedIn = HttpContext.Session.GetString("UserLoggedIn");

            if (string.IsNullOrEmpty(userLoggedIn) || userLoggedIn != "true")
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }

            return View();
        }

        public IActionResult Create()
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
