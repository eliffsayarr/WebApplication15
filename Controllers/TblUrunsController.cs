using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication15.Models;

namespace WebApplication15.Controllers
{
    public class TblUrunsController : Controller
    {
        private readonly db_urunTakipContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public TblUrunsController(db_urunTakipContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: TblUruns
        public async Task<IActionResult> Index()
        {
            try
            {
                var db_urunTakipContext = _context.TblUruns;
                List<TblUrun> UrunList = await db_urunTakipContext.ToListAsync();

                return View(UrunList);
            }
            catch (Exception err)
            {
                throw;
            }
        }

        // GET: TblUruns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblUruns == null)
            {
                return NotFound();
            }

            var tblUrun = await _context.TblUruns
                .Include(t => t.TblKategoriler)
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (tblUrun == null)
            {
                return NotFound();
            }

            return View(tblUrun);
        }

        // GET: TblUruns/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.TblKategorilers, "KategoriId", "KategoriAd");
            return View();
        }

        // POST: TblUruns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrunId,KategoriId,UrunAd,UrunFiyat,UrunAdet,UrunPhoto,ImageFile,FavoriUrun")] TblUrun tblUrun)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = tblUrun;

                    string wwwrootpath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(tblUrun.ImageFile.FileName);
                    string extension = Path.GetExtension(tblUrun.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    tblUrun.UrunPhoto = "~/Contents/" + fileName;
                    string path = Path.Combine(wwwrootpath + "/Contents/", fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await tblUrun.ImageFile.CopyToAsync(filestream);
                    }
                    _context.Add(tblUrun);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["KategoriId"] = new SelectList(_context.TblKategorilers, "KategoriId", "KategoriId", tblUrun.KategoriId);
                return View(tblUrun);
            }
            catch (Exception err)
            {
                var error = err;
                
                ViewBag.messageString = "Ürün Ekleme Sırasında Hata Oluştu. Lütfen Tekrar Deneyiniz.";
                return View("Information");
            }
           
        }

        // GET: TblUruns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblUruns == null)
            {
                return NotFound();
            }

            var tblUrun = await _context.TblUruns.FindAsync(id);
            if (tblUrun == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.TblKategorilers, "KategoriId", "KategoriAd", tblUrun.KategoriId);
            return View(tblUrun);
        }

        // POST: TblUruns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UrunId,KategoriId,UrunAd,UrunFiyat,UrunAdet,UrunPhoto,ImageFile,FavoriUrun")] TblUrun tblUrun)
        {
            if (id != tblUrun.UrunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUrun = await _context.TblUruns.FindAsync(id);

                    // Check if a new image file is uploaded
                    if (tblUrun.ImageFile != null)
                    {
                        // Delete the old photo file if it exists
                        if (!string.IsNullOrEmpty(existingUrun.UrunPhoto))
                        {
                            var oldFilePath = Path.Combine(_hostEnvironment.WebRootPath, existingUrun.UrunPhoto.TrimStart('~', '/'));
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        
                        string wwwrootpath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(tblUrun.ImageFile.FileName);
                        string extension = Path.GetExtension(tblUrun.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        tblUrun.UrunPhoto = "~/Contents/" + fileName;
                        string path = Path.Combine(wwwrootpath + "/Contents/", fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await tblUrun.ImageFile.CopyToAsync(filestream);
                        }
                    }
                    else
                    {
                        // If no new image file is uploaded, keep the existing photo
                        tblUrun.UrunPhoto = existingUrun.UrunPhoto;
                    }

                    // Update the existing product entity with the new data
                    _context.Entry(existingUrun).CurrentValues.SetValues(tblUrun);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblUrunExists(tblUrun.UrunId))
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
            ViewData["KategoriId"] = new SelectList(_context.TblKategorilers, "KategoriId", "KategoriId", tblUrun.KategoriId);
            return View(tblUrun);
        }



        // GET: TblUruns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblUruns == null)
            {
                return NotFound();
            }

            var tblUrun = await _context.TblUruns
                .Include(t => t.TblKategoriler)
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (tblUrun == null)
            {
                return NotFound();
            }

            return View(tblUrun);
        }

        // POST: TblUruns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblUruns == null)
            {
                return Problem("Entity set 'db_urunTakipContext.TblUruns'  is null.");
            }
            var tblUrun = await _context.TblUruns.FindAsync(id);
            if (tblUrun != null)
            {
                _context.TblUruns.Remove(tblUrun);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblUrunExists(int id)
        {
          return (_context.TblUruns?.Any(e => e.UrunId == id)).GetValueOrDefault();
        }
    }
}
