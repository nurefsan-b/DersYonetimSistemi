using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace DersYonetimSistemi.Models.Entity
{
    public class Ders
    {
        [Key]
        [Column("DersID")]
        public int DersID { get; set; }

        [Column("DersKod")]
        public string DersKod { get; set; } = null!;

        [Column("DersAd")]
        public string DersAd { get; set; } = null!;

        [Column("Kredi")]
        public int Kredi { get; set; }

        [Column("AkademisyenID")]
        public int AkademisyenID { get; set; }

        // Navigation properties
        public virtual Akademisyen Akademisyen { get; set; } = null!;
        public virtual ICollection<OgrenciDersSecimi> OgrenciDersSecimis { get; set; } = new List<OgrenciDersSecimi>();
    }
}