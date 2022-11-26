using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.BusinessLogic;
using WebApp.Models;
using WebApp.Services;

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

            TempData["code"] = code;

            return RedirectToAction("Redirect");
        }

        public IActionResult Redirect()
        {
            return View();
        }


        public async Task<IActionResult> HandleCode()
        {
            if (TempData.ContainsKey("code"))
            {
                string code = TempData["code"].ToString();

                Streamer? streamer = await _streamerLogic.RegisterCode(code);

                if (streamer == null)
                {
                    return View("Error");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, streamer.Name),
                    new Claim(ClaimTypes.NameIdentifier, streamer.UserId),
                };

                TwitchUsers? user = await TwitchService.GetTwitchUsers(streamer.AccessToken);

                Console.WriteLine(user);

                Console.WriteLine(user?.Data);

                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Game");
            }

            return View("Error");
        }
    }
}
