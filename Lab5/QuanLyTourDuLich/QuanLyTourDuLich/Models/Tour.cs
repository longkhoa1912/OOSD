namespace QuanLyTourDuLich.Models
{
    public class Tour
    {
        public string MaTour { get; set; }
        public string TenTour { get; set; }
        public int SoNgay { get; set; }
        public int SoDem { get; set; }
        public decimal DonGia { get; set; }

        public string TenHienThi
        {
            get { return MaTour + " - " + TenTour; }
        }
    }
}
