using System;
using System.Collections.Generic;

namespace QLKhachSan.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            DatPhongs = new HashSet<DatPhong>();
            SuDungDichVus = new HashSet<SuDungDichVu>();
        }

        public int SoHoaDon { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public int? MaNv { get; set; }
        public int? MaKh { get; set; }
        public string? TenKh { get; set; }
        public string? Email { get; set; }
        public string? Sdt { get; set; }

        public virtual KhachHang? MaKhNavigation { get; set; }
        public virtual NhanVien? MaNvNavigation { get; set; }
        public virtual ICollection<DatPhong> DatPhongs { get; set; }
        public virtual ICollection<SuDungDichVu> SuDungDichVus { get; set; }
    }
}
