using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Himchistka.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        //public IActionResult SubmitOrder()
        //{
        //    var Request.Form[""]
        //    return View("Add");
        //}

    }
}
