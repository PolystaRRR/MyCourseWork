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
using static NuGet.Packaging.PackagingConstants;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Himchistka.Controllers
{
    public class PhysicalPersonsController : Controller
    {
        private readonly AppDbContext _context;

        public PhysicalPersonsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PhysicalPersons
        public async Task<IActionResult> Index(bool? sex)
        {
            // Получаем список всех пользователей
            var persons = _context.PhysicalPersons.AsQueryable();
            // Фильтрация по полу
            if (sex.HasValue)
            {
                persons = persons.Where(u => u.Sex == sex.Value);
            }

            var appDbContext = await persons.ToListAsync();
            return View(appDbContext);


        }

        // GET: PhysicalPersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PhysicalPersons == null)
            {
                return NotFound();
            }

            var physicalPerson = await _context.PhysicalPersons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (physicalPerson == null)
            {
                return NotFound();
            }

            return View(physicalPerson);
        }

        // GET: PhysicalPersons/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhysicalPersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,MiddleName,Sex")] PhysicalPerson physicalPerson)
        {
            try
            {
                _context.Add(physicalPerson);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) {
                return View(Consts.ImpossibleActionViewName);
                throw;
            }
                return RedirectToAction(nameof(Index));
            }



        // GET: PhysicalPersons/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PhysicalPersons == null)
            {
                return NotFound();
            }

            var physicalPerson = await _context.PhysicalPersons.FindAsync(id);
            if (physicalPerson == null)
            {
                return NotFound();
            }
            return View(physicalPerson);
        }

        // POST: PhysicalPersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,MiddleName,Sex")] PhysicalPerson physicalPerson)
        {
                if (id != physicalPerson.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(physicalPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            catch (DbUpdateConcurrencyException)
                {
                    if (!PhysicalPersonExists(physicalPerson.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            
            
        }

        // GET: PhysicalPersons/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PhysicalPersons == null)
            {
                return NotFound();
            }

            var physicalPerson = await _context.PhysicalPersons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (physicalPerson == null)
            {
                return NotFound();
            }

            return View(physicalPerson);
        }

        // POST: PhysicalPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PhysicalPersons == null)
            {
                return Problem("Entity set 'AppDbContext.PhysicalPersons'  is null.");
            }
            var physicalPerson = await _context.PhysicalPersons.FindAsync(id);
            if (physicalPerson != null)
            {
                _context.PhysicalPersons.Remove(physicalPerson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhysicalPersonExists(int id)
        {
          return (_context.PhysicalPersons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
