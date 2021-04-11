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
    public class GruplarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GruplarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UrunGruplari
        public async Task<IActionResult> Index()
        {
            return View(await _context.UrunGruplari.ToListAsync());
        }

        // GET: Admin/UrunGruplari/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urunGruplari = await _context.UrunGruplari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunGruplari == null)
            {
                return NotFound();
            }

            return View(urunGruplari);
        }

        // GET: Admin/UrunGruplari/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/UrunGruplari/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Baslik,Tanim,ResimYolu")] UrunGruplari urunGruplari)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urunGruplari);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(urunGruplari);
        }

        // GET: Admin/UrunGruplari/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urunGruplari = await _context.UrunGruplari.FindAsync(id);
            if (urunGruplari == null)
            {
                return NotFound();
            }
            return View(urunGruplari);
        }

        // POST: Admin/UrunGruplari/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Baslik,Tanim,ResimYolu")] UrunGruplari urunGruplari)
        {
            if (id != urunGruplari.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urunGruplari);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunGruplariExists(urunGruplari.Id))
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
            return View(urunGruplari);
        }

        // GET: Admin/UrunGruplari/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urunGruplari = await _context.UrunGruplari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunGruplari == null)
            {
                return NotFound();
            }

            return View(urunGruplari);
        }

        // POST: Admin/UrunGruplari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var urunGruplari = await _context.UrunGruplari.FindAsync(id);
            _context.UrunGruplari.Remove(urunGruplari);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunGruplariExists(int id)
        {
            return _context.UrunGruplari.Any(e => e.Id == id);
        }
    }
}
