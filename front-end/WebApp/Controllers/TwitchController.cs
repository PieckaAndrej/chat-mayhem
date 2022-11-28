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
            string twitchState = TempData["twitch_state"]?.ToString() ?? "";
            TempData.Remove("twitch_state");

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
                string code = TempData["code"]?.ToString() ?? "";
                TempData.Remove("code");

                Streamer? streamer = await _streamerLogic.RegisterCode(code);

                if (streamer == null)
                {
                    return View("Error");
                }

                TwitchUsers? users = await TwitchService.GetTwitchUsers(streamer.AccessToken);

                if (users?.Data?.Count == 0)
                {
                    return View("Error");
                }

                UsersData userdata = users.Data.First();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userdata.DisplayName),
                    new Claim(ClaimTypes.NameIdentifier, streamer.UserId),
                    new Claim("AccessToken", streamer.AccessToken),
                    new Claim("ProfileImage", userdata.ProfileImageUrl)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
                    {
                        IsPersistent = true
                    });

                return RedirectToAction("Index", "Game");
            }

            return View("Error");
        }
    }
}
