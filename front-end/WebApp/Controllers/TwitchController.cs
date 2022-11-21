using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class TwitchController : Controller
    {
        public IActionResult Index(string access_token, string scope, string state, string token_type)
        {

            Console.WriteLine(access_token);
            Console.WriteLine(scope);
            Console.WriteLine(state);

            TwitchService twitchService = new TwitchService();
            TwitchValidate? validate = twitchService.ValidateToken(access_token);

            Console.WriteLine(validate?.login);

            return View();
        }
    }
}
