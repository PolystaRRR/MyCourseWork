using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Himchistka.Data;
using Himchistka.Models.DataBase;
using Himchistka.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Himchistka.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(bool? delivery, string paymentType)
        {
            // Получаем список всех заказов
            var orders = _context.Orders.AsQueryable();

            // Фильтрация по наличию доставки
            if (delivery.HasValue)
            {
                if (delivery.Value)
                {
                    orders = orders.Where(u => u.Delivery == true);
                }
                else
                {
                    orders = orders.Where(u => u.Delivery == false);
                }
            }

            //Фильтрация по типу оплаты
            if (!string.IsNullOrEmpty(paymentType))
            {
                if (paymentType == "Онлайн")
                {
                    orders = orders.Where(u => u.PaymentType == "Онлайн");
                }
                else if (paymentType == "Наличные")
                {
                    orders = orders.Where(u => u.PaymentType == "Наличные");
                }
                else if (paymentType == "Карта")
                {
                    orders = orders.Where(u => u.PaymentType == "Карта");
                }
            }

            var appDbContext = await orders.ToListAsync();
            return View(appDbContext);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "PhysicalPersonId", "PhysicalPersonId");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Id,AcceptanceDate,ReadyDate,ReceptionPoint,Delivery,Discount,FinalPrice,PaymentType,ClientId,ServiceId")] Order order)
        {
            try
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) {
                return View(Consts.ImpossibleActionViewName);
                throw;
            }
                return RedirectToAction(nameof(Index));
            
           
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "PhysicalPersonId", "PhysicalPersonId", order.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", order.ServiceId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AcceptanceDate,ReadyDate,ReceptionPoint,Delivery,Discount,FinalPrice,PaymentType,ClientId,ServiceId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            
            
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'AppDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
