using Microsoft.AspNetCore.Mvc;
using QLKhachSan.Models;
using QLKhachSan.ViewModels;
using QLKhachSan.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;
using QLKhachSan.Services;
using Newtonsoft.Json;
using System.Globalization;

namespace QLKhachSan.Controllers
{
    public class RoomController : Controller
    {
        private readonly ILogger<RoomController> _logger;
        private readonly QLKhachSanTTTNContext db;
        private readonly IVnPayService _vnPayService;
        public RoomController(ILogger<RoomController> logger, QLKhachSanTTTNContext context, IVnPayService vnPayService)
        {
            _logger = logger;
            db = context;
            _vnPayService = vnPayService;
        }
        public List<BookingVM> Cart => HttpContext.Session.Get<List<BookingVM>>("Cart") ?? new List<BookingVM>();
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
        public IActionResult Booking(BookingVM model)
        {
            var gia = (from lp in db.LoaiPhongs
                       join p in db.Phongs on lp.MaLp equals p.MaLp
                       where p.MaPhong == model.maphong
                       select lp.Gia).FirstOrDefault();
            TimeSpan soNgay = model.ngaydi - model.ngayden;
            int soNgayTrongKhoang = (soNgay.Days + 1);
            model.thanhtien = (double)gia * soNgayTrongKhoang;
            model.tenphong = HttpContext.Session.GetString("tenphong");
            return View(model);
        }
        public void SaveBookingInfoToDatabase(int customerID, BookingVM model, int roomID, string payment)
        {
            var khachhang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == customerID);

            var hoadon = new HoaDon
            {
                MaKh = customerID,
                NgayThanhToan = model.ngaydi,
                TenKh = model.GiongKhachHang ? khachhang.TenKh : model.HoTen,
                Email = model.GiongKhachHang ? khachhang.Email : model.Email,
                Sdt = model.GiongKhachHang ? khachhang.Sdt : model.DienThoai,
                TinhTrang = payment == "Thanh toán VNPay" ? "Đã thanh toán" : "Chưa thanh toán"
            };
            db.Add(hoadon);
            db.SaveChanges();
            var datphong = new DatPhong
            {
                SoHoaDon = hoadon.SoHoaDon,
                MaPhong = roomID,
                NgayDen = model.ngayden,
                NgayDi = model.ngaydi,
                SoNguoi = model.songuoitoida
            };
            db.Add(datphong);
            db.SaveChanges();
        }
        private void SendConfirmationEmail(BookingVM bookingModel, string customerEmail)
        {
            // Tạo nội dung email
            var tenks = (from ks in db.KhachSans
                         join p in db.Phongs on ks.MaKs equals p.MaKs
                         where p.MaPhong == bookingModel.maphong
                         select ks.TenKhachSan).FirstOrDefault();
            var diachi = (from ks in db.KhachSans
                          join p in db.Phongs on ks.MaKs equals p.MaKs
                          where p.MaPhong == bookingModel.maphong
                          select ks.DiaChi).FirstOrDefault();
            var strSanPham = "<table border='1'>";
            strSanPham += "<thead><tr><th>Tên khách sạn</th><th>Tên phòng</th><th>Ngày đến</th><th>Ngày đi</th><th>Số người</th><th>Địa chỉ</th><th>Tổng tiền</th></tr></thead>";
            strSanPham += "<tbody>";
            strSanPham += "<tr>";
            strSanPham += $"<td>{tenks}</td>";
            strSanPham += $"<td>{bookingModel.tenphong}</td>";
            strSanPham += $"<td>{bookingModel.ngayden}</td>";
            strSanPham += $"<td>{bookingModel.ngaydi}</td>";
            strSanPham += $"<td>{bookingModel.songuoitoida}</td>";
            strSanPham += $"<td>{diachi}</td>";
            strSanPham += $"<td>{bookingModel.thanhtien}</td>";
            strSanPham += "</tr>";
            strSanPham += "</tbody></table>";
            var strThongTinKhachHang = $@"
            <p>Họ tên khách hàng: {bookingModel.HoTen}</p>
            <p>Email: {bookingModel.Email}</p>
            <p>Số điện thoại: {bookingModel.DienThoai}</p>
            ";
            var fullContent = $@"
            <h2>Thông tin đặt phòng</h2>
                {strSanPham}
            <h2>Thông tin khách hàng</h2>
                {strThongTinKhachHang}
            ";

            // Gửi email
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("doducviet3012@gmail.com", "ebfwregutahnwhrj"),
                EnableSsl = true,
            };
            var fromAddress = new MailAddress("doducviet3012@gmail.com", "Hotel");
            var toAddress = new MailAddress(customerEmail);
            var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Phòng bạn được đặt thành công",
                Body = fullContent,
                IsBodyHtml = true
            };
            smtpClient.Send(mailMessage);
        }
        [HttpPost]
        public IActionResult ConfirmBooking(int maphong, BookingVM model, string payment = "COD")
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
                HttpContext.Session.SetString("CustomerID", customerID.ToString());
                HttpContext.Session.SetString("RoomID", maphong.ToString());
                HttpContext.Session.SetString("PaymentMethod", payment);
                HttpContext.Session.SetString("BookingModel", JsonConvert.SerializeObject(model));
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
                if (payment == "Thanh toán VNPay")
                {
                    var vnPayModel = new VnPaymentRequestModel
                    {
                        Amount = model.thanhtien,
                        CreateDate = DateTime.Now,
                        Desscription = $"{model.HoTen}{model.DienThoai}",
                        FullName = model.HoTen,
                        OrderId = new Random().Next(1000, 10000)
                    };
                    return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
                }
                else
                {
                        SendConfirmationEmail(model, email);
                }
                SaveBookingInfoToDatabase(customerID, model, maphong, payment);
                return View(model);
            }

            return Redirect("Success");
        }

        [Authorize]
        public IActionResult PaymentFail()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
        private static bool sentmail = false;
        [Authorize]
        public IActionResult PaymentCallBack()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VNPay:{response.VnPayResponseCode}";
                return RedirectToAction("Booking");
            }
            var customerID = int.Parse(HttpContext.Session.GetString("CustomerID"));
            var roomID = int.Parse(HttpContext.Session.GetString("RoomID"));
            var paymentMethod = HttpContext.Session.GetString("PaymentMethod");
            var bookingModel = JsonConvert.DeserializeObject<BookingVM>(HttpContext.Session.GetString("BookingModel"));
            var email = HttpContext.Session.GetString("email");
            if (bookingModel.GiongKhachHang)
            {
                var khachhang = db.KhachHangs.SingleOrDefault(p => p.MaKh == customerID);
                bookingModel.HoTen = khachhang.TenKh;
                bookingModel.DienThoai = khachhang.Sdt;
                bookingModel.Email = email;
            }
            SaveBookingInfoToDatabase(customerID, bookingModel, roomID, paymentMethod);
            if (!sentmail)
            {
                SendConfirmationEmail(bookingModel, email);
                sentmail = true;
            }
            return View("ConfirmBooking",bookingModel);
        }
    }
}
