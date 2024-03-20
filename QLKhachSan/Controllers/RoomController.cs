using Microsoft.AspNetCore.Mvc;
using QLKhachSan.Models;
using QLKhachSan.ViewModels;
using QLKhachSan.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;

namespace QLKhachSan.Controllers
{
    public class RoomController : Controller
    {
        private readonly ILogger<RoomController> _logger;
        private readonly QLKhachSanTTTNContext db;
        public RoomController(ILogger<RoomController> logger, QLKhachSanTTTNContext context)
        {
            _logger = logger;
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckPhongTrong(PhongTrongVM model)
        {
            var result = from p in db.Phongs
                         join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
                         join ks in db.KhachSans on p.MaKs equals ks.MaKs
                         join t in db.TinhThanhs on ks.MaTinh equals t.MaTinh
                         where !(from dp in db.DatPhongs
                                 where
                                        (model.ngayden >= dp.NgayDen && model.ngayden <= dp.NgayDi) ||
                            (model.ngaydi >= dp.NgayDen && model.ngaydi <= dp.NgayDi) ||
                            (dp.NgayDen >= model.ngayden && dp.NgayDen <= model.ngaydi) ||
                            (dp.NgayDi >= model.ngayden && dp.NgayDi <= model.ngaydi)
                                 select dp.MaPhong).Contains(p.MaPhong) && (model.songuoitoida == null || model.songuoitoida <= lp.SoNguoiToiDa) &&
                                         (model.TenLoaiPhong == null || model.TenLoaiPhong == lp.TenLp) && (model.tenks == null || model.tenks == ks.TenKhachSan)
                         select new SanPhamVM
                         {
                             MaLp = p.MaLp,
                             MaPhong = p.MaPhong,
                             TenPhong = p.TenPhong,
                             Anh = p.Anh,
                             Gia = lp.Gia,
                             SoNguoiToiDa = lp.SoNguoiToiDa,
                             LoaiPhong = lp.TenLp,
                             TenTinh = t.TenTinh
                         };
            var sql = (from ks in db.KhachSans
                       where ks.TenKhachSan == model.tenks
                       select ks.MaKs).FirstOrDefault();
            var thietbi = (from tb in db.ThietBis
                           select tb.TenTb).ToList();
            var loaiphong = (from lp in db.LoaiPhongs
                             join p in db.Phongs on lp.MaLp equals p.MaLp
                             join ks in db.KhachSans on p.MaKs equals ks.MaKs
                             where p.MaKs == sql
                             select lp.TenLp).Distinct().ToList();
            var songuoitoida = db.LoaiPhongs
    .Select(lp => lp.SoNguoiToiDa)
    .Max();
            ViewBag.LoaiPhong = loaiphong;
            ViewBag.songuoitoida = songuoitoida;
            ViewBag.thietbi = thietbi;
            return View(result);
        }
        [Authorize]
        public IActionResult Booking(int maphong, string tenphong , DateTime ngayden, DateTime ngaydi,int songuoitoida)
        {
            ViewBag.maphong = maphong;
            ViewBag.tenphong = tenphong;
            ViewBag.ngayden = ngayden;
            ViewBag.ngaydi = ngaydi;
            ViewBag.songuoitoida = songuoitoida;
            var gia = (from lp in db.LoaiPhongs
                       join p in db.Phongs on lp.MaLp equals p.MaLp
                       where p.MaPhong == maphong
                       select lp.Gia).FirstOrDefault();
            TimeSpan soNgay = ngaydi - ngayden;
            int soNgayTrongKhoang = (soNgay.Days + 1);
            ViewBag.thanhtien = gia * soNgayTrongKhoang;
            return View();
        }
        [HttpPost]
        public IActionResult ConfirmBooking(int maphong, BookingVM model)
        {
            if (ModelState.IsValid)
            {
                    var email = HttpContext.Session.GetString("email");
                    var customerID = (from kh in db.KhachHangs
                                      where kh.Email == email
                                      select kh.MaKh).FirstOrDefault();
                    var khachhang = new KhachHang();
                    string ht;
                    string emailKH;
                    string dienthoai;
                    string adressEmail;
                    if (model.GiongKhachHang)
                    {
                        khachhang = db.KhachHangs.SingleOrDefault(p => p.MaKh == customerID);
                        model.HoTen = khachhang.TenKh;
                        model.DienThoai = khachhang.Sdt;
                        model.Email = email;
                        ht = khachhang.TenKh;
                        emailKH = khachhang.Email;
                        dienthoai = khachhang.Sdt;
                        adressEmail = khachhang.Email;
                    }
                    else
                    {
                        ht = model.HoTen;
                        emailKH = model.Email;
                        dienthoai = model.DienThoai;
                        adressEmail = model.Email;
                    }
                    var hoadon = new HoaDon
                    {
                        MaKh = customerID,
                        NgayThanhToan = model.ngaydi,
                        TenKh = model.HoTen ?? khachhang.TenKh,
                        Email = model.Email ?? khachhang.Email,
                        Sdt = model.DienThoai ?? khachhang.Sdt
                    };
                    db.Add(hoadon);
                    db.SaveChanges();
                    var datphong = new DatPhong
                    {
                        SoHoaDon = hoadon.SoHoaDon,
                        MaPhong = maphong,
                        NgayDen = model.ngayden,
                        NgayDi = model.ngaydi,
                        SoNguoi = model.songuoitoida
                    };
                    var tenks = (from ks in db.KhachSans
                                 join p in db.Phongs on ks.MaKs equals p.MaKs
                                 where p.MaPhong == maphong
                                 select ks.TenKhachSan).FirstOrDefault();
                    var diachi = (from ks in db.KhachSans
                                 join p in db.Phongs on ks.MaKs equals p.MaKs
                                 where p.MaPhong == maphong
                                 select ks.DiaChi).FirstOrDefault();
                    db.Add(datphong);
                    db.SaveChanges();
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("doducviet3012@gmail.com", "ebfwregutahnwhrj"),
                        EnableSsl = true,

                    };

                    double tongtien = 0;
                    var strSanPham = "<table border='1'>";
                    strSanPham += "<thead><tr><th>Tên khách sạn</th><th>Tên phòng</th><th>Ngày đến</th><th>Ngày đi</th><th>Số người</th><th>Địa chỉ</th><th>Tổng tiền</th></tr></thead>";
                    strSanPham += "<tbody>";
                        strSanPham += "<tr>";
                        strSanPham += $"<td>{tenks}</td>";
                        strSanPham += $"<td>{model.tenphong}</td>";
                        strSanPham += $"<td>{model.ngayden}</td>";
                        strSanPham += $"<td>{model.ngaydi}</td>";
                        strSanPham += $"<td>{model.songuoitoida}</td>";
                        strSanPham += $"<td>{diachi}</td>";
                    strSanPham += $"<td>{model.thanhtien}</td>";
                    strSanPham += "</tr>";
                    strSanPham += "</tbody></table>";
                    var strThongTinKhachHang = $@"
                        <p>Họ tên khách hàng: {ht}</p>
                        <p>Email: {emailKH}</p>
                        <p>Số điện thoại: {dienthoai}</p>
                        ";
                    var fullContent = $@"
                         <h2>Thông tin đặt phòng</h2>
                              {strSanPham}
                         <h2>Thông tin khách hàng</h2>
                              {strThongTinKhachHang}
                         ";
                    var fromAddress = new MailAddress("doducviet3012@gmail.com", "Hotel");

                    // Tạo địa chỉ email người nhận
                    var toAddress = new MailAddress(adressEmail);

                    // Tạo đối tượng MailMessage
                    var mailMessage = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = "Phòng bạn được đặt thành công",
                        Body = fullContent,
                        IsBodyHtml = true // Đặt true nếu bạn sử dụng HTML trong nội dung email
                    };

                    // Gửi email
                    //var htmlView = AlternateView.CreateAlternateViewFromString(strSanPham, new ContentType("text/html"));
                    //mailMessage.AlternateViews.Add(htmlView);
                    smtpClient.Send(mailMessage);
                    return View(model);
            }
            return Redirect("Booking");
        }
    }
}
