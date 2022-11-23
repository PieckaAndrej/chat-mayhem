using Microsoft.AspNetCore.Mvc;
using WebApp.BusinessLogic;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TwitchController : Controller
    {
        private StreamerLogic _streamerLogic;

        public TwitchController()
        {
            _streamerLogic = new StreamerLogic();
        }

        public async Task<IActionResult> Index(string code, string state)
        {
            string twitchState = HttpContext.Request.Cookies["twitch_state"] ?? "";

            if (twitchState != state)
            {
                return View("Error");
            }

            Streamer? streamer = await _streamerLogic.RegisterCode(code);

            Console.WriteLine(streamer.Name);
            Console.WriteLine(streamer.UserId);
            Console.WriteLine(streamer.AccessToken);

            return View();
        }
    }
}
