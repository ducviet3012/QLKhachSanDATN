using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QLKhachSan.Models;
using QLKhachSan.ViewModels;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;

namespace QLKhachSan.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly QLKhachSanTTTNContext db;

        public KhachHangController(QLKhachSanTTTNContext context)
        {
            db = context;
        }
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(DangKyVM model)
        {
            if (ModelState.IsValid)
            {
                var email = db.KhachHangs.FirstOrDefault(x => x.Email == model.Email);
                if (email != null)
                {
                    TempData["Message"] = "Email đã tồn tại";
                    return View();
                }
                if (model.Password != model.ConfirmPassword)
                {
                    TempData["Message"] = "Mật khẩu không trùng nhau";
                    return View();
                }
                KhachHang kh = new KhachHang
                {
                    TenKh = model.TenKh,
                    GioiTinh = model.GioiTinh,
                    Cccd = model.Cccd,
                    Email = model.Email,
                    Password = MD5Hash(model.Password),
                    NgaySinh = model.NgaySinh,
                    DiaChi = model.DiaChi,
                    Sdt = model.Sdt,
                    UserId = 1,
                    HieuLuc = 0
                };
                db.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangNhap(DangNhapVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var kh = db.KhachHangs.FirstOrDefault(x => x.Email == model.email);
                HttpContext.Session.SetString("email", model.email);
                if (kh == null)
                {
                    TempData["Message"] = "Tài khoản không tồn tại";
                    return View();
                }
                if (kh.HieuLuc == 1)
                {
                    TempData["Message"] = "Tài khoản đã bị khóa";
                    return View();
                }
                else
                {
                    if (kh.Password != MD5Hash(model.password))
                    {
                        TempData["Message"] = "Mật khẩu không chính xác";
                        return View();
                    }
                    else
                    {
                        if (kh.UserId == 1)
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, kh.Email),
                                new Claim(ClaimTypes.Name, kh.TenKh),
                                new Claim("CustomerID", kh.MaKh.ToString()),
                                new Claim(ClaimTypes.Role,"Customer")
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);
                            //if (Url.IsLocalUrl(ReturnUrl))
                            //{
                            //    return Redirect(ReturnUrl);
                            //}
                            //else
                            //{
                            //    return RedirectToAction("Index", "Home");
                            //}
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult IsAuthenticated()
        {
            // Kiểm tra xem người dùng đã xác thực hay chưa
            bool isAuthenticated = User.Identity.IsAuthenticated;

            return Json(new { isAuthenticated });
        }
        public async Task<IActionResult> DangXuat()
        {
            HttpContext.Session.Remove("email");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult ProFile()
        {
            var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CustomerID").Value;
            var kh = db.KhachHangs.SingleOrDefault(p => p.MaKh.ToString() == customerId);
            if (kh != null)
            {
                ViewBag.KhachHang = kh;
            }
            return View();
        }
        private string MD5Hash(string input)
        {
            using (MD5 md5hash = MD5.Create())
            {
                byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}
