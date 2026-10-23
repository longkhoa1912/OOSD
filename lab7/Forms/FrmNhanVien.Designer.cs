namespace QuanLyNhanSuDongA.Forms
{
    partial class FrmNhanVien
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.grpNhanVien = new System.Windows.Forms.GroupBox();
            this.grpThuViec = new System.Windows.Forms.GroupBox();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhongBan = new System.Windows.Forms.Label();
            this.cboPhongBan = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.chkThuViec = new System.Windows.Forms.CheckBox();
            this.lblSoQD = new System.Windows.Forms.Label();
            this.txtSoQD = new System.Windows.Forms.TextBox();
            this.lblNgayKy = new System.Windows.Forms.Label();
            this.dtpNgayKy = new System.Windows.Forms.DateTimePicker();
            this.lblThang = new System.Windows.Forms.Label();
            this.numThang = new System.Windows.Forms.NumericUpDown();
            this.lblLuong = new System.Windows.Forms.Label();
            this.numLuong = new System.Windows.Forms.NumericUpDown();
            this.lblDieuKhoan = new System.Windows.Forms.Label();
            this.txtDieuKhoan = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.lblTim = new System.Windows.Forms.Label();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.cboLocPB = new System.Windows.Forms.ComboBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.lblTong = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLuong)).BeginInit();
            this.grpNhanVien.SuspendLayout();
            this.grpThuViec.SuspendLayout();
            this.SuspendLayout();
            // lblTieuDe
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Location = new System.Drawing.Point(0, 8);
            this.lblTieuDe.Size = new System.Drawing.Size(1100, 32);
            this.lblTieuDe.Text = "QUẢN LÝ NHÂN VIÊN & QUYẾT ĐỊNH THỬ VIỆC - CÔNG TY SƠN ĐÔNG Á";
            this.lblTieuDe.AutoSize = false;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // grpNhanVien
            this.grpNhanVien.Name = "grpNhanVien";
            this.grpNhanVien.Location = new System.Drawing.Point(12, 48);
            this.grpNhanVien.Size = new System.Drawing.Size(540, 230);
            this.grpNhanVien.Text = "Thông tin nhân viên";
            // grpThuViec
            this.grpThuViec.Name = "grpThuViec";
            this.grpThuViec.Location = new System.Drawing.Point(562, 48);
            this.grpThuViec.Size = new System.Drawing.Size(520, 230);
            this.grpThuViec.Text = "Quyết định tuyển dụng / thử việc";
            // lblMaNV
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Location = new System.Drawing.Point(15, 33);
            this.lblMaNV.Size = new System.Drawing.Size(80, 20);
            this.lblMaNV.Text = "Mã NV";
            this.lblMaNV.AutoSize = true;
            // txtMaNV
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Location = new System.Drawing.Point(100, 30);
            this.txtMaNV.Size = new System.Drawing.Size(165, 24);
            // lblHoTen
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Location = new System.Drawing.Point(280, 33);
            this.lblHoTen.Size = new System.Drawing.Size(80, 20);
            this.lblHoTen.Text = "Họ tên";
            this.lblHoTen.AutoSize = true;
            // txtHoTen
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Location = new System.Drawing.Point(360, 30);
            this.txtHoTen.Size = new System.Drawing.Size(165, 24);
            // lblNgaySinh
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Location = new System.Drawing.Point(15, 68);
            this.lblNgaySinh.Size = new System.Drawing.Size(80, 20);
            this.lblNgaySinh.Text = "Ngày sinh";
            this.lblNgaySinh.AutoSize = true;
            // dtpNgaySinh
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Location = new System.Drawing.Point(100, 65);
            this.dtpNgaySinh.Size = new System.Drawing.Size(165, 24);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            // lblGioiTinh
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Location = new System.Drawing.Point(280, 68);
            this.lblGioiTinh.Size = new System.Drawing.Size(80, 20);
            this.lblGioiTinh.Text = "Giới tính";
            this.lblGioiTinh.AutoSize = true;
            // cboGioiTinh
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Location = new System.Drawing.Point(360, 65);
            this.cboGioiTinh.Size = new System.Drawing.Size(165, 24);
            this.cboGioiTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // lblSDT
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Location = new System.Drawing.Point(15, 103);
            this.lblSDT.Size = new System.Drawing.Size(80, 20);
            this.lblSDT.Text = "Điện thoại";
            this.lblSDT.AutoSize = true;
            // txtSDT
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Location = new System.Drawing.Point(100, 100);
            this.txtSDT.Size = new System.Drawing.Size(165, 24);
            // lblEmail
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Location = new System.Drawing.Point(280, 103);
            this.lblEmail.Size = new System.Drawing.Size(80, 20);
            this.lblEmail.Text = "Email";
            this.lblEmail.AutoSize = true;
            // txtEmail
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Location = new System.Drawing.Point(360, 100);
            this.txtEmail.Size = new System.Drawing.Size(165, 24);
            // lblPhongBan
            this.lblPhongBan.Name = "lblPhongBan";
            this.lblPhongBan.Location = new System.Drawing.Point(15, 138);
            this.lblPhongBan.Size = new System.Drawing.Size(80, 20);
            this.lblPhongBan.Text = "Phòng ban";
            this.lblPhongBan.AutoSize = true;
            // cboPhongBan
            this.cboPhongBan.Name = "cboPhongBan";
            this.cboPhongBan.Location = new System.Drawing.Point(100, 135);
            this.cboPhongBan.Size = new System.Drawing.Size(165, 24);
            this.cboPhongBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // lblTrangThai
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Location = new System.Drawing.Point(280, 138);
            this.lblTrangThai.Size = new System.Drawing.Size(80, 20);
            this.lblTrangThai.Text = "Trạng thái";
            this.lblTrangThai.AutoSize = true;
            // cboTrangThai
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Location = new System.Drawing.Point(360, 135);
            this.cboTrangThai.Size = new System.Drawing.Size(165, 24);
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // lblDiaChi
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Location = new System.Drawing.Point(15, 173);
            this.lblDiaChi.Size = new System.Drawing.Size(80, 20);
            this.lblDiaChi.Text = "Địa chỉ";
            this.lblDiaChi.AutoSize = true;
            // txtDiaChi
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Location = new System.Drawing.Point(100, 170);
            this.txtDiaChi.Size = new System.Drawing.Size(425, 24);
            // chkThuViec
            this.chkThuViec.Name = "chkThuViec";
            this.chkThuViec.Location = new System.Drawing.Point(15, 25);
            this.chkThuViec.Size = new System.Drawing.Size(420, 22);
            this.chkThuViec.Text = "Lập quyết định thử việc kèm theo (thao tác Transaction)";
            this.chkThuViec.AutoSize = true;
            this.chkThuViec.Checked = true;
            this.chkThuViec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThuViec.CheckedChanged += new System.EventHandler(this.chkThuViec_CheckedChanged);
            // lblSoQD
            this.lblSoQD.Name = "lblSoQD";
            this.lblSoQD.Location = new System.Drawing.Point(15, 58);
            this.lblSoQD.Size = new System.Drawing.Size(90, 20);
            this.lblSoQD.Text = "Số quyết định";
            this.lblSoQD.AutoSize = true;
            // txtSoQD
            this.txtSoQD.Name = "txtSoQD";
            this.txtSoQD.Location = new System.Drawing.Point(110, 55);
            this.txtSoQD.Size = new System.Drawing.Size(150, 24);
            // lblNgayKy
            this.lblNgayKy.Name = "lblNgayKy";
            this.lblNgayKy.Location = new System.Drawing.Point(280, 58);
            this.lblNgayKy.Size = new System.Drawing.Size(70, 20);
            this.lblNgayKy.Text = "Ngày ký";
            this.lblNgayKy.AutoSize = true;
            // dtpNgayKy
            this.dtpNgayKy.Name = "dtpNgayKy";
            this.dtpNgayKy.Location = new System.Drawing.Point(355, 55);
            this.dtpNgayKy.Size = new System.Drawing.Size(150, 24);
            this.dtpNgayKy.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            // lblThang
            this.lblThang.Name = "lblThang";
            this.lblThang.Location = new System.Drawing.Point(15, 93);
            this.lblThang.Size = new System.Drawing.Size(95, 20);
            this.lblThang.Text = "Thử việc (tháng)";
            this.lblThang.AutoSize = true;
            // numThang
            this.numThang.Name = "numThang";
            this.numThang.Location = new System.Drawing.Point(110, 90);
            this.numThang.Size = new System.Drawing.Size(150, 24);
            this.numThang.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            // lblLuong
            this.lblLuong.Name = "lblLuong";
            this.lblLuong.Location = new System.Drawing.Point(280, 93);
            this.lblLuong.Size = new System.Drawing.Size(90, 20);
            this.lblLuong.Text = "Lương thử việc";
            this.lblLuong.AutoSize = true;
            // numLuong
            this.numLuong.Name = "numLuong";
            this.numLuong.Location = new System.Drawing.Point(375, 90);
            this.numLuong.Size = new System.Drawing.Size(130, 24);
            this.numLuong.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numLuong.Increment = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numLuong.ThousandsSeparator = true;
            // lblDieuKhoan
            this.lblDieuKhoan.Name = "lblDieuKhoan";
            this.lblDieuKhoan.Location = new System.Drawing.Point(15, 128);
            this.lblDieuKhoan.Size = new System.Drawing.Size(90, 20);
            this.lblDieuKhoan.Text = "Điều khoản";
            this.lblDieuKhoan.AutoSize = true;
            // txtDieuKhoan
            this.txtDieuKhoan.Name = "txtDieuKhoan";
            this.txtDieuKhoan.Location = new System.Drawing.Point(110, 125);
            this.txtDieuKhoan.Size = new System.Drawing.Size(395, 90);
            this.txtDieuKhoan.Multiline = true;
            this.txtDieuKhoan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // btnThem
            this.btnThem.Name = "btnThem";
            this.btnThem.Location = new System.Drawing.Point(12, 288);
            this.btnThem.Size = new System.Drawing.Size(100, 34);
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // btnSua
            this.btnSua.Name = "btnSua";
            this.btnSua.Location = new System.Drawing.Point(122, 288);
            this.btnSua.Size = new System.Drawing.Size(100, 34);
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // btnXoa
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Location = new System.Drawing.Point(232, 288);
            this.btnXoa.Size = new System.Drawing.Size(100, 34);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // btnLamMoi
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Location = new System.Drawing.Point(342, 288);
            this.btnLamMoi.Size = new System.Drawing.Size(100, 34);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // btnThoat
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Location = new System.Drawing.Point(452, 288);
            this.btnThoat.Size = new System.Drawing.Size(100, 34);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // lblTim
            this.lblTim.Name = "lblTim";
            this.lblTim.Location = new System.Drawing.Point(600, 296);
            this.lblTim.Size = new System.Drawing.Size(90, 20);
            this.lblTim.Text = "Tìm (Mã/Tên):";
            this.lblTim.AutoSize = true;
            // txtTim
            this.txtTim.Name = "txtTim";
            this.txtTim.Location = new System.Drawing.Point(695, 293);
            this.txtTim.Size = new System.Drawing.Size(170, 24);
            this.txtTim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTim_KeyDown);
            // cboLocPB
            this.cboLocPB.Name = "cboLocPB";
            this.cboLocPB.Location = new System.Drawing.Point(875, 293);
            this.cboLocPB.Size = new System.Drawing.Size(140, 24);
            this.cboLocPB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // btnTim
            this.btnTim.Name = "btnTim";
            this.btnTim.Location = new System.Drawing.Point(1022, 291);
            this.btnTim.Size = new System.Drawing.Size(60, 28);
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // dgvNhanVien
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.Location = new System.Drawing.Point(12, 334);
            this.dgvNhanVien.Size = new System.Drawing.Size(1070, 300);
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.MultiSelect = false;
            this.dgvNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhanVien.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNhanVien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhanVien_CellClick);
            // lblTong
            this.lblTong.Name = "lblTong";
            this.lblTong.Location = new System.Drawing.Point(12, 642);
            this.lblTong.Size = new System.Drawing.Size(300, 20);
            this.lblTong.Text = "Tổng: 0 nhân viên";
            this.lblTong.AutoSize = true;
            this.lblTong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpNhanVien.Controls.Add(this.lblMaNV);
            this.grpNhanVien.Controls.Add(this.txtMaNV);
            this.grpNhanVien.Controls.Add(this.lblHoTen);
            this.grpNhanVien.Controls.Add(this.txtHoTen);
            this.grpNhanVien.Controls.Add(this.lblNgaySinh);
            this.grpNhanVien.Controls.Add(this.dtpNgaySinh);
            this.grpNhanVien.Controls.Add(this.lblGioiTinh);
            this.grpNhanVien.Controls.Add(this.cboGioiTinh);
            this.grpNhanVien.Controls.Add(this.lblSDT);
            this.grpNhanVien.Controls.Add(this.txtSDT);
            this.grpNhanVien.Controls.Add(this.lblEmail);
            this.grpNhanVien.Controls.Add(this.txtEmail);
            this.grpNhanVien.Controls.Add(this.lblPhongBan);
            this.grpNhanVien.Controls.Add(this.cboPhongBan);
            this.grpNhanVien.Controls.Add(this.lblTrangThai);
            this.grpNhanVien.Controls.Add(this.cboTrangThai);
            this.grpNhanVien.Controls.Add(this.lblDiaChi);
            this.grpNhanVien.Controls.Add(this.txtDiaChi);
            this.grpThuViec.Controls.Add(this.chkThuViec);
            this.grpThuViec.Controls.Add(this.lblSoQD);
            this.grpThuViec.Controls.Add(this.txtSoQD);
            this.grpThuViec.Controls.Add(this.lblNgayKy);
            this.grpThuViec.Controls.Add(this.dtpNgayKy);
            this.grpThuViec.Controls.Add(this.lblThang);
            this.grpThuViec.Controls.Add(this.numThang);
            this.grpThuViec.Controls.Add(this.lblLuong);
            this.grpThuViec.Controls.Add(this.numLuong);
            this.grpThuViec.Controls.Add(this.lblDieuKhoan);
            this.grpThuViec.Controls.Add(this.txtDieuKhoan);
            // FrmNhanVien
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 672);
            this.MinimumSize = new System.Drawing.Size(1110, 710);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.grpNhanVien);
            this.Controls.Add(this.grpThuViec);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.lblTim);
            this.Controls.Add(this.txtTim);
            this.Controls.Add(this.cboLocPB);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.dgvNhanVien);
            this.Controls.Add(this.lblTong);
            this.Name = "FrmNhanVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý nhân sự - Công ty sơn Đông Á";
            this.Load += new System.EventHandler(this.FrmNhanVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLuong)).EndInit();
            this.grpNhanVien.ResumeLayout(false);
            this.grpNhanVien.PerformLayout();
            this.grpThuViec.ResumeLayout(false);
            this.grpThuViec.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpNhanVien;
        private System.Windows.Forms.GroupBox grpThuViec;
        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhongBan;
        private System.Windows.Forms.ComboBox cboPhongBan;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.CheckBox chkThuViec;
        private System.Windows.Forms.Label lblSoQD;
        private System.Windows.Forms.TextBox txtSoQD;
        private System.Windows.Forms.Label lblNgayKy;
        private System.Windows.Forms.DateTimePicker dtpNgayKy;
        private System.Windows.Forms.Label lblThang;
        private System.Windows.Forms.NumericUpDown numThang;
        private System.Windows.Forms.Label lblLuong;
        private System.Windows.Forms.NumericUpDown numLuong;
        private System.Windows.Forms.Label lblDieuKhoan;
        private System.Windows.Forms.TextBox txtDieuKhoan;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.ComboBox cboLocPB;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.DataGridView dgvNhanVien;
        private System.Windows.Forms.Label lblTong;
    }
}
