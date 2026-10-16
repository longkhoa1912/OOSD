using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace QuanLyBenhAn.Forms
{
    public partial class FrmPhieuKham : Form
    {
        private const string DIEU_TRI = "Điều trị";
        private const string DINH_KY = "Khám định kỳ";

        private DataTable dtToa;          // toa thuốc đang soạn
        private DataTable dtXN;           // xét nghiệm đang chỉ định
        private string maHoSoXem = null;  // hồ sơ đang xem (để hiện lịch sử)
        private string maHoSo = null;     // hồ sơ hợp lệ (còn hạn) được phép lập phiếu khám
        private bool loading = false;

        public FrmPhieuKham()
        {
            InitializeComponent();
            this.Load += FrmPhieuKham_Load;
        }

        private void FrmPhieuKham_Load(object sender, EventArgs e)
        {
            loading = true;
            cboLoai.Items.Add(DIEU_TRI);
            cboLoai.Items.Add(DINH_KY);
            cboLoai.SelectedIndex = 0;
            LoadKhoa();
            LoadThuoc();
            LoadXetNghiem();
            InitTables();
            loading = false;

            LoadBacSi();
            dtpNgayGio.Value = DateTime.Now;
            dtpXuatVien.Value = DateTime.Today;
        }

        private void InitTables()
        {
            dtToa = new DataTable();
            dtToa.Columns.Add("MaThuoc", typeof(string));
            dtToa.Columns.Add("Thuốc", typeof(string));
            dtToa.Columns.Add("Số lượng", typeof(int));
            dtToa.Columns.Add("Cách dùng", typeof(string));
            dgvToa.DataSource = dtToa;
            dgvToa.Columns["MaThuoc"].Visible = false;

            dtXN = new DataTable();
            dtXN.Columns.Add("MaXN", typeof(string));
            dtXN.Columns.Add("Xét nghiệm", typeof(string));
            dtXN.Columns.Add("Kết quả", typeof(string));
            dgvXN.DataSource = dtXN;
            dgvXN.Columns["MaXN"].Visible = false;
        }

        // ===== Nạp các combobox =====
        private void LoadKhoa()
        {
            DataTable dt = Db.GetData("SELECT MaKhoa, TenKhoa FROM Khoa ORDER BY TenKhoa");
            cboKhoa.DisplayMember = "TenKhoa";
            cboKhoa.ValueMember = "MaKhoa";
            cboKhoa.DataSource = dt;
        }

        private void LoadBacSi()
        {
            string maKhoa = cboKhoa.SelectedValue as string;
            if (maKhoa == null)
            {
                cboBS.DataSource = null;
                return;
            }
            DataTable dt = Db.GetData("SELECT MaBS, HoTen FROM BacSi WHERE MaKhoa = @k ORDER BY HoTen", new SqlParameter("@k", maKhoa));
            cboBS.DisplayMember = "HoTen";
            cboBS.ValueMember = "MaBS";
            cboBS.DataSource = dt;
        }

        private void LoadThuoc()
        {
            DataTable dt = Db.GetData("SELECT MaThuoc, TenThuoc FROM Thuoc ORDER BY TenThuoc");
            cboThuoc.DisplayMember = "TenThuoc";
            cboThuoc.ValueMember = "MaThuoc";
            cboThuoc.DataSource = dt;
        }

        private void LoadXetNghiem()
        {
            DataTable dt = Db.GetData("SELECT MaXN, TenXN FROM XetNghiem ORDER BY TenXN");
            cboXN.DisplayMember = "TenXN";
            cboXN.ValueMember = "MaXN";
            cboXN.DataSource = dt;
        }

        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loading) return;
            LoadBacSi();
        }

        // Khám định kỳ theo quý: miễn phí
        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loading) return;
            if (cboLoai.Text == DINH_KY)
            {
                txtVienPhi.Text = "0";
                txtVienPhi.Enabled = false;
                lblGhiChuLoai.Text = "Khám sức khỏe định kỳ theo quý: miễn phí";
            }
            else
            {
                txtVienPhi.Enabled = true;
                lblGhiChuLoai.Text = "";
            }
        }

        // ===== Tìm hồ sơ bệnh nhân =====
        private void btnTim_Click(object sender, EventArgs e)
        {
            string ma = txtMaHoSo.Text.Trim();
            if (ma == "")
            {
                MessageBox.Show("Vui lòng nhập Mã hồ sơ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hoTen;
            DateTime? ngaySinh, hetHan;
            if (!Db.LayThongTinHoSo(ma, out hoTen, out ngaySinh, out hetHan))
            {
                maHoSo = null;
                maHoSoXem = null;
                lblThongTin.Text = "Không tìm thấy hồ sơ " + ma;
                lblThongTin.ForeColor = Color.Red;
                dgvLichSu.DataSource = null;
                return;
            }

            bool conHan = hetHan.HasValue && hetHan.Value.Date >= DateTime.Today;
            string sinh = ngaySinh.HasValue ? ngaySinh.Value.ToString("dd/MM/yyyy") : "?";
            maHoSoXem = ma;

            if (conHan)
            {
                maHoSo = ma;
                lblThongTin.Text = hoTen + "  |  Sinh: " + sinh + "  |  Còn hạn đến " + hetHan.Value.ToString("dd/MM/yyyy");
                lblThongTin.ForeColor = Color.DarkGreen;
            }
            else
            {
                maHoSo = null;
                lblThongTin.Text = hoTen + "  |  Sinh: " + sinh + "  |  HẾT HẠN lệ phí";
                lblThongTin.ForeColor = Color.Red;
                MessageBox.Show("Hồ sơ đã hết hạn lệ phí hằng năm. Vui lòng gia hạn ở mục 'Đăng ký - Hồ sơ' trước khi lập phiếu khám mới.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            LoadLichSu();
        }

        // Lịch sử khám để bác sĩ nắm được lý lịch bệnh án
        private void LoadLichSu()
        {
            if (maHoSoXem == null) return;
            dgvLichSu.DataSource = Db.GetLichSuKham(maHoSoXem);
        }

        // ===== Toa thuốc =====
        private void btnThemThuoc_Click(object sender, EventArgs e)
        {
            string maThuoc = cboThuoc.SelectedValue as string;
            if (maThuoc == null)
            {
                MessageBox.Show("Vui lòng chọn thuốc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (DataRow dr in dtToa.Rows)
            {
                if (dr["MaThuoc"].ToString() == maThuoc)
                {
                    MessageBox.Show("Thuốc này đã có trong toa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            dtToa.Rows.Add(maThuoc, cboThuoc.Text, (int)numSL.Value, txtCachDung.Text.Trim());
            txtCachDung.Clear();
            numSL.Value = 1;
        }

        private void dgvToa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dtToa.Rows.Count) dtToa.Rows.RemoveAt(e.RowIndex);
        }

        // ===== Xét nghiệm =====
        private void btnThemXN_Click(object sender, EventArgs e)
        {
            string maXN = cboXN.SelectedValue as string;
            if (maXN == null)
            {
                MessageBox.Show("Vui lòng chọn xét nghiệm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (DataRow dr in dtXN.Rows)
            {
                if (dr["MaXN"].ToString() == maXN)
                {
                    MessageBox.Show("Xét nghiệm này đã được chỉ định!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            dtXN.Rows.Add(maXN, cboXN.Text, txtKetQuaXN.Text.Trim());
            txtKetQuaXN.Clear();
        }

        private void dgvXN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dtXN.Rows.Count) dtXN.Rows.RemoveAt(e.RowIndex);
        }

        // ===== Làm mới =====
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaPhieu();
        }

        private void XoaPhieu()
        {
            dtpNgayGio.Value = DateTime.Now;
            dtpXuatVien.Value = DateTime.Today;
            txtTrieuChung.Clear();
            txtNhiet.Clear();
            txtHuyetAp.Clear();
            txtChanDoan.Clear();
            txtPhanUng.Clear();
            txtCachDung.Clear();
            txtKetQuaXN.Clear();
            numSL.Value = 1;
            txtVienPhi.Text = "0";
            cboLoai.SelectedIndex = 0;
            dtToa.Clear();
            dtXN.Clear();
        }

        private static object GiaTriHoacNull(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s.Trim();
        }

        private static int Quy(DateTime d)
        {
            return (d.Month - 1) / 3 + 1;
        }

        // ===== Lưu phiếu khám (sau khi bệnh nhân xuất viện & thanh toán viện phí) =====
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (maHoSo == null)
            {
                MessageBox.Show("Vui lòng tìm và chọn hồ sơ bệnh nhân còn hạn trước khi lập phiếu khám!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maKhoa = cboKhoa.SelectedValue as string;
            string maBS = cboBS.SelectedValue as string;
            if (maKhoa == null || maBS == null)
            {
                MessageBox.Show("Vui lòng chọn Khoa điều trị và Bác sĩ khám!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTrieuChung.Text) && cboLoai.Text != DINH_KY)
            {
                MessageBox.Show("Vui lòng nhập Triệu chứng của bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTrieuChung.Focus();
                return;
            }

            // Thân nhiệt (không bắt buộc)
            decimal? nhiet = null;
            if (!string.IsNullOrWhiteSpace(txtNhiet.Text))
            {
                decimal t;
                string s = txtNhiet.Text.Trim().Replace(",", ".");
                if (!decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out t) || t < 30 || t > 45)
                {
                    MessageBox.Show("Thân nhiệt không hợp lệ (30 - 45 °C)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNhiet.Focus();
                    return;
                }
                nhiet = t;
            }

            // Viện phí
            decimal vienPhi = 0;
            if (cboLoai.Text != DINH_KY)
            {
                string vp = txtVienPhi.Text.Trim().Replace(".", "").Replace(",", "");
                if (!decimal.TryParse(vp, out vienPhi) || vienPhi < 0)
                {
                    MessageBox.Show("Viện phí phải là số không âm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtVienPhi.Focus();
                    return;
                }
            }

            if (dtpXuatVien.Value.Date < dtpNgayGio.Value.Date)
            {
                MessageBox.Show("Ngày xuất viện không được trước ngày khám!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime ngayKham = dtpNgayGio.Value;
            int quy = Quy(ngayKham);
            int nam = ngayKham.Year;

            // Khám định kỳ: mỗi quý chỉ được 1 lần miễn phí
            if (cboLoai.Text == DINH_KY)
            {
                object so = Db.ExecuteScalar(
                    @"SELECT COUNT(*) FROM PhieuKham
                      WHERE MaHoSo = @ma AND LoaiKham = N'Khám định kỳ'
                        AND DATEPART(QUARTER, NgayGioKham) = @q AND YEAR(NgayGioKham) = @n",
                    new SqlParameter("@ma", maHoSo), new SqlParameter("@q", quy), new SqlParameter("@n", nam));
                if (so != null && Convert.ToInt32(so) > 0)
                {
                    DialogResult dr = MessageBox.Show("Bệnh nhân đã khám sức khỏe định kỳ trong quý " + quy + "/" + nam + ".\nBạn vẫn muốn lưu thêm phiếu khám này?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr != DialogResult.Yes) return;
                }
            }

            // ----- Ghép 1 lô lệnh SQL (transaction) -----
            StringBuilder sb = new StringBuilder();
            List<SqlParameter> ps = new List<SqlParameter>();

            sb.Append("SET XACT_ABORT ON; BEGIN TRAN; ");
            sb.Append(@"INSERT INTO PhieuKham(MaHoSo, NgayGioKham, MaBS, MaKhoa, LoaiKham, TrieuChung, ThanNhiet, HuyetAp, ChanDoan, PhanUngPhu, NgayXuatVien, VienPhi)
                        VALUES (@ma, @ngay, @bs, @khoa, @loai, @tc, @nhiet, @ha, @cd, @pu, @xv, @vp); ");
            sb.Append("DECLARE @id INT = SCOPE_IDENTITY(); ");

            ps.Add(new SqlParameter("@ma", maHoSo));
            SqlParameter pNgay = new SqlParameter("@ngay", SqlDbType.DateTime);
            pNgay.Value = ngayKham;
            ps.Add(pNgay);
            ps.Add(new SqlParameter("@bs", maBS));
            ps.Add(new SqlParameter("@khoa", maKhoa));
            ps.Add(new SqlParameter("@loai", cboLoai.Text));
            ps.Add(new SqlParameter("@tc", GiaTriHoacNull(txtTrieuChung.Text)));

            SqlParameter pNhiet = new SqlParameter("@nhiet", SqlDbType.Decimal);
            pNhiet.Precision = 4;
            pNhiet.Scale = 1;
            pNhiet.Value = nhiet.HasValue ? (object)nhiet.Value : DBNull.Value;
            ps.Add(pNhiet);

            ps.Add(new SqlParameter("@ha", GiaTriHoacNull(txtHuyetAp.Text)));
            ps.Add(new SqlParameter("@cd", GiaTriHoacNull(txtChanDoan.Text)));
            ps.Add(new SqlParameter("@pu", GiaTriHoacNull(txtPhanUng.Text)));

            SqlParameter pXV = new SqlParameter("@xv", SqlDbType.Date);
            pXV.Value = dtpXuatVien.Value.Date;
            ps.Add(pXV);

            SqlParameter pVP = new SqlParameter("@vp", SqlDbType.Decimal);
            pVP.Precision = 18;
            pVP.Scale = 0;
            pVP.Value = vienPhi;
            ps.Add(pVP);

            for (int i = 0; i < dtToa.Rows.Count; i++)
            {
                DataRow r = dtToa.Rows[i];
                sb.Append("INSERT INTO ToaThuoc(MaPhieuKham, MaThuoc, SoLuong, CachDung) VALUES (@id, @t" + i + ", @sl" + i + ", @cdg" + i + "); ");
                ps.Add(new SqlParameter("@t" + i, r["MaThuoc"].ToString()));
                ps.Add(new SqlParameter("@sl" + i, Convert.ToInt32(r["Số lượng"])));
                ps.Add(new SqlParameter("@cdg" + i, GiaTriHoacNull(r["Cách dùng"].ToString())));
            }

            for (int i = 0; i < dtXN.Rows.Count; i++)
            {
                DataRow r = dtXN.Rows[i];
                sb.Append("INSERT INTO KetQuaXN(MaPhieuKham, MaXN, KetQua) VALUES (@id, @x" + i + ", @kq" + i + "); ");
                ps.Add(new SqlParameter("@x" + i, r["MaXN"].ToString()));
                ps.Add(new SqlParameter("@kq" + i, GiaTriHoacNull(r["Kết quả"].ToString())));
            }

            // Nếu là khám định kỳ: đánh dấu lịch hẹn của quý này là "Đã khám"
            if (cboLoai.Text == DINH_KY)
            {
                sb.Append(@"UPDATE LichHen SET TrangThai = N'Đã khám'
                            WHERE MaHen = (SELECT TOP 1 MaHen FROM LichHen
                                           WHERE MaHoSo = @ma AND TrangThai = N'Đã hẹn' AND Quy = @q AND Nam = @n
                                           ORDER BY NgayGioHen); ");
                ps.Add(new SqlParameter("@q", quy));
                ps.Add(new SqlParameter("@n", nam));
            }

            sb.Append("COMMIT;");

            int result = Db.ExecuteNonQuery(sb.ToString(), ps.ToArray());
            if (result > 0)
            {
                MessageBox.Show("Lưu phiếu khám bệnh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XoaPhieu();
                LoadLichSu();
            }
        }
    }
}
