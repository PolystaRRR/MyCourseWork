using Himchistka.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;

namespace Himchistka.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize(Roles = "admin,user")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin,user")]
        public IActionResult Privacy()
        {
            return View();
        }
    



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}