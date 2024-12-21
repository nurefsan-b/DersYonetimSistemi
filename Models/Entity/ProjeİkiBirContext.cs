using Microsoft.EntityFrameworkCore;

namespace DersYonetimSistemi.Models.Entity
{
    public partial class ProjeİkiBirContext : DbContext
    {
        public ProjeİkiBirContext()
        {
        }

        public ProjeİkiBirContext(DbContextOptions<ProjeİkiBirContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Akademisyen> Akademisyens { get; set; } = null!;
        public virtual DbSet<Ders> Dersler { get; set; } = null!;
        public virtual DbSet<Ogrenci> Ogrencis { get; set; } = null!;
        public virtual DbSet<OgrenciDersSecimi> OgrenciDersSecimis { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NE08S3O;Initial Catalog=ProjeİkiBir;Integrated Security=True;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ogrenci>(entity =>
            {
                entity.HasKey(e => e.OgrenciID).HasName("PK__Ogrenci__B02818F78473DD28");

                entity.ToTable("Ogrenci");

                entity.Property(e => e.OgrenciID).HasColumnName("OgrenciID");
                entity.Property(e => e.Ad).HasColumnName("Ad");
                entity.Property(e => e.Soyad).HasColumnName("Soyad");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.Sifre).HasColumnName("Sifre");

                entity.HasOne(d => d.Akademisyen).WithMany(p => p.Ogrencis)
                    .HasForeignKey(d => d.AkademisyenID)
                    .HasConstraintName("FK__Ogrenci__Akademi__4222D4EF");
            });

            modelBuilder.Entity<Ders>(entity =>
            {
                entity.HasKey(e => e.DersID).HasName("PK__Ders__B02818F78473DD28");

                entity.ToTable("Ders");

                entity.Property(e => e.DersID).HasColumnName("DersID");
                entity.Property(e => e.DersAd).HasColumnName("DersAd");
                entity.Property(e => e.DersKod).HasColumnName("DersKod");
                entity.Property(e => e.Kredi).HasColumnName("Kredi");
                entity.Property(e => e.AkademisyenID).HasColumnName("AkademisyenID");

                entity.HasOne(d => d.Akademisyen)
                    .WithMany(a => a.Dersler)
                    .HasForeignKey(d => d.AkademisyenID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ders__AkademisyenID__44FF419A");
            });

            modelBuilder.Entity<Akademisyen>(entity =>
            {
                entity.HasKey(e => e.AkademisyenID).HasName("PK__Akademisyen__B02818F78473DD28");

                entity.ToTable("Akademisyen");

                entity.Property(e => e.AkademisyenID).HasColumnName("AkademisyenID");
                entity.Property(e => e.Ad).HasColumnName("Ad");
                entity.Property(e => e.Soyad).HasColumnName("Soyad");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.Sifre).HasColumnName("Sifre");
            });

            modelBuilder.Entity<OgrenciDersSecimi>(entity =>
            {
                entity.HasKey(e => e.SecimID).HasName("PK__OgrenciD__B02818F78473DD28");

                entity.ToTable("OgrenciDersSecimi");

                entity.Property(e => e.SecimID).HasColumnName("SecimID");
                entity.Property(e => e.DersID).HasColumnName("DersID");
                entity.Property(e => e.OgrenciID).HasColumnName("OgrenciID");

                entity.HasOne(d => d.Ders).WithMany(p => p.OgrenciDersSecimis)
                    .HasForeignKey(d => d.DersID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OgrenciDe__DersI__45F365D3");

                entity.HasOne(d => d.Ogrenci).WithMany(p => p.OgrenciDersSecimis)
                    .HasForeignKey(d => d.OgrenciID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OgrenciDe__Ogren__44FF419A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}