using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KromelSite.Data;
using KromelSite.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace KromelSite.Controllers
{
    public class MakinaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public MakinaController(ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostEnvironment;

        }

        // GET: Makina
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Makina.Include(m => m.UrunGruplari);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Makina/Details/5
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
        
        public IActionResult GrupDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var makinalar =_context.Makina
                .Include(m => m.UrunGruplari)
                .Where(n => n.UrunGruplariID == id);
            if (makinalar == null)
            {
                return NotFound();
            }

            return View(makinalar);
        }

        // GET: Makina/Create
        public IActionResult Create()
        {
            ViewData["UrunGruplariID"] = new SelectList(_context.UrunGruplari, "Id", "Baslik");
            return View();
        }

        // POST: Makina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MakinaAdi,MakinaTanitim,UrunGruplariID,ResimFile")] Makina makina)
        {
            if (ModelState.IsValid)
            {
                string wwwrootPAth = hostingEnvironment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(makina.ResimFile.FileName);
                string extensions = Path.GetExtension(makina.ResimFile.FileName);

                makina.ResimYolu = fileName + DateTime.Now.ToString("yymmssfff") + extensions;

                string path = Path.Combine(wwwrootPAth,"uploads/machines" ,makina.ResimYolu);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await makina.ResimFile.CopyToAsync(fileStream);
                }

                _context.Add(makina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UrunGruplariID"] = new SelectList(_context.UrunGruplari, "Id", "Baslik", makina.UrunGruplariID);
            return View(makina);
        }

        // GET: Makina/Edit/5
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
            ViewData["UrunGruplariID"] = new SelectList(_context.UrunGruplari, "Id", "Baslik", makina.UrunGruplariID);
            return View(makina);
        }

        // POST: Makina/Edit/5
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
            ViewData["UrunGruplariID"] = new SelectList(_context.UrunGruplari, "Id", "Baslik", makina.UrunGruplariID);
            return View(makina);
        }

        // GET: Makina/Delete/5
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

        // POST: Makina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var makina = await _context.Makina.FindAsync(id);

            var path = Path.Combine(hostingEnvironment.WebRootPath, "uploads/machines", makina.ResimYolu);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

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
