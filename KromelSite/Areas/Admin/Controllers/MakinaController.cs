using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KromelSite.Data;
using KromelSite.Models;

namespace KromelSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MakinaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MakinaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Makina
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Makina.Include(m => m.UrunGruplari);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Makina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var makina = await _context.Makina
                .Include(m => m.UrunGruplari)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (makina == null)
            {
                return NotFound();
            }

            return View(makina);
        }

        // GET: Admin/Makina/Create
        public IActionResult Create()
        {
            ViewData["UrunGruplariID"] = new SelectList(_context.UrunGruplari, "Id", "Id");
            return View();
        }

        // POST: Admin/Makina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MakinaAdi,MakinaTanitim,UrunGruplariID,ResimYolu")] Makina makina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(makina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UrunGruplariID"] = new SelectList(_context.UrunGruplari, "Id", "Id", makina.UrunGruplariID);
            return View(makina);
        }

        // GET: Admin/Makina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var makina = await _context.Makina.FindAsync(id);
            if (makina == null)
            {
                return NotFound();
            }
            ViewData["UrunGruplariID"] = new SelectList(_context.UrunGruplari, "Id", "Id", makina.UrunGruplariID);
            return View(makina);
        }

        // POST: Admin/Makina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MakinaAdi,MakinaTanitim,UrunGruplariID,ResimYolu")] Makina makina)
        {
            if (id != makina.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(makina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MakinaExists(makina.Id))
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
            ViewData["UrunGruplariID"] = new SelectList(_context.UrunGruplari, "Id", "Id", makina.UrunGruplariID);
            return View(makina);
        }

        // GET: Admin/Makina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var makina = await _context.Makina
                .Include(m => m.UrunGruplari)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (makina == null)
            {
                return NotFound();
            }

            return View(makina);
        }

        // POST: Admin/Makina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var makina = await _context.Makina.FindAsync(id);
            _context.Makina.Remove(makina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MakinaExists(int id)
        {
            return _context.Makina.Any(e => e.Id == id);
        }
    }
}
