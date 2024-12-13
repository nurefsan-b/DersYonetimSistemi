using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DersYonetimSistemi.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace DersYonetimSistemi.Controllers
{
    [Route("[controller]")]
    public class AkademisyenController : Controller
    {
        private readonly ILogger<AkademisyenController> _logger;
        private readonly Proje2BirContext _context;

        public AkademisyenController(ILogger<AkademisyenController> logger, Proje2BirContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Akademisyen Ana Sayfası
        public IActionResult Anasayfa(int akademisyenId)
        {
            var akademisyen = _context.Akademisyens
                .Include(a => a.Ogrencis)
                .FirstOrDefault(a => a.AkademisyenId == akademisyenId);

            if (akademisyen == null)
                return NotFound();

            return View(akademisyen);
        }

        // Akademisyen Bilgi Güncelleme
        [HttpGet]
        public IActionResult BilgiGuncelleme(int akademisyenId)
        {
            var akademisyen = _context.Akademisyens.FirstOrDefault(a => a.AkademisyenId == akademisyenId);
            if (akademisyen == null)
                return NotFound();
            
            return View(akademisyen);
        }

        // Bilgi Güncelleme POST
        [HttpPost]
        public IActionResult BilgiGuncelleme(Akademisyen akademisyen)
        {
            if (ModelState.IsValid)
            {
                _context.Update(akademisyen);
                _context.SaveChanges();
                return RedirectToAction("Anasayfa", new { akademisyenId = akademisyen.AkademisyenId });
            }
            return View(akademisyen);
        }

        // Ders işlemleri
        public IActionResult DersIslemleri(int akademisyenId)
        {
            var akademisyen = _context.Akademisyens
                .FirstOrDefault(a => a.AkademisyenId == akademisyenId);

            if (akademisyen == null)
                return NotFound();

            return View(akademisyen);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
