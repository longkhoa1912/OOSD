namespace QuanLyNGK.UI
{
    partial class frmBanHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpHoaDon = new System.Windows.Forms.GroupBox();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.dtpNgayLap = new System.Windows.Forms.DateTimePicker();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblTimKH = new System.Windows.Forms.Label();
            this.txtTimKH = new System.Windows.Forms.TextBox();
            this.btnTimKH = new System.Windows.Forms.Button();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblLoaiKhach = new System.Windows.Forms.Label();
            this.grpHang = new System.Windows.Forms.GroupBox();
            this.lblNGK = new System.Windows.Forms.Label();
            this.cboNGK = new System.Windows.Forms.ComboBox();
            this.lblThongTinNGK = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnThemHang = new System.Windows.Forms.Button();
            this.btnXoaHang = new System.Windows.Forms.Button();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.grpThanhToan = new System.Windows.Forms.GroupBox();
            this.lblTongTienCap = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblKhachTraCap = new System.Windows.Forms.Label();
            this.nudKhachTra = new System.Windows.Forms.NumericUpDown();
            this.lblConNoCap = new System.Windows.Forms.Label();
            this.lblConNo = new System.Windows.Forms.Label();
            this.lblNgayTraCap = new System.Windows.Forms.Label();
            this.dtpNgayTra = new System.Windows.Forms.DateTimePicker();
            this.lblLyDoCap = new System.Windows.Forms.Label();
            this.txtLyDoNo = new System.Windows.Forms.TextBox();
            this.lblNgayHenCap = new System.Windows.Forms.Label();
            this.dtpNgayHen = new System.Windows.Forms.DateTimePicker();
            this.lblCanhBao = new System.Windows.Forms.Label();
            this.btnLapHoaDon = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnDanhMuc = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTrangThai = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpHoaDon.SuspendLayout();
            this.grpHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.grpThanhToan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKhachTra)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // grpHoaDon
            //
            this.grpHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHoaDon.Controls.Add(this.lblMaHD);
            this.grpHoaDon.Controls.Add(this.txtMaHD);
            this.grpHoaDon.Controls.Add(this.lblNgayLap);
            this.grpHoaDon.Controls.Add(this.dtpNgayLap);
            this.grpHoaDon.Controls.Add(this.lblNhanVien);
            this.grpHoaDon.Controls.Add(this.cboNhanVien);
            this.grpHoaDon.Controls.Add(this.lblTimKH);
            this.grpHoaDon.Controls.Add(this.txtTimKH);
            this.grpHoaDon.Controls.Add(this.btnTimKH);
            this.grpHoaDon.Controls.Add(this.lblKhachHang);
            this.grpHoaDon.Controls.Add(this.cboKhachHang);
            this.grpHoaDon.Controls.Add(this.lblLoaiKhach);
            this.grpHoaDon.Location = new System.Drawing.Point(12, 8);
            this.grpHoaDon.Name = "grpHoaDon";
            this.grpHoaDon.Size = new System.Drawing.Size(936, 105);
            this.grpHoaDon.TabIndex = 0;
            this.grpHoaDon.TabStop = false;
            this.grpHoaDon.Text = "Thông tin hóa đơn";
            //
            // lblMaHD
            //
            this.lblMaHD.AutoSize = true;
            this.lblMaHD.Location = new System.Drawing.Point(20, 31);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Text = "Mã hóa đơn:";
            //
            // txtMaHD
            //
            this.txtMaHD.Location = new System.Drawing.Point(100, 28);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.ReadOnly = true;
            this.txtMaHD.Size = new System.Drawing.Size(120, 23);
            this.txtMaHD.TabIndex = 0;
            this.txtMaHD.TabStop = false;
            //
            // lblNgayLap
            //
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(250, 31);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Text = "Ngày lập:";
            //
            // dtpNgayLap
            //
            this.dtpNgayLap.Enabled = false;
            this.dtpNgayLap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayLap.Location = new System.Drawing.Point(320, 28);
            this.dtpNgayLap.Name = "dtpNgayLap";
            this.dtpNgayLap.Size = new System.Drawing.Size(120, 23);
            this.dtpNgayLap.TabIndex = 1;
            //
            // lblNhanVien
            //
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Location = new System.Drawing.Point(480, 31);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Text = "Nhân viên:";
            //
            // cboNhanVien
            //
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.Location = new System.Drawing.Point(550, 28);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(240, 23);
            this.cboNhanVien.TabIndex = 2;
            //
            // lblTimKH
            //
            this.lblTimKH.AutoSize = true;
            this.lblTimKH.Location = new System.Drawing.Point(20, 67);
            this.lblTimKH.Name = "lblTimKH";
            this.lblTimKH.Text = "SĐT / Mã KH:";
            //
            // txtTimKH
            //
            this.txtTimKH.Location = new System.Drawing.Point(100, 64);
            this.txtTimKH.Name = "txtTimKH";
            this.txtTimKH.Size = new System.Drawing.Size(120, 23);
            this.txtTimKH.TabIndex = 3;
            this.txtTimKH.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimKH_KeyDown);
            //
            // btnTimKH
            //
            this.btnTimKH.Location = new System.Drawing.Point(228, 62);
            this.btnTimKH.Name = "btnTimKH";
            this.btnTimKH.Size = new System.Drawing.Size(60, 27);
            this.btnTimKH.TabIndex = 4;
            this.btnTimKH.Text = "Tìm";
            this.btnTimKH.UseVisualStyleBackColor = true;
            this.btnTimKH.Click += new System.EventHandler(this.btnTimKH_Click);
            //
            // lblKhachHang
            //
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Location = new System.Drawing.Point(310, 67);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Text = "Khách hàng:";
            //
            // cboKhachHang
            //
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.Location = new System.Drawing.Point(390, 64);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(280, 23);
            this.cboKhachHang.TabIndex = 5;
            this.cboKhachHang.SelectedIndexChanged += new System.EventHandler(this.cboKhachHang_SelectedIndexChanged);
            //
            // lblLoaiKhach
            //
            this.lblLoaiKhach.AutoSize = true;
            this.lblLoaiKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoaiKhach.Location = new System.Drawing.Point(680, 67);
            this.lblLoaiKhach.Name = "lblLoaiKhach";
            this.lblLoaiKhach.Text = "";
            //
            // grpHang
            //
            this.grpHang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHang.Controls.Add(this.lblNGK);
            this.grpHang.Controls.Add(this.cboNGK);
            this.grpHang.Controls.Add(this.lblThongTinNGK);
            this.grpHang.Controls.Add(this.lblSoLuong);
            this.grpHang.Controls.Add(this.nudSoLuong);
            this.grpHang.Controls.Add(this.btnThemHang);
            this.grpHang.Controls.Add(this.btnXoaHang);
            this.grpHang.Location = new System.Drawing.Point(12, 119);
            this.grpHang.Name = "grpHang";
            this.grpHang.Size = new System.Drawing.Size(936, 62);
            this.grpHang.TabIndex = 1;
            this.grpHang.TabStop = false;
            this.grpHang.Text = "Chọn mặt hàng";
            //
            // lblNGK
            //
            this.lblNGK.AutoSize = true;
            this.lblNGK.Location = new System.Drawing.Point(20, 28);
            this.lblNGK.Name = "lblNGK";
            this.lblNGK.Text = "Mặt hàng:";
            //
            // cboNGK
            //
            this.cboNGK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNGK.Location = new System.Drawing.Point(90, 25);
            this.cboNGK.Name = "cboNGK";
            this.cboNGK.Size = new System.Drawing.Size(300, 23);
            this.cboNGK.TabIndex = 0;
            this.cboNGK.SelectedIndexChanged += new System.EventHandler(this.cboNGK_SelectedIndexChanged);
            //
            // lblThongTinNGK
            //
            this.lblThongTinNGK.Location = new System.Drawing.Point(400, 28);
            this.lblThongTinNGK.Name = "lblThongTinNGK";
            this.lblThongTinNGK.Size = new System.Drawing.Size(230, 20);
            this.lblThongTinNGK.Text = "";
            //
            // lblSoLuong
            //
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(640, 28);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Text = "SL:";
            //
            // nudSoLuong
            //
            this.nudSoLuong.Location = new System.Drawing.Point(670, 25);
            this.nudSoLuong.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(70, 23);
            this.nudSoLuong.TabIndex = 1;
            this.nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnThemHang
            //
            this.btnThemHang.Location = new System.Drawing.Point(750, 22);
            this.btnThemHang.Name = "btnThemHang";
            this.btnThemHang.Size = new System.Drawing.Size(85, 28);
            this.btnThemHang.TabIndex = 2;
            this.btnThemHang.Text = "Thêm";
            this.btnThemHang.UseVisualStyleBackColor = true;
            this.btnThemHang.Click += new System.EventHandler(this.btnThemHang_Click);
            //
            // btnXoaHang
            //
            this.btnXoaHang.Location = new System.Drawing.Point(843, 22);
            this.btnXoaHang.Name = "btnXoaHang";
            this.btnXoaHang.Size = new System.Drawing.Size(80, 28);
            this.btnXoaHang.TabIndex = 3;
            this.btnXoaHang.Text = "Xóa dòng";
            this.btnXoaHang.UseVisualStyleBackColor = true;
            this.btnXoaHang.Click += new System.EventHandler(this.btnXoaHang_Click);
            //
            // dgvChiTiet
            //
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.Location = new System.Drawing.Point(12, 190);
            this.dgvChiTiet.MultiSelect = false;
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(936, 240);
            this.dgvChiTiet.TabIndex = 2;
            //
            // grpThanhToan
            //
            this.grpThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThanhToan.Controls.Add(this.lblTongTienCap);
            this.grpThanhToan.Controls.Add(this.lblTongTien);
            this.grpThanhToan.Controls.Add(this.lblKhachTraCap);
            this.grpThanhToan.Controls.Add(this.nudKhachTra);
            this.grpThanhToan.Controls.Add(this.lblConNoCap);
            this.grpThanhToan.Controls.Add(this.lblConNo);
            this.grpThanhToan.Controls.Add(this.lblNgayTraCap);
            this.grpThanhToan.Controls.Add(this.dtpNgayTra);
            this.grpThanhToan.Controls.Add(this.lblLyDoCap);
            this.grpThanhToan.Controls.Add(this.txtLyDoNo);
            this.grpThanhToan.Controls.Add(this.lblNgayHenCap);
            this.grpThanhToan.Controls.Add(this.dtpNgayHen);
            this.grpThanhToan.Controls.Add(this.lblCanhBao);
            this.grpThanhToan.Location = new System.Drawing.Point(12, 438);
            this.grpThanhToan.Name = "grpThanhToan";
            this.grpThanhToan.Size = new System.Drawing.Size(936, 170);
            this.grpThanhToan.TabIndex = 3;
            this.grpThanhToan.TabStop = false;
            this.grpThanhToan.Text = "Thanh toán / công nợ / giao hàng";
            //
            // lblTongTienCap
            //
            this.lblTongTienCap.AutoSize = true;
            this.lblTongTienCap.Location = new System.Drawing.Point(20, 34);
            this.lblTongTienCap.Name = "lblTongTienCap";
            this.lblTongTienCap.Text = "Tổng tiền:";
            //
            // lblTongTien
            //
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTongTien.Location = new System.Drawing.Point(110, 26);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Text = "0 đ";
            //
            // lblKhachTraCap
            //
            this.lblKhachTraCap.AutoSize = true;
            this.lblKhachTraCap.Location = new System.Drawing.Point(20, 72);
            this.lblKhachTraCap.Name = "lblKhachTraCap";
            this.lblKhachTraCap.Text = "Khách trả:";
            //
            // nudKhachTra
            //
            this.nudKhachTra.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            this.nudKhachTra.Location = new System.Drawing.Point(110, 69);
            this.nudKhachTra.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.nudKhachTra.Name = "nudKhachTra";
            this.nudKhachTra.Size = new System.Drawing.Size(180, 23);
            this.nudKhachTra.TabIndex = 0;
            this.nudKhachTra.ThousandsSeparator = true;
            this.nudKhachTra.ValueChanged += new System.EventHandler(this.nudKhachTra_ValueChanged);
            //
            // lblConNoCap
            //
            this.lblConNoCap.AutoSize = true;
            this.lblConNoCap.Location = new System.Drawing.Point(20, 108);
            this.lblConNoCap.Name = "lblConNoCap";
            this.lblConNoCap.Text = "Còn nợ:";
            //
            // lblConNo
            //
            this.lblConNo.AutoSize = true;
            this.lblConNo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblConNo.Location = new System.Drawing.Point(110, 106);
            this.lblConNo.Name = "lblConNo";
            this.lblConNo.Text = "0 đ";
            //
            // lblNgayTraCap
            //
            this.lblNgayTraCap.AutoSize = true;
            this.lblNgayTraCap.Location = new System.Drawing.Point(340, 34);
            this.lblNgayTraCap.Name = "lblNgayTraCap";
            this.lblNgayTraCap.Text = "Ngày dự kiến trả:";
            //
            // dtpNgayTra
            //
            this.dtpNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayTra.Location = new System.Drawing.Point(470, 31);
            this.dtpNgayTra.Name = "dtpNgayTra";
            this.dtpNgayTra.Size = new System.Drawing.Size(140, 23);
            this.dtpNgayTra.TabIndex = 1;
            //
            // lblLyDoCap
            //
            this.lblLyDoCap.AutoSize = true;
            this.lblLyDoCap.Location = new System.Drawing.Point(340, 72);
            this.lblLyDoCap.Name = "lblLyDoCap";
            this.lblLyDoCap.Text = "Lý do nợ:";
            //
            // txtLyDoNo
            //
            this.txtLyDoNo.Location = new System.Drawing.Point(470, 69);
            this.txtLyDoNo.MaxLength = 200;
            this.txtLyDoNo.Name = "txtLyDoNo";
            this.txtLyDoNo.Size = new System.Drawing.Size(440, 23);
            this.txtLyDoNo.TabIndex = 2;
            //
            // lblNgayHenCap
            //
            this.lblNgayHenCap.AutoSize = true;
            this.lblNgayHenCap.Location = new System.Drawing.Point(340, 108);
            this.lblNgayHenCap.Name = "lblNgayHenCap";
            this.lblNgayHenCap.Text = "Ngày hẹn giao:";
            //
            // dtpNgayHen
            //
            this.dtpNgayHen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHen.Location = new System.Drawing.Point(470, 105);
            this.dtpNgayHen.Name = "dtpNgayHen";
            this.dtpNgayHen.Size = new System.Drawing.Size(140, 23);
            this.dtpNgayHen.TabIndex = 3;
            //
            // lblCanhBao
            //
            this.lblCanhBao.ForeColor = System.Drawing.Color.Firebrick;
            this.lblCanhBao.Location = new System.Drawing.Point(20, 135);
            this.lblCanhBao.Name = "lblCanhBao";
            this.lblCanhBao.Size = new System.Drawing.Size(890, 32);
            this.lblCanhBao.Text = "";
            //
            // btnLapHoaDon
            //
            this.btnLapHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLapHoaDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLapHoaDon.Location = new System.Drawing.Point(12, 618);
            this.btnLapHoaDon.Name = "btnLapHoaDon";
            this.btnLapHoaDon.Size = new System.Drawing.Size(150, 38);
            this.btnLapHoaDon.TabIndex = 4;
            this.btnLapHoaDon.Text = "Lập hóa đơn";
            this.btnLapHoaDon.UseVisualStyleBackColor = true;
            this.btnLapHoaDon.Click += new System.EventHandler(this.btnLapHoaDon_Click);
            //
            // btnLamMoi
            //
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLamMoi.Location = new System.Drawing.Point(172, 618);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 38);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            //
            // btnDanhMuc
            //
            this.btnDanhMuc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDanhMuc.Location = new System.Drawing.Point(302, 618);
            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnDanhMuc.Size = new System.Drawing.Size(160, 38);
            this.btnDanhMuc.TabIndex = 6;
            this.btnDanhMuc.Text = "Danh mục NGK...";
            this.btnDanhMuc.UseVisualStyleBackColor = true;
            this.btnDanhMuc.Click += new System.EventHandler(this.btnDanhMuc_Click);
            //
            // btnDong
            //
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(848, 618);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 38);
            this.btnDong.TabIndex = 7;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            //
            // statusStrip1
            //
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblTrangThai });
            this.statusStrip1.Location = new System.Drawing.Point(0, 658);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(960, 22);
            this.statusStrip1.TabIndex = 8;
            //
            // lblTrangThai
            //
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(39, 17);
            this.lblTrangThai.Text = "Sẵn sàng";
            //
            // frmBanHang
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 680);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnDanhMuc);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnLapHoaDon);
            this.Controls.Add(this.grpThanhToan);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.grpHang);
            this.Controls.Add(this.grpHoaDon);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(976, 719);
            this.Name = "frmBanHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bán hàng & Lập hóa đơn";
            this.Load += new System.EventHandler(this.frmBanHang_Load);
            this.grpHoaDon.ResumeLayout(false);
            this.grpHoaDon.PerformLayout();
            this.grpHang.ResumeLayout(false);
            this.grpHang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.grpThanhToan.ResumeLayout(false);
            this.grpThanhToan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKhachTra)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.GroupBox grpHoaDon;
        private System.Windows.Forms.Label lblMaHD;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.Label lblNgayLap;
        private System.Windows.Forms.DateTimePicker dtpNgayLap;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblTimKH;
        private System.Windows.Forms.TextBox txtTimKH;
        private System.Windows.Forms.Button btnTimKH;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblLoaiKhach;
        private System.Windows.Forms.GroupBox grpHang;
        private System.Windows.Forms.Label lblNGK;
        private System.Windows.Forms.ComboBox cboNGK;
        private System.Windows.Forms.Label lblThongTinNGK;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Button btnThemHang;
        private System.Windows.Forms.Button btnXoaHang;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.GroupBox grpThanhToan;
        private System.Windows.Forms.Label lblTongTienCap;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblKhachTraCap;
        private System.Windows.Forms.NumericUpDown nudKhachTra;
        private System.Windows.Forms.Label lblConNoCap;
        private System.Windows.Forms.Label lblConNo;
        private System.Windows.Forms.Label lblNgayTraCap;
        private System.Windows.Forms.DateTimePicker dtpNgayTra;
        private System.Windows.Forms.Label lblLyDoCap;
        private System.Windows.Forms.TextBox txtLyDoNo;
        private System.Windows.Forms.Label lblNgayHenCap;
        private System.Windows.Forms.DateTimePicker dtpNgayHen;
        private System.Windows.Forms.Label lblCanhBao;
        private System.Windows.Forms.Button btnLapHoaDon;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDanhMuc;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTrangThai;
    }
}
