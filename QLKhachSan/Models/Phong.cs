﻿using System;
using System.Collections.Generic;

namespace QLKhachSan.Models
{
    public partial class Phong
    {
        public Phong()
        {
            Ctanhs = new HashSet<Ctanh>();
            DatPhongs = new HashSet<DatPhong>();
        }

        public int MaPhong { get; set; }
        public int? MaKs { get; set; }
        public string? TenPhong { get; set; }
        public string? TinhTrang { get; set; }
        public string? MaLp { get; set; }
        public string? Anh { get; set; }
        public int? Slvote { get; set; }
        public string? MoTa { get; set; }

        public virtual KhachSan? MaKsNavigation { get; set; }
        public virtual LoaiPhong? MaLpNavigation { get; set; }
        public virtual ICollection<Ctanh> Ctanhs { get; set; }
        public virtual ICollection<DatPhong> DatPhongs { get; set; }
    }
}
