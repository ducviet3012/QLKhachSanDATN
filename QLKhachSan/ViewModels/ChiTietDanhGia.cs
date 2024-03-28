namespace QLKhachSan.ViewModels
{
    public class ChiTietDanhGia
    {
        public IEnumerable<ChiTietPhongVM> ChiTietPhong { get; set; }
        public IEnumerable<DanhGiaKS> DanhGia { get; set; }
        public IEnumerable<DiemTrungBinhGopYVM> DiemTrungBinh { get; set; }
    }
}
