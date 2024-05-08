using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLKhachSan.Models;
using QLKhachSan.ViewModels;
using QLKhachSan.ViewModels.Admin;
using System.Runtime.Intrinsics.Arm;
using X.PagedList;


namespace QLKhachSan.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class HomeAdminController : Controller
    {
        QLKhachSanTTTNContext db = new QLKhachSanTTTNContext();
        [Route("admin")]
        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("email");
			var ten = (from nv in db.NhanViens
					   where nv.Email == email
					   select nv.TenNv).FirstOrDefault();
			ViewBag.ten = ten;
			return View();
		}
		[Route("khachsan")]
        public IActionResult KhachSan(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var khachsan = (from ks in db.KhachSans
                            join tt in db.TinhThanhs on ks.MaTinh equals tt.MaTinh
                            select new KhachSanVM
                            {
                                MaKS = ks.MaKs,
                                TenKS = ks.TenKhachSan,
                                Anh = ks.Anh,
                                TenTinh = tt.TenTinh,
                                DiaChi = ks.DiaChi,
                                Duyet = ks.Duyet,
                                Mota = ks.MoTa
                            }).OrderBy(x => x.MaKS);
            PagedList<KhachSanVM> lst = new PagedList<KhachSanVM>(khachsan, pageNumber, pageSize);

            return View(lst);
        }
		[Route("themks")]
        [HttpGet]
        public IActionResult ThemKS()
        {
				var lastMaKS = db.Phongs.OrderByDescending(p => p.MaPhong).FirstOrDefault()?.MaPhong ?? 0;
				var nextMaKS = lastMaKS + 1;
				ViewBag.nextMaKS = nextMaKS;
				return View();
        }
        [Route("themks")]
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
                var ks = new KhachSan
                {
                    MaKs = model.MaKS,
                    MaTinh = matinh,
                    TenKhachSan = model.TenKS,
                    Anh = fileName,
                    DiaChi = model.DiaChi,
                    MoTa = model.Mota,
                    Duyet = 0
                };
                db.Add(ks);
                db.SaveChanges();
                return RedirectToAction("KhachSan");
            }
            return View();
        }

        [Route("suaks")]
        [HttpGet]
        public IActionResult SuaKS(int id)
        {
            var khachsan = (from ks in db.KhachSans
                            join tt in db.TinhThanhs on ks.MaTinh equals tt.MaTinh
                            where ks.MaKs == id
                            select new KhachSanVM
                            {
                                MaKS = ks.MaKs,
                                TenKS = ks.TenKhachSan,
                                MaTinh = ks.MaTinh,
                                DiaChi = ks.DiaChi,
                                Anh = ks.Anh,
                                Mota = ks.MoTa,
                                TenTinh = tt.TenTinh
                            }).FirstOrDefault();
            return View(khachsan);
        }
        [Route("suaks")]
        [HttpPost]
        public IActionResult SuaKS(KhachSanVM model, IFormFile Anh)
        {
            var ks = db.KhachSans.FirstOrDefault(p => p.MaKs == model.MaKS);
            var matinh = (from tt in db.TinhThanhs
                        where tt.TenTinh == model.TenTinh
                        select tt.MaTinh).FirstOrDefault();
            if (ks != null)
            {
                if (Anh != null && Anh.Length > 0)
                {
                    var filename = Path.GetFileName(Anh.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Images", filename);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Anh.CopyTo(stream);
                    }
                    ks.Anh = filename;
                }
                ks.TenKhachSan = model.TenKS;
                ks.MaTinh = matinh;
                ks.DiaChi = model.DiaChi;
                ks.MoTa = model.Mota;

                db.Update(ks);
                db.SaveChanges();
                return RedirectToAction("KhachSan");
            }
            return View();
        }
        [Route("Duyet")]
        [HttpPost]
        public IActionResult Duyet(int maKS,int newValue)
        {
			var khachSan = db.KhachSans.Find(maKS);
			if (khachSan != null)
			{
				khachSan.Duyet = newValue;
				db.SaveChanges();
				return Json("Cập nhật thành công!");
			}
			return Json("Không tìm thấy khách sạn!");
		}

        [Route("doanhthu")]
        public IActionResult DoanhThu(int? nam)
        {
            var result = (from hd in db.HoaDons
                          join dp in db.DatPhongs on hd.SoHoaDon equals dp.SoHoaDon
                          join p in db.Phongs on dp.MaPhong equals p.MaPhong
                          join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
                          where hd.NgayThanhToan.Value.Year == nam
                          group new { hd, dp, p, lp } by new { hd.NgayThanhToan.Value.Month, hd.NgayThanhToan.Value.Year } into g
                          select new DoanhThuVM
                          {
                              Nam = g.Key.Year,
                              Thang = g.Key.Month,
                              TongTien = g.Sum(x => (decimal)(x.lp.Gia * (x.dp.NgayDi.Value.Day - x.dp.NgayDen.Value.Day + 1))*40/100)
                          }).ToList();
            ViewBag.nam = nam;
            return View(result);
        }
	}
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.HttpContext.Response.Redirect("/KhachHang/DangNhap");
            }
        }
    }
}
