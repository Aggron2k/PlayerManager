using Microsoft.AspNetCore.Mvc;
using PlayerManager.Models;
using System.Diagnostics;
using System.Text.Encodings.Web;

namespace PlayerManager.Controllers
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
            ViewData["Title"] = "BUMM!";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public string Welcome(int id, string name = "World")
        {
            return HtmlEncoder.Default.Encode($"Hello {id}. {name}");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
