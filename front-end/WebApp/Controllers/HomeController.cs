using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            var param = new NameValueCollection();

            // GUID string for a state
            string twitchState = Guid.NewGuid().ToString();
            TempData["twitch_state"] =  twitchState;

            string baseUrl = "https://id.twitch.tv/oauth2/authorize?";
            param.Add("response_type", "code");
            param.Add("client_id", "8hmbxjfogmmj9e14y2ohn2vb0q8zv5");
            param.Add("scope", "chat:read");
            param.Add("redirect_uri", "https://localhost:7026/twitch");
            param.Add("state", twitchState);

            var url = HttpUtility.ParseQueryString(baseUrl);
            url.Add(param);
            
            return Redirect(HttpUtility.UrlDecode(url.ToString()));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}