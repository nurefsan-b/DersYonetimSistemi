using System;
using System.Collections.Generic;

namespace DersYonetimSistemi.Models.Entity;

public partial class Ders
{
    public int DersId { get; set; }

    public string DersKod { get; set; } = null!;

    public string DersAd { get; set; } = null!;

    public bool ZorunluMu { get; set; }

    public int Kredi { get; set; }

    public virtual ICollection<Akademisyen> Akademisyens { get; set; } = new List<Akademisyen>();

    public virtual ICollection<OgrenciDersSecimi> OgrenciDersSecimis { get; set; } = new List<OgrenciDersSecimi>();
}
