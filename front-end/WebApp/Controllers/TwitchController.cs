using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class TwitchController : Controller
    {
        public IActionResult Index(string code, string scope, string state)
        {
            TwitchService twitchService = new TwitchService();
            TwitchToken? twitchToken = twitchService.GetTwitchToken(code);

            Console.WriteLine(twitchToken);

            TwitchValidate? validate = twitchService.ValidateToken(twitchToken.AccessToken); 

            Console.WriteLine(validate);

            return View();
        }
    }
}
