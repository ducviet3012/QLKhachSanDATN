using System;
using System.Collections.Generic;

namespace QLKhachSan.Models
{
    public partial class SuDungThietBi
    {
        public int MaTb { get; set; }
        public string MaLp { get; set; } = null!;

        public virtual LoaiPhong MaLpNavigation { get; set; } = null!;
        public virtual ThietBi MaTbNavigation { get; set; } = null!;
    }
}
