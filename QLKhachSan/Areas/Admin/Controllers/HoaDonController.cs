using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QLKhachSan.Models;
using QLKhachSan.ViewModels;
using QLKhachSan.ViewModels.Admin;
using X.PagedList;

namespace QLKhachSan.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class HoaDonController : Controller
    {
        QLKhachSanTTTNContext db = new QLKhachSanTTTNContext();
        [Route("hoadon")]
        [CustomAuthorize]
        public IActionResult HoaDon(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var hoadon = (from hd in db.HoaDons
                            join dp in db.DatPhongs on hd.SoHoaDon equals dp.SoHoaDon
                            join p in db.Phongs on dp.MaPhong equals p.MaPhong
                            select new HoaDonVM
                            {
                                sohoadon = hd.SoHoaDon,
                                tenphong = p.TenPhong,
                                ngaythanhtoan = hd.NgayThanhToan,
                                ngayden = dp.NgayDen,
                                ngaydi = dp.NgayDi,
                                tenkh = hd.TenKh,
                                songuoi = dp.SoNguoi,
                                sdt = hd.Sdt,
                                tinhtrang = hd.TinhTrang
                            }).OrderBy(x => x.sohoadon);
            PagedList<HoaDonVM> lst = new PagedList<HoaDonVM>(hoadon, pageNumber, pageSize);

            return View(lst);
        }

        [Route("SuaHD")]
        [HttpGet]
        public IActionResult SuaHD(int id)
        {
            var hoadon = (from hd in db.HoaDons
                          join dp in db.DatPhongs on hd.SoHoaDon equals dp.SoHoaDon
                          join p in db.Phongs on dp.MaPhong equals p.MaPhong
                          where hd.SoHoaDon == id
                          select new HoaDonVM
                          {
                              sohoadon = hd.SoHoaDon,
                              maphong = p.MaPhong,
                              tenphong = p.TenPhong,
                              ngaythanhtoan = hd.NgayThanhToan,
                              ngayden = dp.NgayDen,
                              ngaydi = dp.NgayDi,
                              tenkh = hd.TenKh,
                              songuoi = dp.SoNguoi,
                              sdt = hd.Sdt,
                              tinhtrang = hd.TinhTrang
                          }).FirstOrDefault();
            return View(hoadon);
        }
        [Route("SuaHD")]
        [HttpPost]
        public IActionResult SuaHD(HoaDonVM model)
        {
            var hoadon = db.HoaDons.FirstOrDefault(p => p.SoHoaDon == model.sohoadon);
            if (hoadon != null)
            {
                hoadon.NgayThanhToan = model.ngaythanhtoan;
                hoadon.TenKh = model.tenkh;
                hoadon.Sdt = model.sdt;
                hoadon.TinhTrang = model.tinhtrang;

                db.Update(hoadon);

                var dp = db.DatPhongs.FirstOrDefault(p => p.SoHoaDon == model.sohoadon);
                if (dp != null)
                {
                    dp.NgayDen = model.ngayden;
                    dp.NgayDi = model.ngaydi;
                    dp.SoNguoi = model.songuoi;

                    db.Update(dp);
                }

                db.SaveChanges();
                return RedirectToAction("HoaDon");
            }
            return View();
        }

    }
}
