using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DersYonetimSistemi.Models.Entity
{
    public partial class OgrenciDersSecimi
    {
        [Key]
        [Column("SecimID")]
        public int SecimID { get; set; }

        [Column("OgrenciID")]
        public int OgrenciID { get; set; }

        [Column("DersID")]
        public int DersID { get; set; }

        [Column("Onay")]
        public bool? Onay { get; set; }

        [Column("AkademisyenID")]
        public int AkademisyenID { get; set; }

        public virtual Akademisyen Akademisyen { get; set; } = null!;

        public virtual Ders Ders { get; set; } = null!;

        public virtual Ogrenci Ogrenci { get; set; } = null!;
    }
}