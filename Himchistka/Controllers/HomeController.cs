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
        [Authorize(Roles = "Admin,User")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,User")]
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize(Roles = "Admin,User")]
        public IActionResult About()
        {

            return View();
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Service()
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