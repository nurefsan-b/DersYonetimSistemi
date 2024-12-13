using System;
using System.Collections.Generic;

namespace DersYonetimSistemi.Models.Entity;

public partial class Ogrenci
{
    public int OgrenciId { get; set; }

    public string Ad { get; set; } = null!;

    public string Soyad { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sifre { get; set; } = null!;

    public int? AkademisyenId { get; set; }

    public virtual Akademisyen? Akademisyen { get; set; }

    public virtual ICollection<OgrenciDersSecimi> OgrenciDersSecimis { get; set; } = new List<OgrenciDersSecimi>();
}
