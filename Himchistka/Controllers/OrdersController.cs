using Himchistka.Data;
using Himchistka.Models.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Himchistka.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController (AppDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            IEnumerable<Order> objOrderList = _context.Orders;
            return View(objOrderList);
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Order obj)
        {
            _context.Orders.Add(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
