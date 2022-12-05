using Data.ModelLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using NuGet.Protocol;
using System.Text.Json;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
