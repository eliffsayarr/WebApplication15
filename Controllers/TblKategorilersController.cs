using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication15.Models;

namespace WebApplication15.Controllers
{
    public class TblKategorilersController : Controller
    {
        private readonly db_urunTakipContext _context;

        public TblKategorilersController(db_urunTakipContext context)
        {
            _context = context;
        }

        // GET: TblKategorilers
        public async Task<IActionResult> Index()
        {
              return _context.TblKategorilers != null ? 
                          View(await _context.TblKategorilers.ToListAsync()) :
                          Problem("Entity set 'db_urunTakipContext.TblKategorilers'  is null.");
        }

        // GET: TblKategorilers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblKategorilers == null)
            {
                return NotFound();
            }

            var tblKategoriler = await _context.TblKategorilers
                .FirstOrDefaultAsync(m => m.KategoriId == id);
            if (tblKategoriler == null)
            {
                return NotFound();
            }

            return View(tblKategoriler);
        }

        // GET: TblKategorilers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblKategorilers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategoriId,KategoriAd")] TblKategoriler tblKategoriler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblKategoriler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblKategoriler);
        }

        // GET: TblKategorilers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblKategorilers == null)
            {
                return NotFound();
            }

            var tblKategoriler = await _context.TblKategorilers.FindAsync(id);
            if (tblKategoriler == null)
            {
                return NotFound();
            }
            return View(tblKategoriler);
        }

        // POST: TblKategorilers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KategoriId,KategoriAd")] TblKategoriler tblKategoriler)
        {
            if (id != tblKategoriler.KategoriId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblKategoriler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblKategorilerExists(tblKategoriler.KategoriId))
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
            return View(tblKategoriler);
        }

        // GET: TblKategorilers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblKategorilers == null)
            {
                return NotFound();
            }

            var tblKategoriler = await _context.TblKategorilers
                .FirstOrDefaultAsync(m => m.KategoriId == id);
            if (tblKategoriler == null)
            {
                return NotFound();
            }

            return View(tblKategoriler);
        }

        // POST: TblKategorilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblKategorilers == null)
            {
                return Problem("Entity set 'db_urunTakipContext.TblKategorilers'  is null.");
            }
            var tblKategoriler = await _context.TblKategorilers.FindAsync(id);
            if (tblKategoriler != null)
            {
                _context.TblKategorilers.Remove(tblKategoriler);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                ViewBag.messageString = tblKategoriler.KategoriAd.ToString() +
                    " Kategorisinde Ürün Girişi Mevcuttur. Kategori Silinemez ";
                return View("Information");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TblKategorilerExists(int id)
        {
          return (_context.TblKategorilers?.Any(e => e.KategoriId == id)).GetValueOrDefault();
        }
    }
}
