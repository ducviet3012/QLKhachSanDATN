using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QLKhachSan.Models
{
    public partial class QLKhachSanTTTNContext : DbContext
    {
        public QLKhachSanTTTNContext()
        {
        }

        public QLKhachSanTTTNContext(DbContextOptions<QLKhachSanTTTNContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<Csvc> Csvcs { get; set; } = null!;
        public virtual DbSet<Ctanh> Ctanhs { get; set; } = null!;
        public virtual DbSet<DatPhong> DatPhongs { get; set; } = null!;
        public virtual DbSet<DichVu> DichVus { get; set; } = null!;
        public virtual DbSet<GopY> Gopies { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<KhachSan> KhachSans { get; set; } = null!;
        public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; } = null!;
        public virtual DbSet<LoaiUser> LoaiUsers { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<Phong> Phongs { get; set; } = null!;
        public virtual DbSet<SuDungDichVu> SuDungDichVus { get; set; } = null!;
        public virtual DbSet<SuDungThietBi> SuDungThietBis { get; set; } = null!;
        public virtual DbSet<TDoanhThu> TDoanhThus { get; set; } = null!;
        public virtual DbSet<ThietBi> ThietBis { get; set; } = null!;
        public virtual DbSet<TinhThanh> TinhThanhs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=VIET\\SQLEXPRESS;Initial Catalog=QLKhachSanTTTN;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasKey(e => e.Idblog);

                entity.ToTable("Blog");

                entity.Property(e => e.Idblog)
                    .HasMaxLength(10)
                    .HasColumnName("IDblog");

                entity.Property(e => e.Anh).HasMaxLength(50);

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.NgayDang).HasColumnType("date");

                entity.Property(e => e.ThongTin).HasColumnType("text");

                entity.Property(e => e.TieuDe).HasMaxLength(100);

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_Blog_NhanVien");
            });

            modelBuilder.Entity<Csvc>(entity =>
            {
                entity.ToTable("CSVC");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<Ctanh>(entity =>
            {
                entity.HasKey(e => e.TenAnh);

                entity.ToTable("CTAnh");

                entity.Property(e => e.TenAnh).HasMaxLength(50);

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.Ctanhs)
                    .HasForeignKey(d => d.MaPhong)
                    .HasConstraintName("FK_CTAnh_Phong");
            });

            modelBuilder.Entity<DatPhong>(entity =>
            {
                entity.HasKey(e => new { e.MaPhong, e.SoHoaDon });

                entity.ToTable("DatPhong");

                entity.Property(e => e.NgayDen).HasColumnType("date");

                entity.Property(e => e.NgayDi).HasColumnType("date");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.DatPhongs)
                    .HasForeignKey(d => d.MaPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DatPhong_Phong");

                entity.HasOne(d => d.SoHoaDonNavigation)
                    .WithMany(p => p.DatPhongs)
                    .HasForeignKey(d => d.SoHoaDon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DatPhong_HoaDon");
            });

            modelBuilder.Entity<DichVu>(entity =>
            {
                entity.HasKey(e => e.MaDv);

                entity.ToTable("DichVu");

                entity.Property(e => e.MaDv)
                    .HasMaxLength(10)
                    .HasColumnName("MaDV");

                entity.Property(e => e.Anh).HasMaxLength(30);

                entity.Property(e => e.TenDv)
                    .HasMaxLength(30)
                    .HasColumnName("TenDV");

                entity.Property(e => e.ThongTin).HasMaxLength(300);
            });

            modelBuilder.Entity<GopY>(entity =>
            {
                entity.ToTable("GopY");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.MaKs).HasColumnName("MaKS");

                entity.Property(e => e.NgayComment).HasColumnType("date");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Gopies)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_GopY_KhachHang");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.SoHoaDon);

                entity.ToTable("HoaDon");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.NgayThanhToan).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(50)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_HoaDon_KhachHang");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_HoaDon_NhanVien");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.ToTable("KhachHang");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(50)
                    .HasColumnName("CCCD");

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LoaiKhachHang).HasMaxLength(50);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(50)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.KhachHangs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_KhachHang_User");
            });

            modelBuilder.Entity<KhachSan>(entity =>
            {
                entity.HasKey(e => e.MaKs);

                entity.ToTable("KhachSan");

                entity.Property(e => e.MaKs).HasColumnName("MaKS");

                entity.Property(e => e.Anh).HasMaxLength(50);

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.TenKhachSan).HasMaxLength(50);

                entity.HasOne(d => d.MaTinhNavigation)
                    .WithMany(p => p.KhachSans)
                    .HasForeignKey(d => d.MaTinh)
                    .HasConstraintName("FK_KhachSan_TinhThanh");
            });

            modelBuilder.Entity<LoaiPhong>(entity =>
            {
                entity.HasKey(e => e.MaLp);

                entity.ToTable("LoaiPhong");

                entity.Property(e => e.MaLp)
                    .HasMaxLength(10)
                    .HasColumnName("MaLP");

                entity.Property(e => e.Anh).HasMaxLength(50);

                entity.Property(e => e.KichThuoc).HasMaxLength(20);

                entity.Property(e => e.TenLp)
                    .HasMaxLength(50)
                    .HasColumnName("TenLP");

                entity.Property(e => e.ThongTin).HasMaxLength(100);
            });

            modelBuilder.Entity<LoaiUser>(entity =>
            {
                entity.HasKey(e => e.IdLoaiUser);

                entity.ToTable("LoaiUser");

                entity.Property(e => e.TenLoai).HasMaxLength(50);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNv);

                entity.ToTable("NhanVien");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.Anh).HasMaxLength(50);

                entity.Property(e => e.Cccd)
                    .HasMaxLength(50)
                    .HasColumnName("CCCD");

                entity.Property(e => e.ChucVu).HasMaxLength(50);

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(50)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenNv)
                    .HasMaxLength(50)
                    .HasColumnName("TenNV");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_NhanVien_User");
            });

            modelBuilder.Entity<Phong>(entity =>
            {
                entity.HasKey(e => e.MaPhong);

                entity.ToTable("Phong");

                entity.Property(e => e.Anh).HasMaxLength(50);

                entity.Property(e => e.MaKs).HasColumnName("MaKS");

                entity.Property(e => e.MaLp)
                    .HasMaxLength(10)
                    .HasColumnName("MaLP");

                entity.Property(e => e.TenPhong).HasMaxLength(50);

                entity.HasOne(d => d.MaKsNavigation)
                    .WithMany(p => p.Phongs)
                    .HasForeignKey(d => d.MaKs)
                    .HasConstraintName("FK_Phong_KhachSan");

                entity.HasOne(d => d.MaLpNavigation)
                    .WithMany(p => p.Phongs)
                    .HasForeignKey(d => d.MaLp)
                    .HasConstraintName("FK_Phong_LoaiPhong");
            });

            modelBuilder.Entity<SuDungDichVu>(entity =>
            {
                entity.HasKey(e => new { e.MaDv, e.SoHoaDon })
                    .HasName("PK_SuDungDichVu_1");

                entity.ToTable("SuDungDichVu");

                entity.Property(e => e.MaDv)
                    .HasMaxLength(10)
                    .HasColumnName("MaDV");

                entity.Property(e => e.NgayMua).HasColumnType("date");

                entity.HasOne(d => d.MaDvNavigation)
                    .WithMany(p => p.SuDungDichVus)
                    .HasForeignKey(d => d.MaDv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SuDungDichVu_DichVu");

                entity.HasOne(d => d.SoHoaDonNavigation)
                    .WithMany(p => p.SuDungDichVus)
                    .HasForeignKey(d => d.SoHoaDon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SuDungDichVu_HoaDon");
            });

            modelBuilder.Entity<SuDungThietBi>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SuDungThietBi");

                entity.Property(e => e.MaLp)
                    .HasMaxLength(10)
                    .HasColumnName("MaLP");

                entity.Property(e => e.MaTb).HasColumnName("MaTB");

                entity.HasOne(d => d.MaLpNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaLp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SuDungThietBi_LoaiPhong");

                entity.HasOne(d => d.MaTbNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaTb)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SuDungThietBi_ThietBi");
            });

            modelBuilder.Entity<TDoanhThu>(entity =>
            {
                entity.ToTable("tDoanhThu");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<ThietBi>(entity =>
            {
                entity.HasKey(e => e.MaTb);

                entity.ToTable("ThietBi");

                entity.Property(e => e.MaTb).HasColumnName("MaTB");

                entity.Property(e => e.TenTb)
                    .HasMaxLength(50)
                    .HasColumnName("TenTB");
            });

            modelBuilder.Entity<TinhThanh>(entity =>
            {
                entity.HasKey(e => e.MaTinh);

                entity.ToTable("TinhThanh");

                entity.Property(e => e.Anh).HasMaxLength(50);

                entity.Property(e => e.TenTinh).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.HasOne(d => d.IdLoaiUserNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdLoaiUser)
                    .HasConstraintName("FK_User_LoaiUser");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
