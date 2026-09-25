using System;
using System.Data;
using System.Windows.Forms;
using Quanlykhachsang;

namespace Quanlykhachsang.Forms
{
    public partial class FrmDichVu : Form
    {
        public FrmDichVu()
        {
            InitializeComponent();
            this.Load += FrmDichVu_Load;
        }

        private void FrmDichVu_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mẫu ban đầu
            txtPhieuLuuTru.Text = "DP001";
            txtPhong.Text = "A101";
            txtDichVu.Text = "Ăn sáng";
            txtNgaySuDung.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtSoLuong.Text = "2";

            LoadDataDichVu();
        }

        // Tải dữ liệu từ bảng PhieuSuDungDV, ChiTietPhieuSuDungDV và DichVu
        private void LoadDataDichVu()
        {
            string query = @"SELECT p.SoPhieuDat AS [Số phiếu], 
                                   p.SoPhong AS [Phòng], 
                                   CONVERT(VARCHAR(10), p.NgaySuDung, 103) AS [Ngày], 
                                   dv.TenDV AS [Dịch vụ], 
                                   ct.SoLuong AS [Số lượng], 
                                   ct.DonGia AS [Đơn giá], 
                                   ct.ThanhTien AS [Thành tiền]
                            FROM PhieuSuDungDV p 
                            JOIN ChiTietPhieuSuDungDV ct ON p.SoPhieuSDDV = ct.SoPhieuSDDV
                            JOIN DichVu dv ON ct.MaDV = dv.MaDV";

            DataTable dt = Db.GetData(query);
            if (dt != null)
            {
                dgvDichVu.DataSource = dt;
            }
        }

        // Xử lý nút Ghi nhận dịch vụ
        private void btnGhiNhan_Click(object sender, EventArgs e)
        {
            string soPhieuDat = txtPhieuLuuTru.Text.Trim();
            string soPhong = txtPhong.Text.Trim();
            string tenDV = txtDichVu.Text.Trim();
            string soLuongStr = txtSoLuong.Text.Trim();

            if (string.IsNullOrEmpty(soPhieuDat) || string.IsNullOrEmpty(soPhong) || string.IsNullOrEmpty(tenDV) || string.IsNullOrEmpty(soLuongStr))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(soLuongStr, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Kiểm tra xem phòng và phiếu đặt đó có tồn tại trong ChiTietDatPhong không
            string checkCTDat = $"SELECT * FROM ChiTietDatPhong WHERE SoPhieuDat = '{soPhieuDat}' AND SoPhong = '{soPhong}'";
            DataTable dtCTDat = Db.GetData(checkCTDat);
            if (dtCTDat == null || dtCTDat.Rows.Count == 0)
            {
                MessageBox.Show($"Không tìm thấy Phòng '{soPhong}' thuộc Phiếu đặt '{soPhieuDat}' trong CSDL! Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Tìm MaDV và DonGia từ bảng DichVu
            string checkDV = $"SELECT MaDV, DonGia FROM DichVu WHERE TenDV = N'{tenDV}'";
            DataTable dtDV = Db.GetData(checkDV);

            string maDV = "DV01";
            decimal donGia = 120000;

            if (dtDV != null && dtDV.Rows.Count > 0)
            {
                maDV = dtDV.Rows[0]["MaDV"].ToString();
                donGia = Convert.ToDecimal(dtDV.Rows[0]["DonGia"]);
            }

            // 3. Kiểm tra xem đã có PhieuSuDungDV cho ngày hôm nay chưa
            string maNV = "NV01";
            string soPhieuSDDV = "";
            string checkPhieuSD = $"SELECT SoPhieuSDDV FROM PhieuSuDungDV WHERE SoPhieuDat = '{soPhieuDat}' AND SoPhong = '{soPhong}' AND NgaySuDung = CAST(GETDATE() AS DATE)";
            DataTable dtPhieuSD = Db.GetData(checkPhieuSD);

            if (dtPhieuSD != null && dtPhieuSD.Rows.Count > 0)
            {
                soPhieuSDDV = dtPhieuSD.Rows[0]["SoPhieuSDDV"].ToString();
            }
            else
            {
                // Tạo mới PhieuSuDungDV
                soPhieuSDDV = "SDDV_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string insertPhieuSD = $"INSERT INTO PhieuSuDungDV(SoPhieuSDDV, SoPhieuDat, SoPhong, NgaySuDung, MaNV) VALUES ('{soPhieuSDDV}', '{soPhieuDat}', '{soPhong}', CAST(GETDATE() AS DATE), '{maNV}')";
                Db.ExecuteNonQuery(insertPhieuSD);
            }

            // 4. Thêm vào ChiTietPhieuSuDungDV
            string insertCTSDDV = $"INSERT INTO ChiTietPhieuSuDungDV(SoPhieuSDDV, MaDV, SoLuong, DonGia) VALUES ('{soPhieuSDDV}', '{maDV}', {soLuong}, {donGia})";
            int res = Db.ExecuteNonQuery(insertCTSDDV);

            if (res > 0)
            {
                MessageBox.Show("Ghi nhận dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataDichVu();
            }
            else
            {
                MessageBox.Show("Dịch vụ này đã được thêm trong phiếu sử dụng hôm nay!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}