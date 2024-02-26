using Microsoft.AspNetCore.Mvc;
using QLKhachSan.Models;
using QLKhachSan.ViewModels;

namespace QLKhachSan.ViewComponents
{
    public class KhachSanViewComponent:ViewComponent
    {
        private readonly QLKhachSanTTTNContext db;
        public KhachSanViewComponent(QLKhachSanTTTNContext context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.KhachSans.Select(ks => new KhachSanVM
            {
                MaKS = ks.MaKs,
                TenKS = ks.TenKhachSan,
                Anh = ks.Anh,
                DiaChi = ks.DiaChi
            }).OrderBy(p => p.TenKS);
            return View(data);
        }
    }
}
