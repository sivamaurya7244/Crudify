using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        
        public bool checkUserLogin()
        {
            var userLoggedIn = HttpContext.Session.GetString("UserLoggedIn");

            if (string.IsNullOrEmpty(userLoggedIn) || userLoggedIn != "true")
            {
                RedirectToAction("Login", "Login", new { area = "" });
            }
            return true;
        }
        public IActionResult RedirectToLogin()
        {
           return RedirectToAction("Login", "Login", new { area = "" });
        }
    }
}
