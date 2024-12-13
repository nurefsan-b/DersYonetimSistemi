using Microsoft.AspNetCore.Mvc;
using DersYonetimSistemi.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace DersYonetimSistemi.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly Proje2BirContext _context;

        public OgrenciController(Proje2BirContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Anasayfa(int ogrenciId)
        {
            var ogrenci = await _context.Ogrencis
                .Include(s => s.Akademisyen)
                .Include(s => s.OgrenciDersSecimis)
                .ThenInclude(sc => sc.Ders)
                .FirstOrDefaultAsync(s => s.OgrenciId == ogrenciId);

            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }

        public async Task<IActionResult> DersSecimi(int ogrenciId)
        {
            var ogrenci = await _context.Ogrencis
                .Include(s => s.OgrenciDersSecimis)
                .ThenInclude(sc => sc.Ders)
                .FirstOrDefaultAsync(s => s.OgrenciId == ogrenciId);

            if (ogrenci == null)
            {
                return NotFound();
            }

            var dersler = await _context.Ders.ToListAsync();
            ViewBag.Dersler = dersler;

            return View(ogrenci);
        }

       
        public async Task<IActionResult> BilgiGuncelleme(int ogrenciId)
        {
            var ogrenci = await _context.Ogrencis.FirstOrDefaultAsync(s => s.OgrenciId == ogrenciId);

            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }

        [HttpPost]
        public async Task<IActionResult> BilgiGuncelleme(int ogrenciId, Ogrenci ogrenciModel)
        {
            if (ogrenciId != ogrenciModel.OgrenciId)
            {
                return BadRequest();
            }

            var ogrenci = await _context.Ogrencis.FirstOrDefaultAsync(s => s.OgrenciId == ogrenciId);

            if (ogrenci == null)
            {
                return NotFound();
            }

            ogrenci.Ad = ogrenciModel.Ad;
            ogrenci.Soyad = ogrenciModel.Soyad;
            ogrenci.Email = ogrenciModel.Email;
            ogrenci.Sifre = ogrenciModel.Sifre;

            await _context.SaveChangesAsync();

            return RedirectToAction("Anasayfa", new { ogrenciId = ogrenci.OgrenciId });
        }
    }
}
