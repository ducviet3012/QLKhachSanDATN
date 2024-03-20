using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLKhachSan.Models;
using QLKhachSan.ViewModels;
using QLKhachSan.ViewModels.Admin;
using X.PagedList;

namespace QLKhachSan.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeAdminController : Controller
    {
        QLKhachSanTTTNContext db = new QLKhachSanTTTNContext();
        [Route("admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("phong")]
        public IActionResult Phong(int? page)
        {
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var phong = (from p in db.Phongs
                         join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
                         join ks in db.KhachSans on p.MaKs equals ks.MaKs
                         select new PhongVM
                         {
                             MaPhong = p.MaPhong,
                             TenPhong = p.TenPhong,
                             Anh = p.Anh,
                             MoTa = p.MoTa,
                             TenLP = lp.TenLp,
                             TenKS = ks.TenKhachSan
                         }).OrderBy(x=>x.MaPhong);
   //         var datphong = (from p in db.Phongs
   //                         join dp in db.DatPhongs on p.MaPhong equals dp.MaPhong
   //                         select new DatPhongVM
   //                         {
   //                             NgayDen = dp.NgayDen,
   //                             NgayDi = dp.NgayDi
   //                         });
			//var viewModel = new PhongDetailVM
			//{
			//	phongvm = phong.ToList(),
			//	datphongvm = datphong.ToList(),
			//};
			// Truyền danh sách 'viewModel' thay vì đối tượng 'viewModel'
			PagedList<PhongVM> lst = new PagedList<PhongVM>(phong, pageNumber, pageSize);

			return View(lst);
        }
		[Route("themphong")]
		[HttpGet]
		public IActionResult ThemPhong()
		{
            var lastMaPhong = db.Phongs.OrderByDescending(p => p.MaPhong).FirstOrDefault()?.MaPhong ?? 0;
            var nextMaPhong = lastMaPhong + 1;

            // Trả về mã phòng mới cho giao diện
            ViewBag.NextMaPhong = nextMaPhong;
            ViewBag.MaKs = new SelectList(db.KhachSans.ToList(), "MaKs", "TenKhachSan");
            ViewBag.MaLp = new SelectList(db.LoaiPhongs.ToList(), "MaLp", "TenLp");
            return View();
		}
        [Route("themphong")]
        [HttpPost]
        public IActionResult ThemPhong(PhongVM model,IFormFile Anh,List<IFormFile> AnhDetail)
        {
             if (ModelState.IsValid)
            {
				bool isPhongAdded = false;
				// Lấy tên tệp
				var fileName = Path.GetFileName(Anh.FileName);
                    // Lưu tệp vào thư mục trên máy chủ
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","img", "Images", fileName);

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
                return RedirectToAction("Phong");
            }
            return View(model);
        }

        [Route("suaphong")]
        [HttpGet]
        public IActionResult SuaPhong(int id)
        {
            ViewBag.MaKs = new SelectList(db.KhachSans.ToList(), "MaKs", "TenKhachSan");
            ViewBag.MaLp = new SelectList(db.LoaiPhongs.ToList(), "MaLp", "TenLp");
            var phong = (from p in db.Phongs
                        join anh in db.Ctanhs on p.MaPhong equals anh.MaPhong
                        where p.MaPhong == id
                        select new PhongVM
                        {
                            MaPhong = p.MaPhong,
                            TenPhong = p.TenPhong,
                            MaKS = p.MaKs,
                            MaLP = p.MaLp,
                            Anh = p.Anh,
                            MoTa = p.MoTa,
                            AnhDetail = anh.TenAnh
                        }).FirstOrDefault();
            return View(phong);
        }
        [Route("suaphong")]
        [HttpPost]
        public IActionResult SuaPhong(PhongVM model, IFormFile Anh , List<IFormFile> AnhDetail)
        {
            var filename = Path.GetFileName(Anh.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Images", filename);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Anh.CopyTo(stream);
            }
            var updatePhong = new Phong
            {
                TenPhong = model.TenPhong,
                MaKs = model.MaKS,
                MaLp = model.MaLP,
                Anh = filename,
                MoTa = model.MoTa
            };
            db.Update(updatePhong);
            db.SaveChanges();
            //foreach (var file in AnhDetail)
            //{
            //    if (file != null && file.Length > 0)
            //    {
            //        var fileName1 = Path.GetFileName(file.FileName);
            //        var filePath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Images", fileName1);

            //        using (var stream = new FileStream(filePath1, FileMode.Create))
            //        {
            //            file.CopyTo(stream);
            //        }
            //        var maphong = (from p in db.Phongs
            //                       where p.TenPhong == model.TenPhong
            //                       select p.MaPhong).FirstOrDefault();
            //        // Lưu thông tin về ảnh chi tiết vào cơ sở dữ liệu
            //        var anhChiTiet = new Ctanh
            //        {
            //            MaPhong = maphong, // Thay bằng ID của phòng chính
            //            TenAnh = fileName1
            //        };

            //        // Lưu thông tin vào cơ sở dữ liệu
            //        db.Update(anhChiTiet);
            //        db.SaveChanges();
            //    }
            //}
            return RedirectToAction("Phong");
        }
        [Route("xoaphong")]
        [HttpGet]
        public IActionResult XoaPhong(int id)
        {
            var anh = db.Ctanhs.Where(x => x.MaPhong == id);
            if(anh.Any())
            {
                db.RemoveRange(anh);
            }
            db.Remove(db.DatPhongs.Find(id));
            db.Remove(db.Phongs.Find(id));
            db.SaveChanges();
            TempData["Message"] = "Đã xóa phòng thành công";
            return RedirectToAction("Phong");
        }
    }
}
