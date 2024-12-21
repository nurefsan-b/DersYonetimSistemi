using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DersYonetimSistemi.Models.Entity;
using System.Threading.Tasks;

namespace DersYonetimSistemi.Controllers
{
    [Route("[controller]")]
    public class OgrenciDersSecimiController : Controller
    {
        private readonly ILogger<OgrenciDersSecimiController> _logger;
        private readonly ProjeİkiBirContext _context;

        public OgrenciDersSecimiController(ILogger<OgrenciDersSecimiController> logger, ProjeİkiBirContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Öğrenci Ders Başvurusu Gönderme
        [HttpPost]
        public async Task<IActionResult> DersSecimi(int ogrenciId, int dersId)
        {
            var ogrenci = await _context.Ogrencis.FirstOrDefaultAsync(s => s.OgrenciID == ogrenciId);
            if (ogrenci == null)
            {
                return NotFound();
            }

            // Başvuru oluşturma
            var ogrenciDersSecimi = new OgrenciDersSecimi
            {
                OgrenciID = ogrenciId,
                DersID = dersId,
                Onay = false,  // Başlangıçta onaylanmamış
            };

            _context.OgrenciDersSecimis.Add(ogrenciDersSecimi);
            await _context.SaveChangesAsync();

            return RedirectToAction("OgrenciAnasayfa", "Ogrenci", new { ogrenciId });
        }

        // Ders Başvurularını Onaylama veya Reddetme
        [Route("Onayla")]
        public async Task<IActionResult> Onayla(int secimId, bool onayDurumu)
        {
            var ogrenciDersSecimi = await _context.OgrenciDersSecimis.FindAsync(secimId);

            if (ogrenciDersSecimi == null)
            {
                return NotFound();
            }

            ogrenciDersSecimi.Onay = onayDurumu;
            await _context.SaveChangesAsync();

            return RedirectToAction("Onaylar", new { AkademisyenId = ogrenciDersSecimi.Ders.AkademisyenID });
        }

        // Anasayfa
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}