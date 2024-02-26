using System;
using System.Collections.Generic;

namespace QLKhachSan.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            Blogs = new HashSet<Blog>();
            HoaDons = new HashSet<HoaDon>();
        }

        public int MaNv { get; set; }
        public string? TenNv { get; set; }
        public int? GioiTinh { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Cccd { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? Sdt { get; set; }
        public string? ChucVu { get; set; }
        public string? Anh { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
