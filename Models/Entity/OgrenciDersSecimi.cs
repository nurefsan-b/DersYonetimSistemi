using System;
using System.Collections.Generic;

namespace DersYonetimSistemi.Models.Entity;

public partial class OgrenciDersSecimi
{
    public int SecimId { get; set; }

    public int OgrenciId { get; set; }

    public int DersId { get; set; }

    public bool Onay { get; set; }

    public virtual Ders Ders { get; set; } = null!;

    public virtual Ogrenci Ogrenci { get; set; } = null!;
}
