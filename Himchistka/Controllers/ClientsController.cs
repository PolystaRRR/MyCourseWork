using Himchistka.Data;
using Microsoft.AspNetCore.Mvc;

namespace Himchistka.Controllers
{
    public class ClientsController : Controller
    {

        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Clients.ToList();
            return View();
        }
    }
}
