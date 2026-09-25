using System;
using System.Data;
using System.Windows.Forms;
using Quanlykhachsang;

namespace Quanlykhachsang.Forms
{
    public partial class FrmTraPhong : Form
    {
        private DataTable dtDenBu;
        private string selectedSoPhong = "";
        private string selectedMaTienNghi = "";

        public FrmTraPhong()
        {
            InitializeComponent();
            this.Load += FrmTraPhong_Load;
        }

        private void FrmTraPhong_Load(object sender, EventArgs e)
        {
            InitTableDenBu();

            // Đặt các giá trị mặc định theo mẫu giao diện
            txtPhieuDangO.Text = "DP001";
            txtSoPhieuDenBu.Text = "DB001";
            txtMucDo.Text = "Hư hỏng nhẹ";
            txtSoTienDenBu.Text = "500000";

            txtSoHoaDon.Text = "HD001";
            txtSoNgayTinhTien.Text = "2";

            txtHinhThuc.Text = "Thẻ";
            txtSoTienThanhToan.Text = "1200000";

            LoadAllData();
        }

        private void InitTableDenBu()
        {
            dtDenBu = new DataTable();
            dtDenBu.Columns.Add("Tiện nghi đền bù", typeof(string));
            dtDenBu.Columns.Add("Mức độ", typeof(string));
            dtDenBu.Columns.Add("Số tiền", typeof(decimal));

            dgvTienNghiDenBu.DataSource = dtDenBu;
        }

        private void LoadAllData()
        {
            string soPhieu = txtPhieuDangO.Text.Trim();
            if (string.IsNullOrEmpty(soPhieu)) return;

            LoadDataPhong(soPhieu);
            LoadDataHoaDon(soPhieu);
        }

        private void txtPhieuDangO_TextChanged(object sender, EventArgs e)
        {
            LoadAllData();
        }

        // 1. Tải danh sách phòng của Phiếu Đặt
        private void LoadDataPhong(string soPhieu)
        {
            string query = $@"SELECT ct.SoPhong AS [Phòng], p.DonGiaNgay AS [Đơn giá/ngày]
                            FROM ChiTietDatPhong ct
                            JOIN Phong p ON ct.SoPhong = p.SoPhong
                            WHERE ct.SoPhieuDat = '{soPhieu}'";

            DataTable dt = Db.GetData(query);
            dgvPhong.DataSource = dt;

            if (dt != null && dt.Rows.Count > 0)
            {
                selectedSoPhong = dt.Rows[0]["Phòng"].ToString();
                LoadDataTienNghi(selectedSoPhong);
            }
            else
            {
                dgvTienNghi.DataSource = null;
            }
        }

