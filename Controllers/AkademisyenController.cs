using Microsoft.AspNetCore.Mvc;
using DersYonetimSistemi.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace DersYonetimSistemi.Controllers
{
    public class AkademisyenController : Controller
    {
        private readonly ProjeİkiBirContext _context;

        public AkademisyenController(ProjeİkiBirContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Anasayfa(int AkademisyenId)
        {
            var akademisyen = await _context.Akademisyens
                .Include(s => s.Ogrencis)
                .ThenInclude(s => s.OgrenciDersSecimis)
                .ThenInclude(s => s.Ders)
                .FirstOrDefaultAsync(s => s.AkademisyenID == AkademisyenId);

            if (akademisyen == null)
            {
                return NotFound();
            }
             ViewBag.AkademisyenId = AkademisyenId;
            return View(akademisyen);
        }
         public async Task<IActionResult> DersIslemleri(int AkademisyenId)
    {
        try
        {
            var dersSecimleri = await _context.OgrenciDersSecimis
                .Include(d => d.Ogrenci)
                .Include(d => d.Ders)
                 .Where(d => d.AkademisyenID == AkademisyenId && d.Onay == true)
                .ToListAsync();
                
                ViewBag.AkademisyenId = AkademisyenId;
            return View(dersSecimleri);
        }
        catch
        {
            return View(new List<OgrenciDersSecimi>());
        }
    }
         [HttpGet]
        public async Task<IActionResult> Onayla(int secimId, bool onayDurumu)
        {
            var ogrenciDersSecimi = await _context.OgrenciDersSecimis
                .Include(d => d.Ders)
                .FirstOrDefaultAsync(d => d.SecimID == secimId);

            if (ogrenciDersSecimi == null)
            {
                return NotFound();
            }
            if (onayDurumu)
             ogrenciDersSecimi.Onay = onayDurumu ? null : false;
             await _context.SaveChangesAsync();
              return RedirectToAction("DersIslemleri", new { AkademisyenId = ogrenciDersSecimi.AkademisyenID });
        }

        public async Task<IActionResult> BilgiGuncelleme(int AkademisyenId)
        {
            var akademisyen = await _context.Akademisyens.FirstOrDefaultAsync(a => a.AkademisyenID == AkademisyenId);

            if (akademisyen == null)
            {
                return NotFound();
            }
            ViewBag.AkademisyenId = AkademisyenId;
            return View(akademisyen);
        }

        [HttpPost]
        public async Task<IActionResult> BilgiGuncelleme(int AkademisyenId, Akademisyen akademisyenModel)
        {
            if (AkademisyenId != akademisyenModel.AkademisyenID)
            {
                return BadRequest();
            }

            var akademisyen = await _context.Akademisyens.FirstOrDefaultAsync(a => a.AkademisyenID == AkademisyenId);

            if (akademisyen == null)
            {
                return NotFound();
            }

            akademisyen.Ad = akademisyenModel.Ad;
            akademisyen.Soyad = akademisyenModel.Soyad;
            akademisyen.Email = akademisyenModel.Email;
            akademisyen.Sifre = akademisyenModel.Sifre;

            await _context.SaveChangesAsync();

            return RedirectToAction("Anasayfa", new { AkademisyenId = akademisyen.AkademisyenID });
        }
         public IActionResult CikisYap()
    {
        return RedirectToAction("Index", "Home");
    }
    }
}