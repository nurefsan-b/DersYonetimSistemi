using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DersYonetimSistemi.Models.Entity;

public partial class Proje2BirContext : DbContext
{
    public Proje2BirContext()
    {
    }

    public Proje2BirContext(DbContextOptions<Proje2BirContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Akademisyen> Akademisyens { get; set; }

    public virtual DbSet<Ders> Ders { get; set; }

    public virtual DbSet<Ogrenci> Ogrencis { get; set; }

    public virtual DbSet<OgrenciDersSecimi> OgrenciDersSecimis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NE08S3O;Initial Catalog=Proje2Bir;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Akademisyen>(entity =>
        {
            entity.HasKey(e => e.AkademisyenId).HasName("PK__Akademis__3540887E225004E3");

            entity.ToTable("Akademisyen");

            entity.HasIndex(e => e.Email, "UQ__Akademis__A9D1053405723537").IsUnique();

            entity.Property(e => e.AkademisyenId).HasColumnName("AkademisyenID");
            entity.Property(e => e.Ad).HasMaxLength(50);
            entity.Property(e => e.DersId).HasColumnName("DersID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Sifre).HasMaxLength(50);
            entity.Property(e => e.Soyad).HasMaxLength(50);
            entity.Property(e => e.Unvan).HasMaxLength(50);

            entity.HasOne(d => d.Ders).WithMany(p => p.Akademisyens)
                .HasForeignKey(d => d.DersId)
                .HasConstraintName("FK__Akademisy__DersI__3E52440B");
        });

        modelBuilder.Entity<Ders>(entity =>
        {
            entity.HasKey(e => e.DersId).HasName("PK__Ders__E8B3DE713D195A8D");

            entity.Property(e => e.DersId).HasColumnName("DersID");
            entity.Property(e => e.DersAd).HasMaxLength(50);
            entity.Property(e => e.DersKod).HasMaxLength(50);
        });

        modelBuilder.Entity<Ogrenci>(entity =>
        {
            entity.HasKey(e => e.OgrenciId).HasName("PK__Ogrenci__E497E6D44994106E");

            entity.ToTable("Ogrenci");

            entity.HasIndex(e => e.Email, "UQ__Ogrenci__A9D105342040BD28").IsUnique();

            entity.Property(e => e.OgrenciId).HasColumnName("OgrenciID");
            entity.Property(e => e.Ad).HasMaxLength(50);
            entity.Property(e => e.AkademisyenId).HasColumnName("AkademisyenID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Sifre).HasMaxLength(50);
            entity.Property(e => e.Soyad).HasMaxLength(50);

            entity.HasOne(d => d.Akademisyen).WithMany(p => p.Ogrencis)
                .HasForeignKey(d => d.AkademisyenId)
                .HasConstraintName("FK__Ogrenci__Akademi__4222D4EF");
        });

        modelBuilder.Entity<OgrenciDersSecimi>(entity =>
        {
            entity.HasKey(e => e.SecimId).HasName("PK__OgrenciD__B02818F78473DD28");

            entity.ToTable("OgrenciDersSecimi");

            entity.Property(e => e.SecimId).HasColumnName("SecimID");
            entity.Property(e => e.DersId).HasColumnName("DersID");
            entity.Property(e => e.OgrenciId).HasColumnName("OgrenciID");

            entity.HasOne(d => d.Ders).WithMany(p => p.OgrenciDersSecimis)
                .HasForeignKey(d => d.DersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OgrenciDe__DersI__45F365D3");

            entity.HasOne(d => d.Ogrenci).WithMany(p => p.OgrenciDersSecimis)
                .HasForeignKey(d => d.OgrenciId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OgrenciDe__Ogren__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
