using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QLKhachSan.Models;
using QLKhachSan.ViewModels;
using System.Diagnostics;
using X.PagedList;

namespace QLKhachSan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QLKhachSanTTTNContext db;
        public HomeController(ILogger<HomeController> logger, QLKhachSanTTTNContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listSanPhamVM = from t in db.TinhThanhs
                                select new TinhThanhVM
                                {
                                    MaTinh = t.MaTinh,
                                    TenTinh = t.TenTinh,
                                    Anh = t.Anh,
                                    SoLuongKhachSans = db.KhachSans.Count(khachSan => khachSan.MaTinh == t.MaTinh)
                                };
            PagedList<TinhThanhVM> pagedListResult = new PagedList<TinhThanhVM>(listSanPhamVM, pageNumber, pageSize);
            var hotel = (from ks in db.KhachSans
                         select ks.TenKhachSan).ToList();
            ViewBag.hotel = hotel;
            return View(pagedListResult);
        }
        public IActionResult KhachSanTheoTinh(int matinh, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            //var giatb = (from ks in db.KhachSans
            //             join p in db.Phongs on ks.MaKs equals p.MaKs
            //             join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
            //             where ks.MaTinh ==matinh
            //             select lp.Gia).Average();
            var listKS = db.KhachSans.AsTracking()
                .Where(x => x.MaTinh == matinh)
                .OrderBy(x => x.TenKhachSan)
                .Join(db.TinhThanhs, ks => ks.MaTinh, t => t.MaTinh, (ks, t) => new KhachSanVM
                {
                    MaTinh = ks.MaTinh,
                    MaKS = ks.MaKs,
                    TenKS = ks.TenKhachSan,
                    Anh = ks.Anh,
                    DiaChi = ks.DiaChi,
                    DanhGia = ks.DanhGia,
                    Mota = ks.MoTa
                })
                .ToList();
            var khuvuc = (from ks in db.KhachSans
                          where ks.MaTinh == matinh
                          select ks.DiaChi).ToList();
            ViewBag.khuvuc = khuvuc;
            ViewBag.matinh = matinh;
            //ViewBag.giatb = giatb;
            // Tạo PagedList từ danh sách SanPhamVM
            PagedList<KhachSanVM> pagedListResult = new PagedList<KhachSanVM>(listKS, pageNumber, pageSize);
            return View(pagedListResult);
        }
        public IActionResult ChiTietKS(int MaKS)
        {
            var result = from p in db.Phongs
                         join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
                         where p.MaKs == MaKS
                         select new ChiTietPhongVM
                         {
                             MaPhong = p.MaPhong,
                             TenPhong = p.TenPhong,
                             AnhPhong = p.Anh,
                             Gia = lp.Gia,
                             SoNguoiToiDa = lp.SoNguoiToiDa,
                         };
            var danhgia = from dg in db.Gopies
                          where dg.MaKs == MaKS
                          select new DanhGiaKS
                          {
                              TenKh = (from kh in db.KhachHangs
                                       where kh.MaKh == dg.MaKh
                                       select kh.TenKh).FirstOrDefault(),
                              NgayComment = dg.NgayComment,
                              Comment = dg.Comment,
                              DiemNhanVien = dg.DiemNhanVien,
                              DiemThucAn = dg.DiemDoAn,
                              DiemThoaiMai = dg.DiemThoaiMai,
                              DiemSachSe = dg.DiemSachSe,
                              DiemDichVu = dg.DiemDichVu
                          };
            var soVote = (from x in db.Gopies
                          where x.MaKs == MaKS
                          select x.Id).Count();
            ViewBag.soVote = soVote;
            var diemNhanVien = db.Gopies.Where(dg => dg.MaKs == MaKS).Select(dg => dg.DiemNhanVien);
            var diemThucAn = db.Gopies.Where(dg => dg.MaKs == MaKS).Select(dg => dg.DiemDoAn);
            var diemSachSe = db.Gopies.Where(dg => dg.MaKs == MaKS).Select(dg => dg.DiemSachSe);
            var diemThoaiMai = db.Gopies.Where(dg => dg.MaKs == MaKS).Select(dg => dg.DiemThoaiMai);
            var diemDichVu = db.Gopies.Where(dg => dg.MaKs == MaKS).Select(dg => dg.DiemDichVu);

            if (diemNhanVien.Any() && diemThucAn.Any() && diemSachSe.Any() && diemThoaiMai.Any() && diemDichVu.Any()) { 
                ViewBag.dtbNhanVien = Math.Round((decimal)diemNhanVien.Average(), 1);
                ViewBag.dtbThucAn = Math.Round((decimal)diemThucAn.Average(), 1);
                ViewBag.dtbSachSe = Math.Round((decimal)diemSachSe.Average(), 1);
                ViewBag.dtbThoaiMai = Math.Round((decimal)diemThoaiMai.Average(), 1);
                ViewBag.dtbDichVu = Math.Round((decimal)diemDichVu.Average(), 1);
            }
            var thietbi = (from tb in db.ThietBis
                           select tb.TenTb).ToList();
            var loaiphong = (from lp in db.LoaiPhongs
                             join p in db.Phongs on lp.MaLp equals p.MaLp
                             join ks in db.KhachSans on p.MaKs equals ks.MaKs
                             where p.MaKs == MaKS
                             select lp.TenLp).Distinct().ToList();
            var songuoitoida = db.LoaiPhongs
                              .Select(lp => lp.SoNguoiToiDa)
                              .Max();
            var viewModel = new ChiTietDanhGia
            {
                ChiTietPhong = result.ToList(),
                DanhGia = danhgia.ToList(),
        };
            ViewBag.maks = MaKS;
            ViewBag.LoaiPhong = loaiphong;
            ViewBag.songuoitoida = songuoitoida;
            ViewBag.thietbi = thietbi;
            return View(viewModel);
        }
        public IActionResult ChiTietPhong(DateTime ngayden, DateTime ngaydi, int id)
        {
            var result = (from Phong in db.Phongs
						  join dp in db.DatPhongs on Phong.MaPhong equals dp.MaPhong into phongDatPhong
						  from dp in phongDatPhong.DefaultIfEmpty()
						  join LoaiPhong in db.LoaiPhongs on Phong.MaLp equals LoaiPhong.MaLp
                          join ks in db.KhachSans on Phong.MaKs equals ks.MaKs
                          where Phong.MaPhong == id
                          select new ChiTietPhongVM
                          {
                              MaPhong = Phong.MaPhong,
                              TenPhong = Phong.TenPhong,
                              AnhPhong = Phong.Anh,
                              MoTaPhong = Phong.MoTa,
                              DiaChi = ks.DiaChi,
                              SoNguoiToiDa = LoaiPhong.SoNguoiToiDa,
                              ngayden = dp.NgayDen,
                              ngaydi = dp.NgayDi
                          }).FirstOrDefault();
            var anhphong = db.Ctanhs.Where(x => x.MaPhong == id).ToList();
            ViewBag.anhphong = anhphong;
            var songuoitoida = db.LoaiPhongs
                               .Where(lp => db.Phongs.Any(p => p.MaPhong == id && p.MaLp == lp.MaLp))
                               .Select(lp => lp.SoNguoiToiDa)
                               .Max();
            ViewBag.songuoitoida = songuoitoida;
            var malp = (from p in db.Phongs
                        where p.MaPhong == id
                        select p.MaLp).FirstOrDefault();
            var thietbi = (from lp in db.LoaiPhongs
                           join sdtb in db.SuDungThietBis on lp.MaLp equals sdtb.MaLp
                           join tb in db.ThietBis on sdtb.MaTb equals tb.MaTb
                           where lp.MaLp == malp
                           select tb.TenTb).ToList();
            ViewBag.thietbi = thietbi;
            var datPhongs = db.DatPhongs.Where(dp => dp.MaPhong == id).ToList();
            var result1 = new List<DateTime>();

            foreach (var datPhong in datPhongs)
            {
                var ngayDen = datPhong.NgayDen.Value.AddDays(-1);
                var soNgayTrongKhoang = (int)(datPhong.NgayDi - datPhong.NgayDen).Value.TotalDays + 1;

                for (var i = 0; i < soNgayTrongKhoang; i++)
                {
                    result1.Add(ngayDen.AddDays(i + 1));
                }
            }

            ViewBag.result1 = result1;
            return View(result);
        }
        [HttpPost]
        public IActionResult LocKhachSan(int matinh, List<string> selectKhuvuc)
        {
            if ((selectKhuvuc == null || selectKhuvuc.Count == 0))
            {
                var allHotel = from ks in db.KhachSans
                               where ks.MaTinh == matinh
                               select new KhachSanVM
                               {
                                   MaKS = ks.MaKs,
                                   TenKS = ks.TenKhachSan,
                                   Anh = ks.Anh
                               };
                return PartialView("_PartialKhachSan", allHotel);
            }
            var listKS = new List<KhachSanVM>();
            var sql = from ks in db.KhachSans
                      where selectKhuvuc.Contains(ks.DiaChi)
                      select new KhachSanVM
                      {
                          MaKS = ks.MaKs,
                          TenKS = ks.TenKhachSan,
                          Anh = ks.Anh
                      };
            listKS.AddRange(sql.Distinct());
            return PartialView("_PartialKhachSan", listKS);
        }
        [HttpPost]
        public IActionResult UpdateSelection(int maks, List<string> selectedRooms, List<string> selectedPrices, List<string> selectedThietBi)
        {
            // Nếu không có loại phòng hoặc giá nào được chọn, lấy tất cả phòng
            if ((selectedRooms == null || selectedRooms.Count == 0) && (selectedPrices == null || selectedPrices.Count == 0) && (selectedThietBi == null || selectedThietBi.Count == 0))
            {
                var allRooms = from p in db.Phongs
                               join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
                               where p.MaKs == maks
                               select new ChiTietPhongVM
                               {
                                   MaPhong = p.MaPhong,
                                   TenPhong = p.TenPhong,
                                   AnhPhong = p.Anh,
                                   Gia = lp.Gia,
                                   SoNguoiToiDa = lp.SoNguoiToiDa,
                               };

                return PartialView("_PartialPhong", allRooms);
            }

            var filteredResult = new List<ChiTietPhongVM>();

            if (selectedPrices == null || selectedPrices.Count == 0)
            {
                var sql = from p in db.Phongs
                          join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
                          join sdtb in db.SuDungThietBis on lp.MaLp equals sdtb.MaLp
                          join tb in db.ThietBis on sdtb.MaTb equals tb.MaTb
                          where (selectedRooms == null || selectedRooms.Count == 0 || selectedRooms.Contains(lp.TenLp))
                             && (selectedThietBi == null || selectedThietBi.Count == 0 || selectedThietBi.Contains(tb.TenTb))
                          select new ChiTietPhongVM
                          {
                              MaPhong = p.MaPhong,
                              TenPhong = p.TenPhong,
                              AnhPhong = p.Anh,
                              Gia = lp.Gia,
                              SoNguoiToiDa = lp.SoNguoiToiDa,
                          };
                filteredResult.AddRange(sql.Distinct());
            }
            else
            {
                var priceRanges = selectedPrices
                .Select(range => range.Split('-'))
                .Select(parts => (MinPrice: double.Parse(parts[0]), MaxPrice: double.Parse(parts[1])))
                .ToList();

                foreach (var range in priceRanges)
                {
                    double minPrice = range.MinPrice;
                    double maxPrice = range.MaxPrice;

                    var result = from p in db.Phongs
                                 join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
                                 join sdtb in db.SuDungThietBis on lp.MaLp equals sdtb.MaLp
                                 join tb in db.ThietBis on sdtb.MaTb equals tb.MaTb
                                 where (selectedRooms == null || selectedRooms.Count == 0 || selectedRooms.Contains(lp.TenLp)) &&
                                       (lp.Gia >= minPrice && lp.Gia <= maxPrice) && (selectedThietBi == null || selectedThietBi.Count == 0 || selectedThietBi.Contains(tb.TenTb))
                                 select new ChiTietPhongVM
                                 {
                                     MaPhong = p.MaPhong,
                                     TenPhong = p.TenPhong,
                                     AnhPhong = p.Anh,
                                     Gia = lp.Gia,
                                     SoNguoiToiDa = lp.SoNguoiToiDa,
                                 };

                    filteredResult.AddRange(result.Distinct());
                }
            }

            return PartialView("_PartialPhong", filteredResult);
        }

        private bool IsPriceInRange(double? gia, List<string> selectedPrices)
        {
            Console.WriteLine($"Gia: {gia}");
            if (gia.HasValue)
            {
                foreach (var selectedPrice in selectedPrices)
                {
                    Console.WriteLine($"Selected Price: {selectedPrice}");
                    var range = selectedPrice.Split('-');
                    if (range.Length == 2)
                    {
                        double minPrice = double.Parse(range[0]);
                        double maxPrice = double.Parse(range[1]);
                        if (gia >= minPrice && gia <= maxPrice)
                        {
                            return true;
                        }
                    }
                    else if (range.Length == 1)
                    {
                        double singlePrice = double.Parse(range[0]);

                        if (gia == singlePrice)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        [HttpPost]
        public IActionResult DanhGia(DanhGiaKS model,int maks)
        {
            var email = HttpContext.Session.GetString("email");
            var maKh = (from kh in db.KhachHangs
                        where kh.Email == email
                        select kh.MaKh).FirstOrDefault();
            var danhgia = new GopY
            {
                MaKs = model.Maks,
                MaKh = maKh,
                NgayComment = DateTime.Today,
                Comment = model.Comment,
                DiemNhanVien = model.DiemNhanVien,
                DiemDoAn = model.DiemThucAn,
                DiemSachSe = model.DiemSachSe,
                DiemThoaiMai = model.DiemThoaiMai,
                DiemDichVu = model.DiemDichVu
            };
            db.Add(danhgia);
            db.SaveChanges();
            return Json(new { success = true, redirectTo = Url.Action("ChiTietKS", "Home") });
        }
        //[HttpPost]
        //public IActionResult UpdateRoomSelection(List<string> selectedRooms)
        //{
        //    if (selectedRooms == null || selectedRooms.Count == 0)
        //    {
        //        var allRooms = from p in db.Phongs
        //                       join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
        //                       select new ChiTietPhongVM
        //                       {
        //                           MaPhong = p.MaPhong,
        //                           TenPhong = p.TenPhong,
        //                           AnhPhong = p.Anh,
        //                           Gia = lp.Gia,
        //                           SoNguoiToiDa = lp.SoNguoiToiDa,
        //                       };

        //        return PartialView("_PartialPhong", allRooms);
        //    }
        //    // Tìm phòng phù hợp dựa trên loại phòng đã chọn
        //    var filteredResult = from p in db.Phongs
        //                         join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
        //                         where selectedRooms.Contains(lp.TenLp) 
        //                         select new ChiTietPhongVM
        //                         {
        //                             MaPhong = p.MaPhong,
        //                             TenPhong = p.TenPhong,
        //                             AnhPhong = p.Anh,
        //                             Gia = lp.Gia,
        //                             SoNguoiToiDa = lp.SoNguoiToiDa,
        //                         };

        //    return PartialView("_PartialPhong", filteredResult);
        //}
        //[HttpPost]
        //public IActionResult UpdatePriceSelection(List<string> selectedPrices)
        //{
        //    // Nếu không có giá nào được chọn, lấy tất cả phòng
        //    if (selectedPrices == null || selectedPrices.Count == 0)
        //    {
        //        var allRooms = from p in db.Phongs
        //                       join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
        //                       select new ChiTietPhongVM
        //                       {
        //                           MaPhong = p.MaPhong,
        //                           TenPhong = p.TenPhong,
        //                           AnhPhong = p.Anh,
        //                           Gia = lp.Gia,
        //                           SoNguoiToiDa = lp.SoNguoiToiDa,
        //                       };

        //        return PartialView("_PartialPhong", allRooms);
        //    }

        //    // Ngược lại, thực hiện lọc dữ liệu theo giá đã chọn
        //    var filteredResult = from p in db.Phongs
        //                         join lp in db.LoaiPhongs on p.MaLp equals lp.MaLp
        //                         where selectedPrices.Contains(lp.Gia.ToString())
        //                         select new ChiTietPhongVM
        //                         {
        //                             MaPhong = p.MaPhong,
        //                             TenPhong = p.TenPhong,
        //                             AnhPhong = p.Anh,
        //                             Gia = lp.Gia,
        //                             SoNguoiToiDa = lp.SoNguoiToiDa,
        //                         };

        //    return PartialView("_PartialPhong", filteredResult);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}