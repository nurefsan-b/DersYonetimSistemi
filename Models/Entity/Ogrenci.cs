using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace DersYonetimSistemi.Models.Entity
{
    public class Ogrenci
    {
        [Key]
        [Column("OgrenciID")]
        public int OgrenciID { get; set; }

        [Column("Ad")]
        public string Ad { get; set; } = null!;

        [Column("Soyad")]
        public string Soyad { get; set; } = null!;

        [Column("Email")]
        public string Email { get; set; } = null!;

        [Column("Sifre")]
        public int Sifre { get; set; } 
        public int AkademisyenID { get; set; }
        [ForeignKey("AkademisyenID")]

        // Navigation properties
        public virtual Akademisyen Akademisyen { get; set; } = null!;
        public virtual ICollection<OgrenciDersSecimi> OgrenciDersSecimis { get; set; } = new List<OgrenciDersSecimi>();
    }
}