using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class TwitchController : Controller
    {
        public IActionResult Index(String code, String scope, string state)
        {

            Console.WriteLine(code);
            Console.WriteLine(scope);
            Console.WriteLine(state);
            Console.WriteLine("yo");
            return View("Index", $"code={code}&scope={scope}&state={state}");
        }
    }
}
