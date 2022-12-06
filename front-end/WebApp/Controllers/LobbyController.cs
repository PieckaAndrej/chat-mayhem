using Data.ModelLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using NuGet.Protocol;
using System.Runtime.CompilerServices;
using System.Text.Json;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public void Refresh()
        {
            Console.WriteLine("refresh");
        }
    }
}
