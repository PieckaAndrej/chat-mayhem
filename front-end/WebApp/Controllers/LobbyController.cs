using Data.ModelLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using NuGet.Protocol;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {
        public IActionResult Index()
        {
            JoinLobby joinLobby= new JoinLobby();

            joinLobby.Code = TempData["code"]?.ToString() ?? "";
            joinLobby.Name = TempData["name"]?.ToString() ?? "";
            TempData.Remove("code");
            TempData.Remove("name");

            return View("Index", joinLobby);
        }

        public async Task<IActionResult> RefreshToken(string access)
        {
            var claims = HttpContext.User.Claims;
            var newClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, claims.Single(c => c.Type == ClaimTypes.Name).Value),
                    new Claim(ClaimTypes.NameIdentifier, claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value),
                    new Claim("AccessToken", access),
                    new Claim("ProfileImage", claims.Single(c => c.Type == "ProfileImage").Value)
                };
            var claimsIdentity = new ClaimsIdentity(newClaims, "Login");
            await HttpContext.SignOutAsync();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
                    {
                        IsPersistent = true
                    });
            return View("Close");
        }

        public IActionResult Join(string code)
        {
            JoinLobby model = new JoinLobby();
            model.Code = code;

            return View(model);
        }

        [HttpPost]
        public IActionResult Join(JoinLobby joinLobby)
        {
            TempData["code"] = joinLobby.Code;
            TempData["name"] = joinLobby.Name;
            return RedirectToAction("Index");
        }
    }
}
