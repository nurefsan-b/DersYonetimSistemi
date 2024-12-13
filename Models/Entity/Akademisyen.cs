using System;
using System.Collections.Generic;

namespace DersYonetimSistemi.Models.Entity;

public partial class Akademisyen
{
    public int AkademisyenId { get; set; }

    public string? Ad { get; set; }

    public string? Soyad { get; set; }

    public string? Unvan { get; set; }

    public string Email { get; set; } = null!;

    public string Sifre { get; set; } = null!;

    public int? DersId { get; set; }

    public virtual Ders? Ders { get; set; }

    public virtual ICollection<Ogrenci> Ogrencis { get; set; } = new List<Ogrenci>();
}
