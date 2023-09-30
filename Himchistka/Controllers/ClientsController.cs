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

namespace Himchistka.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string searchField , string searchString)
        {
            var appDbContext = _context.Clients.Include(c => c.PhysicalPerson).AsQueryable();
           

            if (_context.Clients == null)
            {
                return RedirectToAction("Index");
            }

            var clients = _context.Clients.Include(c => c.PhysicalPerson).AsQueryable();


            if (!string.IsNullOrEmpty(searchString))
            {
                switch (searchField)
                {
                   
                    case "PhoneNumber":
                        clients = clients.Where(c => c.PhoneNumber.Contains(searchString));
                        break;
                    case "Email":
                        clients = clients.Where(c => c.Email.Contains(searchString));
                        break;
                    case "Adress":
                        clients = clients.Where(c => c.Adress.Contains(searchString));
                        break;
                }
            }


            return View ("Index",  await clients.ToListAsync());
            
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.PhysicalPerson)
                .FirstOrDefaultAsync(m => m.PhysicalPersonId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        [Authorize(Roles ="admin")]
        public IActionResult Create()
        {
            ViewData["PhysicalPersonId"] = new SelectList(_context.PhysicalPersons, "Id", "Id");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("PhysicalPersonId,PhoneNumber,Email,Adress")] Client client)
        {
            try
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) {
                return View(Consts.ImpossibleActionViewName);
                throw;
            }
            return RedirectToAction(nameof(Index));
            
            
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["PhysicalPersonId"] = new SelectList(_context.PhysicalPersons, "Id", "Name", client.PhysicalPersonId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("PhysicalPersonId,PhoneNumber,Email,Adress")] Client client)
        {
            if (id != client.PhysicalPersonId)
            {
                return NotFound();
            }

            
           
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.PhysicalPersonId))
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

        // GET: Clients/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.PhysicalPerson)
                .FirstOrDefaultAsync(m => m.PhysicalPersonId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'AppDbContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return (_context.Clients?.Any(e => e.PhysicalPersonId == id)).GetValueOrDefault();
        }


      
    }
}
