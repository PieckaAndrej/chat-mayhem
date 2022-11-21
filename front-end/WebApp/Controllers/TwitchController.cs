using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class TwitchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
