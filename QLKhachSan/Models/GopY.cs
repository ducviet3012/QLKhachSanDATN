using System;
using System.Collections.Generic;

namespace QLKhachSan.Models
{
    public partial class GopY
    {
        public int Id { get; set; }
        public int? MaKh { get; set; }
        public string? MaPhong { get; set; }
        public int? Vote { get; set; }
        public string? Comment { get; set; }

        public virtual KhachHang? MaKhNavigation { get; set; }
    }
}
