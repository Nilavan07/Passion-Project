using Microsoft.AspNetCore.Mvc;

namespace FootballHub.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
