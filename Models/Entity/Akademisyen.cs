using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace DersYonetimSistemi.Models.Entity
{
    public class Akademisyen
    {
        [Key]
        [Column("AkademisyenID")]
        public int AkademisyenID { get; set; }

        [Column("Ad")]
        public string? Ad { get; set; }

        [Column("Soyad")]
        public string? Soyad { get; set; }

        [Column("Email")]
        public string Email { get; set; } = null!;

        [Column("Sifre")]
        public int Sifre { get; set; } 

        // Navigation properties
         public ICollection<Ogrenci> Ogrencis { get; set; } = new List<Ogrenci>();
        public virtual ICollection<Ders> Dersler { get; set; } = new List<Ders>();
    }
}