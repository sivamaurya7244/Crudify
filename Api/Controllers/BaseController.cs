using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }
    }
}
