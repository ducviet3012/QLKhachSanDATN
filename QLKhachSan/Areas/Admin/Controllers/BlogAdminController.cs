using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLKhachSan.Models;
using QLKhachSan.ViewModels;
using QLKhachSan.ViewModels.Admin;
using X.PagedList;

namespace QLKhachSan.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class BlogAdminController : Controller
    {
        QLKhachSanTTTNContext db = new QLKhachSanTTTNContext();
        [Route("Blog")]
        public IActionResult Blog(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var blog = (from b in db.Blogs
                        join nv in db.NhanViens on b.MaNv equals nv.MaNv
                        select new BlogVM
                        {
                            IDBlog = b.Idblog,
                            TenNV = nv.TenNv,
                            Anh = b.Anh,
                            TieuDe = b.TieuDe,
                            ThongTin = b.ThongTin,
                            NgayDang = b.NgayDang
                        });
            PagedList<BlogVM> lst = new PagedList<BlogVM>(blog, pageNumber, pageSize);

            return View(lst);
        }
        [Route("ThemBlog")]
        [HttpGet]
        public IActionResult ThemBlog()
        {
            var lastMaKS = db.Phongs.OrderByDescending(p => p.MaPhong).FirstOrDefault()?.MaPhong ?? 0;
            var nextMaKS = lastMaKS + 1;
            ViewBag.nextMaKS = nextMaKS;
            return View();
        }
        [Route("ThemBlog")]
        [HttpPost]
        public IActionResult ThemBlog(BlogVM model, IFormFile Anh)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(Anh.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Anh.CopyTo(stream);
                }
                var email = HttpContext.Session.GetString("email");
                var manv = (from nv in db.NhanViens
                              where nv.Email == email
                              select nv.MaNv).FirstOrDefault();
                var blog = new Blog
                {
                    MaNv = manv,
                    TieuDe = model.TieuDe,
                    ThongTin = model.ThongTin,
                    Anh = fileName,
                    NgayDang = DateTime.Now
                };
                db.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Blog");
            }
            return View();
        }
    }
}
