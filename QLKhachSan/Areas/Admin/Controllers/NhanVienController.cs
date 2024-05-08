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
    public class NhanVienController : Controller
    {
        QLKhachSanTTTNContext db = new QLKhachSanTTTNContext();
        [Route("NhanVien")]
		public IActionResult NhanVien(int? page)
        {
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var nhanvien = (from nv in db.NhanViens
							select new NhanVienVM
							{
								MaNv = nv.MaNv,
								TenNv = nv.TenNv,
								GioiTinh = nv.GioiTinh,
								DiaChi = nv.DiaChi,
								Cccd = nv.Cccd,
								NgaySinh = nv.NgaySinh,
								Email = nv.Email,
								Sdt = nv.Sdt,
								ChucVu = nv.ChucVu
							}).OrderBy(x => x.MaNv);
			PagedList<NhanVienVM> lst = new PagedList<NhanVienVM>(nhanvien, pageNumber, pageSize);

			return View(lst);
		}
    }
}
