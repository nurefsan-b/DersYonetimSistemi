using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DersYonetimSistemi.Models;
using DersYonetimSistemi.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace DersYonetimSistemi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Proje2BirContext _context;

    public HomeController(ILogger<HomeController> logger, Proje2BirContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
        public async Task<IActionResult> Index(string Email, string password)
        {
            var ogrenci = await _context.Ogrencis
                .Include(s => s.Akademisyen)
                .Include(s => s.OgrenciDersSecimis)
                .ThenInclude(sc => sc.Ders)
                .FirstOrDefaultAsync(s => s.Email == Email && s.Sifre == password);
            
            var akademisyen = await _context.Akademisyens
        .Include(i => i.Ogrencis)  
        .ThenInclude(i => i.OgrenciDersSecimis)  
        .ThenInclude(od => od.Ders)  
        .FirstOrDefaultAsync(a => a.Email == Email && a.Sifre == password); 
            if (ogrenci != null)
            {
                return RedirectToAction("Anasayfa", "Ogrenci", new { ogrenciId = ogrenci.OgrenciId });
            }
            else if(akademisyen != null)
            {
                return RedirectToAction("Anasayfa", "Akademisyen", new { akademisyenId = akademisyen.AkademisyenId});
            }
            else 
            {
            ViewBag.Error = "Invalid login attempt.";
            return View();
            }
        }

    public IActionResult Welcome()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
