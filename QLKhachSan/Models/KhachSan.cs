using System;
using System.Collections.Generic;

namespace QLKhachSan.Models
{
    public partial class KhachSan
    {
        public KhachSan()
        {
            LoaiPhongs = new HashSet<LoaiPhong>();
            Phongs = new HashSet<Phong>();
        }

        public int MaKs { get; set; }
        public int? MaTinh { get; set; }
        public string? TenKhachSan { get; set; }
        public string? DiaChi { get; set; }
        public string? MoTa { get; set; }
        public string? Anh { get; set; }
        public int? DanhGia { get; set; }

        public virtual TinhThanh? MaTinhNavigation { get; set; }
        public virtual ICollection<LoaiPhong> LoaiPhongs { get; set; }
        public virtual ICollection<Phong> Phongs { get; set; }
    }
}
