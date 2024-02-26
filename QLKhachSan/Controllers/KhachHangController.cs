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
        [HttpPost]
        public IActionResult DangKy(DangKyVM model)
        {
            if (ModelState.IsValid)
            {
                var email = db.KhachHangs.FirstOrDefault(x => x.Email == model.Email);
                if (email != null)
                {
                    return Json(new { success = false, message = "Email đã tồn tại" });
                }
                if (model.Password != model.ConfirmPassword)
                {
                    return Json(new { success = false, message = "Mật khẩu không trùng nhau" });
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
                return Json(new { success = true, redirectUrl = "/Home" });
            }
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
                    return Json(new { success = false, message = "Tài khoản không tồn tại" });
                }
                if (kh.HieuLuc == 1)
                {
                    return Json(new { success = false, message = "Tài khoản đã bị khóa" });
                }
                else
                {
                    if (kh.Password != MD5Hash(model.password))
                    {
                        return Json(new { success = false, message = "Mật khẩu không chính xác" });
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
                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return Json(new { success = true, redirectUrl = "/Home" });
                            }
                        }
                    }
                }
            }
            return View();
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
