using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
