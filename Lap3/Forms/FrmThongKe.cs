using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Quanlykhachsang;

namespace Quanlykhachsang.Forms
{
    public partial class FrmThongKe : Form
    {
        public FrmThongKe()
        {
            InitializeComponent();
            this.Load += FrmThongKe_Load;
        }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            // Thiết lập khoảng thời gian mặc định là tháng hiện tại
            DateTime now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

            ThongKeData();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            ThongKeData();
        }

        private void ThongKeData()
        {
            string tuNgay = dtpTuNgay.Value.ToString("yyyy-MM-dd");
            string denNgay = dtpDenNgay.Value.ToString("yyyy-MM-dd");

            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");

            // 1. Thống kê Số phiếu đặt trong khoảng thời gian
            string sqlPhieuDat = $@"SELECT COUNT(*) FROM PhieuDatPhong 
                                  WHERE CAST(NgayLap AS DATE) BETWEEN '{tuNgay}' AND '{denNgay}'";
            DataTable dtPhieuDat = Db.GetData(sqlPhieuDat);
            int countPhieuDat = (dtPhieuDat != null && dtPhieuDat.Rows.Count > 0) ? Convert.ToInt32(dtPhieuDat.Rows[0][0]) : 0;
            lblPhieuDat.Text = $"Phiếu đặt: {countPhieuDat}";

            // 2. Thống kê Số phiếu đang ở
            string sqlDangO = @"SELECT COUNT(*) FROM PhieuDatPhong WHERE TrangThai = N'Đang ở'";
            DataTable dtDangO = Db.GetData(sqlDangO);
            int countDangO = (dtDangO != null && dtDangO.Rows.Count > 0) ? Convert.ToInt32(dtDangO.Rows[0][0]) : 0;
            lblDangO.Text = $"Đang ở: {countDangO}";

            // 3. Thống kê Hóa đơn và Doanh thu hóa đơn
            string sqlHoaDon = $@"SELECT COUNT(*), ISNULL(SUM(TongTien), 0) FROM HoaDon 
                                 WHERE CAST(NgayLap AS DATE) BETWEEN '{tuNgay}' AND '{denNgay}'";
            DataTable dtHoaDon = Db.GetData(sqlHoaDon);
            int countHoaDon = 0;
            decimal doanhThu = 0;
            if (dtHoaDon != null && dtHoaDon.Rows.Count > 0)
            {
                countHoaDon = Convert.ToInt32(dtHoaDon.Rows[0][0]);
                doanhThu = Convert.ToDecimal(dtHoaDon.Rows[0][1]);
            }
            lblHoaDon.Text = $"Hóa đơn: {countHoaDon}";
            lblDoanhThuHD.Text = $"Doanh thu HĐ: {doanhThu.ToString("#,##0", cul)} đ";

            // 4. Thống kê Tổng tiền đền bù
            string sqlDenBu = $@"SELECT ISNULL(SUM(TongTien), 0) FROM PhieuDenBu 
                                WHERE CAST(NgayLap AS DATE) BETWEEN '{tuNgay}' AND '{denNgay}'";
            DataTable dtDenBu = Db.GetData(sqlDenBu);
            decimal tongDenBu = (dtDenBu != null && dtDenBu.Rows.Count > 0) ? Convert.ToDecimal(dtDenBu.Rows[0][0]) : 0;
            lblTongDenBu.Text = $"Tổng đền bù: {tongDenBu.ToString("#,##0", cul)} đ";

            // 5. Thống kê Chi tiết Dịch vụ đã sử dụng trong khoảng thời gian
            string sqlDichVu = $@"SELECT 
                                    dv.MaDV AS [Mã DV], 
                                    dv.TenDV AS [Tên dịch vụ], 
                                    ISNULL(SUM(ct.SoLuong), 0) AS [Tổng số lượng], 
                                    ISNULL(SUM(ct.ThanhTien), 0) AS [Tổng tiền]
                                  FROM DichVu dv
                                  LEFT JOIN ChiTietPhieuSuDungDV ct ON dv.MaDV = ct.MaDV
                                  LEFT JOIN PhieuSuDungDV p ON ct.SoPhieuSDDV = p.SoPhieuSDDV 
                                       AND p.NgaySuDung BETWEEN '{tuNgay}' AND '{denNgay}'
                                  GROUP BY dv.MaDV, dv.TenDV";

            DataTable dtDichVu = Db.GetData(sqlDichVu);
            if (dtDichVu != null)
            {
                dgvThongKeDichVu.DataSource = dtDichVu;
            }
        }
    }
}