        // 2. Click chọn phòng -> Load danh sách tiện nghi lắp đặt trong phòng đó
        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedSoPhong = dgvPhong.Rows[e.RowIndex].Cells["Phòng"].Value.ToString();
                LoadDataTienNghi(selectedSoPhong);
            }
        }

        private void LoadDataTienNghi(string soPhong)
        {
            string query = $@"SELECT tn.MaTienNghi, ltn.TenLoaiTN AS [Tiện nghi], ltn.TenLoaiTN AS [Loại], tn.TinhTrangHienTai AS [Tình trạng]
                            FROM PhieuLapDat pld
                            JOIN TienNghi tn ON pld.MaTienNghi = tn.MaTienNghi
                            JOIN LoaiTienNghi ltn ON tn.MaLoaiTN = ltn.MaLoaiTN
                            WHERE pld.SoPhong = '{soPhong}'";

            DataTable dt = Db.GetData(query);
            if (dt != null && dt.Columns.Contains("MaTienNghi"))
            {
                dgvTienNghi.DataSource = dt;
                if (dgvTienNghi.Columns["MaTienNghi"] != null)
                    dgvTienNghi.Columns["MaTienNghi"].Visible = false;
            }
        }

        private void dgvTienNghi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedMaTienNghi = dgvTienNghi.Rows[e.RowIndex].Cells["MaTienNghi"].Value.ToString();
                string tenTN = dgvTienNghi.Rows[e.RowIndex].Cells["Tiện nghi"].Value.ToString();

                // Tự động tìm mức đền bù quy định nếu có
                string queryQd = $@"SELECT qd.MucDenBu, qd.MucDoThietHai 
                                  FROM QuyDinhDenBu qd
                                  JOIN TienNghi tn ON qd.MaLoaiTN = tn.MaLoaiTN
                                  WHERE tn.MaTienNghi = '{selectedMaTienNghi}'";

                DataTable dtQd = Db.GetData(queryQd);
                if (dtQd != null && dtQd.Rows.Count > 0)
                {
                    txtMucDo.Text = dtQd.Rows[0]["MucDoThietHai"].ToString();
                    txtSoTienDenBu.Text = Convert.ToDecimal(dtQd.Rows[0]["MucDenBu"]).ToString("0");
                }
            }
        }

        // 3. Xử lý Lập Phiếu Đền Bù
        private void btnLapPhieuDenBu_Click(object sender, EventArgs e)
        {
            string soPhieuDat = txtPhieuDangO.Text.Trim();
            string soPhieuDB = txtSoPhieuDenBu.Text.Trim();
            string mucDo = txtMucDo.Text.Trim();
            string soTienStr = txtSoTienDenBu.Text.Trim();

            if (string.IsNullOrEmpty(soPhieuDat) || string.IsNullOrEmpty(soPhieuDB) || string.IsNullOrEmpty(selectedSoPhong))
            {
                MessageBox.Show("Vui lòng chọn Phòng và nhập đầy đủ thông tin phiếu đền bù!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal.TryParse(soTienStr, out decimal soTien);

            string maNV = "NV02"; // Phục vụ phòng

            // Thêm Phiếu đền bù
            string insertPDB = $@"INSERT INTO PhieuDenBu(SoPhieuDenBu, SoPhieuDat, SoPhong, NgayLap, MaNV, TongTien)
                                VALUES ('{soPhieuDB}', '{soPhieuDat}', '{selectedSoPhong}', GETDATE(), '{maNV}', {soTien})";

            int resPDB = Db.ExecuteNonQuery(insertPDB);

            if (resPDB > 0)
            {
                // Thêm Chi tiết phiếu đền bù nếu đã chọn thiết bị
                if (!string.IsNullOrEmpty(selectedMaTienNghi))
                {
                    string insertCTDB = $@"INSERT INTO ChiTietPhieuDenBu(SoPhieuDenBu, MaTienNghi, MucDoThietHai, SoTien)
                                          VALUES ('{soPhieuDB}', '{selectedMaTienNghi}', N'{mucDo}', {soTien})";
                    Db.ExecuteNonQuery(insertCTDB);
                }

                // Đưa thông tin lên Grid Đền bù bên phải
                dtDenBu.Rows.Add(string.IsNullOrEmpty(selectedMaTienNghi) ? "Thiết bị hỏng" : selectedMaTienNghi, mucDo, soTien);

                MessageBox.Show("Lập phiếu đền bù thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 4. Xử lý Lập Hóa Đơn
        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            string soPhieuDat = txtPhieuDangO.Text.Trim();
            string soHoaDon = txtSoHoaDon.Text.Trim();
            string soNgayStr = txtSoNgayTinhTien.Text.Trim();

            if (string.IsNullOrEmpty(soPhieuDat) || string.IsNullOrEmpty(soHoaDon) || !int.TryParse(soNgayStr, out int soNgay))
            {
                MessageBox.Show("Vui lòng nhập hợp lệ Số hóa đơn và Số ngày tính tiền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tính tiền phòng
            string queryTienPhong = $@"SELECT SUM(p.DonGiaNgay * {soNgay}) AS TienPhong
                                     FROM ChiTietDatPhong ct
                                     JOIN Phong p ON ct.SoPhong = p.SoPhong
                                     WHERE ct.SoPhieuDat = '{soPhieuDat}'";
            DataTable dtTienPhong = Db.GetData(queryTienPhong);
            decimal tienPhong = 0;
            if (dtTienPhong != null && dtTienPhong.Rows[0]["TienPhong"] != DBNull.Value)
            {
                tienPhong = Convert.ToDecimal(dtTienPhong.Rows[0]["TienPhong"]);
            }

            // Tính tiền dịch vụ
            string queryTienDV = $@"SELECT ISNULL(SUM(ct.ThanhTien), 0) AS TienDV
                                  FROM PhieuSuDungDV p
                                  JOIN ChiTietPhieuSuDungDV ct ON p.SoPhieuSDDV = ct.SoPhieuSDDV
                                  WHERE p.SoPhieuDat = '{soPhieuDat}'";
            DataTable dtTienDV = Db.GetData(queryTienDV);
            decimal tienDV = 0;
            if (dtTienDV != null && dtTienDV.Rows[0]["TienDV"] != DBNull.Value)
            {
                tienDV = Convert.ToDecimal(dtTienDV.Rows[0]["TienDV"]);
            }

            string maNV = "NV03"; // Lễ tân/Thanh toán

            string insertHD = $@"INSERT INTO HoaDon(SoHoaDon, SoPhieuDat, NgayLap, MaNV, SoNgayTinhTien, TienPhong, TienDichVu, TrangThai)
                                VALUES ('{soHoaDon}', '{soPhieuDat}', GETDATE(), '{maNV}', {soNgay}, {tienPhong}, {tienDV}, N'Chưa thanh toán')";

            int res = Db.ExecuteNonQuery(insertHD);
            if (res > 0)
            {
                MessageBox.Show("Lập hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataHoaDon(soPhieuDat);
                txtSoTienThanhToan.Text = (tienPhong + tienDV).ToString("0");
            }
        }

        private void LoadDataHoaDon(string soPhieu)
        {
            string query = $@"SELECT SoHoaDon AS [Hóa đơn], 
                                   SoPhieuDat AS [Phiếu đặt], 
                                   TienPhong AS [Tiền phòng], 
                                   TienDichVu AS [Tiền dịch vụ], 
                                   TongTien AS [Tổng tiền], 
                                   TrangThai AS [Trạng thái]
                            FROM HoaDon 
                            WHERE SoPhieuDat = '{soPhieu}'";

            DataTable dt = Db.GetData(query);
            dgvHoaDon.DataSource = dt;
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtSoHoaDon.Text = dgvHoaDon.Rows[e.RowIndex].Cells["Hóa đơn"].Value.ToString();
                txtSoTienThanhToan.Text = Convert.ToDecimal(dgvHoaDon.Rows[e.RowIndex].Cells["Tổng tiền"].Value).ToString("0");
            }
        }

        // 5. Xử lý Thanh Toán
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            string soHoaDon = txtSoHoaDon.Text.Trim();
            string hinhThuc = txtHinhThuc.Text.Trim();
            string soTienStr = txtSoTienThanhToan.Text.Trim();

            if (string.IsNullOrEmpty(soHoaDon) || string.IsNullOrEmpty(hinhThuc) || !decimal.TryParse(soTienStr, out decimal soTien))
            {
                MessageBox.Show("Vui lòng chọn Hóa đơn, Hình thức và Số tiền thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maTT = "TT_" + DateTime.Now.ToString("fff");
            string insertTT = $@"INSERT INTO ThanhToan(MaThanhToan, SoHoaDon, NgayThanhToan, HinhThuc, SoTien)
                                VALUES ('{maTT}', '{soHoaDon}', GETDATE(), N'{hinhThuc}', {soTien})";

            int res = Db.ExecuteNonQuery(insertTT);
            if (res > 0)
            {
                // Cập nhật trạng thái Hóa đơn
                string updateHD = $"UPDATE HoaDon SET TrangThai = N'Đã thanh toán' WHERE SoHoaDon = '{soHoaDon}'";
                Db.ExecuteNonQuery(updateHD);

                MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataHoaDon(txtPhieuDangO.Text.Trim());
            }
        }

        // 6. Xử lý Hoàn tất trả phòng
        private void btnHoanTatTraPhong_Click(object sender, EventArgs e)
        {
            string soPhieuDat = txtPhieuDangO.Text.Trim();

            if (string.IsNullOrEmpty(soPhieuDat)) return;

            // Cập nhật Trạng thái Phiếu Đặt sang 'Đã trả'
            string updatePhieu = $"UPDATE PhieuDatPhong SET TrangThai = N'Đã trả', NgayTraThucTe = GETDATE() WHERE SoPhieuDat = '{soPhieuDat}'";
            Db.ExecuteNonQuery(updatePhieu);

            // Cập nhật Trạng thái các Phòng trong phiếu này về 'Trống'
            string updatePhong = $@"UPDATE Phong 
                                   SET TrangThai = N'Trống' 
                                   WHERE SoPhong IN (SELECT SoPhong FROM ChiTietDatPhong WHERE SoPhieuDat = '{soPhieuDat}')";
            Db.ExecuteNonQuery(updatePhong);

            MessageBox.Show("Đã hoàn tất thủ tục trả phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}