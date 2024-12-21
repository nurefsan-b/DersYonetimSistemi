using Microsoft.AspNetCore.Mvc;
using DersYonetimSistemi.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DersYonetimSistemi.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly ProjeİkiBirContext _context;

        public OgrenciController(ProjeİkiBirContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Anasayfa(int ogrenciId)
        {
            var ogrenci = await _context.Ogrencis
                .Include(s => s.Akademisyen)
                .Include(s => s.OgrenciDersSecimis)
                .ThenInclude(sc => sc.Ders)
                .FirstOrDefaultAsync(s => s.OgrenciID == ogrenciId);

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
                .FirstOrDefaultAsync(s => s.OgrenciID == ogrenciId);

            if (ogrenci == null)
            {
                return NotFound();
            }

            var dersler = await _context.Dersler.ToListAsync();
            ViewBag.Dersler = dersler;

            return View(ogrenci);
        }
          
        public async Task<IActionResult> DersSecimiGet(int ogrenciId, int dersId){
            
            var ogrenci = await _context.Ogrencis
                .Include(s => s.OgrenciDersSecimis)
                .ThenInclude(sc => sc.Ders)
                .FirstOrDefaultAsync(s => s.OgrenciID == ogrenciId);

            if (ogrenci == null)
            {
                return NotFound();
            }

            var dersler = await _context.Dersler.ToListAsync();
            ViewBag.Dersler = dersler;

            return View(ogrenci);
          }    
          [HttpPost]
        public async Task<IActionResult> DersSecimi(int ogrenciId, int dersId)
        {
              var ders = await _context.Dersler.FirstOrDefaultAsync(d => d.DersID == dersId);
            if (ders == null)
            {
                return NotFound();
            }

            var dersSecimi = new OgrenciDersSecimi
            {
                OgrenciID = ogrenciId,
                DersID = dersId,
                AkademisyenID = ders.AkademisyenID,  
                Onay = true
            };

            _context.OgrenciDersSecimis.Add(dersSecimi);
            await _context.SaveChangesAsync();

            return RedirectToAction("Anasayfa", new { ogrenciId });
        }
        public async Task<IActionResult> BilgiGuncelleme(int ogrenciId)
        {
            var ogrenci = await _context.Ogrencis.FirstOrDefaultAsync(s => s.OgrenciID == ogrenciId);

            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }

        [HttpPost]
        public async Task<IActionResult> BilgiGuncelleme(int ogrenciId, Ogrenci ogrenciModel)
        {
            if (ogrenciId != ogrenciModel.OgrenciID)
            {
                 ModelState.AddModelError("", "Öğrenci ID'leri eşleşmiyor. Lütfen tekrar kontrol edin.");
                 return View(ogrenciModel);
            
            }

            // Veritabanından öğrenciyi bul
            var ogrenci = await _context.Ogrencis.FirstOrDefaultAsync(s => s.OgrenciID == ogrenciId);

            if (ogrenci == null)
            {
                return NotFound("Öğrenci bulunamadı.");
            }

            try
            {
                // Model'den gelen bilgileri veritabanındaki nesneye aktar
                ogrenci.Ad = ogrenciModel.Ad;
                ogrenci.Soyad = ogrenciModel.Soyad;
                ogrenci.Email = ogrenciModel.Email;
                ogrenci.Sifre = ogrenciModel.Sifre;

                // Değişiklikleri kaydet
                await _context.SaveChangesAsync();

                return RedirectToAction("Anasayfa", new { ogrenciId = ogrenci.OgrenciID });
            }
            catch (DbUpdateException ex)
            {
                // Hata durumunda kullanıcıya bilgi ver
                ModelState.AddModelError("", $"Bilgiler güncellenirken bir hata oluştu: {ex.Message}");
                return View(ogrenciModel);
            }
        }
    }
}
