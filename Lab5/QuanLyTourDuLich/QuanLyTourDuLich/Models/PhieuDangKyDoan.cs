using System;

namespace QuanLyTourDuLich.Models
{
    public class PhieuDangKyDoan
    {
        public int MaPhieu { get; set; }
        public int MaDoan { get; set; }
        public string MaTour { get; set; }
        public DateTime NgayDi { get; set; }
        public int SoNguoi { get; set; }
        public string DiaDiemDon { get; set; }
        public bool CoBaoHiem { get; set; }
    }
}
