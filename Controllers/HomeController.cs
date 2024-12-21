using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DersYonetimSistemi.Models;
using DersYonetimSistemi.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DersYonetimSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProjeİkiBirContext _context;

        public HomeController(ILogger<HomeController> logger, ProjeİkiBirContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Email, int password)
        {
            try
            {
                // Check Ogrenci
                var ogrenci = await _context.Ogrencis
                    .Include(o => o.Akademisyen)
                    .FirstOrDefaultAsync(o => o.Email == Email && o.Sifre == password);

                if (ogrenci != null)
                {
                    return RedirectToAction("Anasayfa", "Ogrenci", new { ogrenciId = ogrenci.OgrenciID });
                }

                // Check Akademisyen
                var akademisyen = await _context.Akademisyens
                    .FirstOrDefaultAsync(a => a.Email == Email && a.Sifre == password);

                if (akademisyen != null)
                {
                    return RedirectToAction("Anasayfa", "Akademisyen", new { AkademisyenId = akademisyen.AkademisyenID });
                }

                TempData["ErrorMessage"] = "Geçersiz email veya şifre";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login hatası");
                TempData["ErrorMessage"] = "Bir hata oluştu";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}