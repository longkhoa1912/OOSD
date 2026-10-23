using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QuanLyNhanSuDongA.Forms
{
    public partial class FrmNhanVien : Form
    {
        public FrmNhanVien()
        {
            InitializeComponent();
        }

        // ================= LOAD =================
        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboTrangThai.Items.AddRange(new object[] { "Thử việc", "Chính thức", "Thôi việc" });
            try
            {
                LoadPhongBan();
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ResetForm();
        }

        // ComboBox khóa ngoại: Phòng ban
        private void LoadPhongBan()
        {
            DataTable dt = Db.GetTable("SELECT MaPhongBan, TenPhongBan FROM PhongBan ORDER BY TenPhongBan");

            cboPhongBan.DisplayMember = "TenPhongBan";
            cboPhongBan.ValueMember = "MaPhongBan";
            cboPhongBan.DataSource = dt;

            DataTable dtLoc = dt.Copy();
            DataRow all = dtLoc.NewRow();
            all["MaPhongBan"] = "";
            all["TenPhongBan"] = "-- Tất cả phòng ban --";
            dtLoc.Rows.InsertAt(all, 0);
            cboLocPB.DisplayMember = "TenPhongBan";
            cboLocPB.ValueMember = "MaPhongBan";
            cboLocPB.DataSource = dtLoc;
        }

        // DataGridView danh sách nhân viên (có tìm kiếm / lọc)
        private void LoadGrid()
        {
            string kw = "%" + txtTim.Text.Trim() + "%";
            string pb = cboLocPB.SelectedValue == null ? "" : cboLocPB.SelectedValue.ToString();

            string sql = @"SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.GioiTinh, nv.SoDienThoai,
                                  nv.Email, nv.DiaChi, nv.TrangThai, nv.MaPhongBan, pb.TenPhongBan
                           FROM NhanVien nv JOIN PhongBan pb ON nv.MaPhongBan = pb.MaPhongBan
                           WHERE (nv.MaNV LIKE @k OR nv.HoTen LIKE @k)
                             AND (@pb = '' OR nv.MaPhongBan = @pb)
                           ORDER BY nv.MaNV";
            dgvNhanVien.DataSource = Db.GetTable(sql,
                new SqlParameter("@k", kw),
                new SqlParameter("@pb", pb));

            dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
            dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên";
            dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới tính";
            dgvNhanVien.Columns["SoDienThoai"].HeaderText = "Điện thoại";
            dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvNhanVien.Columns["TenPhongBan"].HeaderText = "Phòng ban";
            dgvNhanVien.Columns["MaPhongBan"].Visible = false;
            lblTong.Text = "Tổng: " + dgvNhanVien.Rows.Count + " nhân viên";
        }

        // ================= TRẠNG THÁI FORM =================
        private void ResetForm()
        {
            txtMaNV.Clear(); txtHoTen.Clear(); txtDiaChi.Clear();
            txtSDT.Clear(); txtEmail.Clear();
            dtpNgaySinh.Value = new DateTime(2000, 1, 1);
            cboGioiTinh.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            if (cboPhongBan.Items.Count > 0) cboPhongBan.SelectedIndex = 0;

            chkThuViec.Checked = true;
            txtSoQD.Clear();
            dtpNgayKy.Value = DateTime.Today;
            numThang.Value = 2;
            numLuong.Value = 0;
            txtDieuKhoan.Text = "Hai bên cam kết thực hiện đúng nội quy, quy định của công ty trong thời gian thử việc.";

            txtMaNV.ReadOnly = false;          // mở khóa khi thêm mới
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            grpThuViec.Enabled = true;
            txtMaNV.Focus();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow r = dgvNhanVien.Rows[e.RowIndex];

            txtMaNV.Text = r.Cells["MaNV"].Value.ToString();
            txtHoTen.Text = r.Cells["HoTen"].Value.ToString();
            dtpNgaySinh.Value = Convert.ToDateTime(r.Cells["NgaySinh"].Value);
            cboGioiTinh.Text = r.Cells["GioiTinh"].Value.ToString();
            txtSDT.Text = r.Cells["SoDienThoai"].Value.ToString();
            txtEmail.Text = r.Cells["Email"].Value.ToString();
            txtDiaChi.Text = r.Cells["DiaChi"].Value.ToString();
            cboTrangThai.Text = r.Cells["TrangThai"].Value.ToString();
            cboPhongBan.SelectedValue = r.Cells["MaPhongBan"].Value.ToString();

            txtMaNV.ReadOnly = true;           // khóa Mã NV khi chỉnh sửa
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            grpThuViec.Enabled = false;        // QĐ thử việc chỉ lập khi thêm mới
        }

        // ================= KIỂM TRA DỮ LIỆU =================
        private bool ValidateInput()
        {
            if (txtMaNV.Text.Trim() == "")
                return Warn("Mã nhân viên không được để trống.", txtMaNV);
            if (txtHoTen.Text.Trim() == "")
                return Warn("Họ tên không được để trống.", txtHoTen);
            if (cboPhongBan.SelectedValue == null)
                return Warn("Vui lòng chọn phòng ban.", cboPhongBan);
            if (dtpNgaySinh.Value.Date > DateTime.Today.AddYears(-18))
                return Warn("Nhân viên phải đủ 18 tuổi.", dtpNgaySinh);
            if (txtSDT.Text.Trim() != "" && !Regex.IsMatch(txtSDT.Text.Trim(), @"^0\d{9,10}$"))
                return Warn("Số điện thoại không hợp lệ (10-11 số, bắt đầu bằng 0).", txtSDT);
            if (txtEmail.Text.Trim() != "" && !Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return Warn("Email không hợp lệ.", txtEmail);

            if (btnThem.Enabled && chkThuViec.Checked)
            {
                if (txtSoQD.Text.Trim() == "")
                    return Warn("Số quyết định thử việc không được để trống.", txtSoQD);
                if (numThang.Value <= 0)
                    return Warn("Thời gian thử việc phải lớn hơn 0.", numThang);
                if (numLuong.Value <= 0)
                    return Warn("Mức lương thử việc phải lớn hơn 0.", numLuong);
            }
            return true;
        }

        private bool Warn(string msg, Control c)
        {
            MessageBox.Show(msg, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            c.Focus();
            return false;
        }

        // ================= THÊM (TRANSACTION: NV + QĐ THỬ VIỆC) =================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string maNV = txtMaNV.Text.Trim();
            string maPB = cboPhongBan.SelectedValue.ToString();

            if (Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM NhanVien WHERE MaNV=@m",
                    new SqlParameter("@m", maNV))) > 0)
            {
                Warn("Mã nhân viên đã tồn tại.", txtMaNV);
                return;
            }

            using (SqlConnection con = Db.GetConnection())
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    string sqlNV = @"INSERT INTO NhanVien(MaNV,HoTen,NgaySinh,GioiTinh,DiaChi,SoDienThoai,Email,TrangThai,MaPhongBan)
                                     VALUES(@MaNV,@HoTen,@NgaySinh,@GioiTinh,@DiaChi,@SDT,@Email,@TrangThai,@MaPB)";
                    using (var cmd = new SqlCommand(sqlNV, con, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value.Date);
                        cmd.Parameters.AddWithValue("@GioiTinh", cboGioiTinh.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                        cmd.Parameters.AddWithValue("@SDT", txtSDT.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@TrangThai", chkThuViec.Checked ? "Thử việc" : cboTrangThai.Text);
                        cmd.Parameters.AddWithValue("@MaPB", maPB);
                        cmd.ExecuteNonQuery();
                    }

                    if (chkThuViec.Checked)
                    {
                        string sqlQD = @"INSERT INTO QuyetDinhTuyenDung
                                         (MaQDThuViec,SoQuyetDinh,MaNV,MaPhongBan,NgayKy,ThoiGianThuViec,LuongThuViec,DieuKhoan)
                                         VALUES(@MaQD,@So,@MaNV,@MaPB,@NgayKy,@Thang,@Luong,@DieuKhoan)";
                        using (var cmd = new SqlCommand(sqlQD, con, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaQD", "TV_" + maNV + "_" + DateTime.Now.ToString("yyMMddHHmmss"));
                            cmd.Parameters.AddWithValue("@So", txtSoQD.Text.Trim());
                            cmd.Parameters.AddWithValue("@MaNV", maNV);
                            cmd.Parameters.AddWithValue("@MaPB", maPB);
                            cmd.Parameters.AddWithValue("@NgayKy", dtpNgayKy.Value.Date);
                            cmd.Parameters.AddWithValue("@Thang", (int)numThang.Value);
                            cmd.Parameters.AddWithValue("@Luong", numLuong.Value);
                            cmd.Parameters.AddWithValue("@DieuKhoan", txtDieuKhoan.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    MessageBox.Show(chkThuViec.Checked
                        ? "Đã thêm nhân viên và lập quyết định thử việc thành công."
                        : "Thêm nhân viên thành công.",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    tran.Rollback();   // không để dữ liệu cập nhật dở dang
                    MessageBox.Show("Thêm thất bại, dữ liệu đã được hoàn tác.\n" + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            LoadGrid();
            ResetForm();
        }

        // ================= SỬA =================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            try
            {
                string sql = @"UPDATE NhanVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh,
                                      DiaChi=@DiaChi, SoDienThoai=@SDT, Email=@Email,
                                      TrangThai=@TrangThai, MaPhongBan=@MaPB
                               WHERE MaNV=@MaNV";
                int n = Db.Execute(sql,
                    new SqlParameter("@HoTen", txtHoTen.Text.Trim()),
                    new SqlParameter("@NgaySinh", dtpNgaySinh.Value.Date),
                    new SqlParameter("@GioiTinh", cboGioiTinh.Text),
                    new SqlParameter("@DiaChi", txtDiaChi.Text.Trim()),
                    new SqlParameter("@SDT", txtSDT.Text.Trim()),
                    new SqlParameter("@Email", txtEmail.Text.Trim()),
                    new SqlParameter("@TrangThai", cboTrangThai.Text),
                    new SqlParameter("@MaPB", cboPhongBan.SelectedValue.ToString()),
                    new SqlParameter("@MaNV", txtMaNV.Text.Trim()));

                MessageBox.Show(n > 0 ? "Cập nhật thành công." : "Không tìm thấy nhân viên để cập nhật.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= XÓA (CHẶN NẾU ĐÃ PHÁT SINH QUYẾT ĐỊNH) =================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            try
            {
                int qd = Convert.ToInt32(Db.Scalar(
                    "SELECT COUNT(*) FROM QuyetDinhTuyenDung WHERE MaNV=@m", new SqlParameter("@m", maNV)));
                int tp = Convert.ToInt32(Db.Scalar(
                    "SELECT COUNT(*) FROM PhongBan WHERE MaTruongPhong=@m", new SqlParameter("@m", maNV)));

                if (qd > 0 || tp > 0)
                {
                    MessageBox.Show("Không thể xóa: nhân viên đã phát sinh quyết định hoặc đang là trưởng phòng.\n" +
                                    "Hãy chuyển trạng thái sang \"Thôi việc\" thay vì xóa.",
                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show("Xóa nhân viên " + maNV + "?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                Db.Execute("DELETE FROM NhanVien WHERE MaNV=@m", new SqlParameter("@m", maNV));
                MessageBox.Show("Đã xóa nhân viên.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
                ResetForm();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Không thể xóa do ràng buộc khóa ngoại.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa thất bại:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= KHÁC =================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTim.Clear();
            cboLocPB.SelectedIndex = 0;
            LoadGrid();
            ResetForm();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtTim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadGrid();
        }

        private void chkThuViec_CheckedChanged(object sender, EventArgs e)
        {
            bool on = chkThuViec.Checked;
            txtSoQD.Enabled = on; dtpNgayKy.Enabled = on;
            numThang.Enabled = on; numLuong.Enabled = on; txtDieuKhoan.Enabled = on;
            if (on && btnThem.Enabled) cboTrangThai.Text = "Thử việc";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
