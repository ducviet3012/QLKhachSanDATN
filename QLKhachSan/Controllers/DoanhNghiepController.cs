using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLKhachSan.Models;
using QLKhachSan.ViewModels;
using QLKhachSan.ViewModels.Admin;

namespace QLKhachSan.Controllers
{
    public class DoanhNghiepController : Controller
    {
        private readonly QLKhachSanTTTNContext db;

        public DoanhNghiepController(QLKhachSanTTTNContext context)
        {
            db = context;
        }
        public IActionResult DoanhThuKS(int? nam, int? maKhachSan)
        {
            var result = (from hd in db.HoaDons
                          join dp in db.DatPhongs on hd.SoHoaDon equals dp.SoHoaDon
                          join p in db.Phongs on dp.MaPhong equals p.MaPhong
                          join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
                          join ks in db.KhachSans on p.MaKs equals ks.MaKs
                          where hd.NgayThanhToan.Value.Year == nam && (maKhachSan == null || ks.MaKs == maKhachSan)
                          group new { hd, dp, p, lp } by new { hd.NgayThanhToan.Value.Month, hd.NgayThanhToan.Value.Year } into g
                          select new DoanhThuVM
                          {
                              Nam = g.Key.Year,
                              Thang = g.Key.Month,
                              TongTien = g.Sum(x => (decimal)(x.lp.Gia * (x.dp.NgayDi.Value.Day - x.dp.NgayDen.Value.Day + 1))*60/100)
                          }).ToList();
            var email = HttpContext.Session.GetString("email");
            var khachsan = (from ks in db.KhachSans
                            join kh in db.KhachHangs on ks.MaKh equals kh.MaKh
                            where kh.Email == email
                            select new { ks.MaKs, ks.TenKhachSan }).ToList();
            //var maks = (from ks in db.KhachSans
            //            join kh in db.KhachHangs on ks.MaKh equals kh.MaKh
            //            where kh.Email == email
            //            select ks.MaKs).FirstOrDefault();
            //HttpContext.Session.SetString(maks.ToString(), "IDKS");
            ViewBag.ks = khachsan;
            ViewBag.nam = nam;
            return View(result);
        }
        public IActionResult HoaDonKS(int? maks)
        {
            var hoadon = (from hd in db.HoaDons
                          join dp in db.DatPhongs on hd.SoHoaDon equals dp.SoHoaDon
                          join p in db.Phongs on dp.MaPhong equals p.MaPhong
                          join ks in db.KhachSans on p.MaKs equals ks.MaKs
                          where ks.MaKs == maks
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
                          }).ToList();
            var email = HttpContext.Session.GetString("email");
            var khachsan = (from ks in db.KhachSans
                            join kh in db.KhachHangs on ks.MaKh equals kh.MaKh
                            where kh.Email == email
                            select new { ks.MaKs, ks.TenKhachSan }).ToList();
            ViewBag.ks = khachsan;
            return View(hoadon);
        }
        public IActionResult ThemKS()
        {
            var lastMaKS = db.Phongs.OrderByDescending(p => p.MaPhong).FirstOrDefault()?.MaPhong ?? 0;
            var nextMaKS = lastMaKS + 1;
            ViewBag.nextMaKS = nextMaKS;
            return View();
        }
        [HttpPost]
        public IActionResult ThemKS(KhachSanVM model, IFormFile Anh)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(Anh.FileName);
                // Lưu tệp vào thư mục trên máy chủ
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Anh.CopyTo(stream);
                }
                var matinh = (from tt in db.TinhThanhs
                              where tt.TenTinh == model.TenTinh
                              select tt.MaTinh).FirstOrDefault();
                var email = HttpContext.Session.GetString("email");
                var idKH = (from kh in db.KhachHangs
                            where kh.Email == email
                            select kh.MaKh).FirstOrDefault();
                var ks = new KhachSan
                {
                    MaKs = model.MaKS,
                    MaTinh = matinh,
                    TenKhachSan = model.TenKS,
                    Anh = fileName,
                    DiaChi = model.DiaChi,
                    MoTa = model.Mota,
                    Duyet = 0,
                    MaKh = idKH
                };
                db.Add(ks);
                db.SaveChanges();
                return RedirectToAction("ProFile");
            }
            return View();
        }
        [HttpGet]
        public IActionResult ThemPhong(int? maks)
        {
            var lastMaPhong = db.Phongs.OrderByDescending(p => p.MaPhong).FirstOrDefault()?.MaPhong ?? 0;
            var nextMaPhong = lastMaPhong + 1;
            ViewBag.NextMaPhong = nextMaPhong;
            ViewBag.MaKs = new SelectList(db.KhachSans.ToList(), "MaKs", "TenKhachSan");
            var loaiPhongs = from lp in db.LoaiPhongs
                             where lp.MaKs == maks
                             select new
                             {
                                 lp.MaLp,
                                 lp.TenLp
                             };
            ViewBag.MaLp = new SelectList(loaiPhongs.ToList(), "MaLp", "TenLp");
            return View();
        }
        [HttpPost]
        public IActionResult ThemPhong(PhongVM model, IFormFile Anh, List<IFormFile> AnhDetail)
        {
            if (ModelState.IsValid)
            {
                bool isPhongAdded = false;
                // Lấy tên tệp
                var fileName = Path.GetFileName(Anh.FileName);
                // Lưu tệp vào thư mục trên máy chủ
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Anh.CopyTo(stream);
                }
                foreach (var file in AnhDetail)
                {
                    if (file != null && file.Length > 0)
                    {
                        var fileName1 = Path.GetFileName(file.FileName);
                        var filePath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Images", fileName1);

                        using (var stream = new FileStream(filePath1, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        var check = db.Ctanhs
                            .Where(ctanh => ctanh.TenAnh.Contains(fileName1))
                            .Count();
                        if (check == 0)
                        {
                            if (!isPhongAdded)
                            {
                                var phong = new Phong
                                {
                                    MaKs = model.MaKS,
                                    MaLp = model.MaLP,
                                    TenPhong = model.TenPhong,
                                    Anh = fileName,
                                    MoTa = model.MoTa
                                };
                                db.Add(phong);
                                db.SaveChanges();
                                isPhongAdded = true;
                            }
                            var anhChiTiet = new Ctanh
                            {
                                MaPhong = model.MaPhong, // Thay bằng ID của phòng chính
                                TenAnh = fileName1
                            };
                            // Lưu thông tin vào cơ sở dữ liệu
                            db.Add(anhChiTiet);
                            db.SaveChanges();
                        }
                        else
                        {
                            TempData["Message"] = "Ảnh chi tiết đã tồn tại";
                            return RedirectToAction("ThemPhong");
                        }
                    }
                }
                return RedirectToAction("ProFile","KhachHang");
            }
            return View(model);
        }
    }
}
