namespace QuanLyTourDuLich.GUI
{
    partial class frmLapPhieuDangKyDoan
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.grpTour = new System.Windows.Forms.GroupBox();
            this.lblTour = new System.Windows.Forms.Label();
            this.cboTour = new System.Windows.Forms.ComboBox();
            this.lblNgayDi = new System.Windows.Forms.Label();
            this.dtpNgayDi = new System.Windows.Forms.DateTimePicker();
            this.lblSoNgayDem = new System.Windows.Forms.Label();
            this.txtSoNgayDem = new System.Windows.Forms.TextBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.grpDoan = new System.Windows.Forms.GroupBox();
            this.lblTenCoQuan = new System.Windows.Forms.Label();
            this.txtTenCoQuan = new System.Windows.Forms.TextBox();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblNguoiDaiDien = new System.Windows.Forms.Label();
            this.txtNguoiDaiDien = new System.Windows.Forms.TextBox();
            this.lblSoNguoi = new System.Windows.Forms.Label();
            this.nudSoNguoi = new System.Windows.Forms.NumericUpDown();
            this.lblDiaDiemDon = new System.Windows.Forms.Label();
            this.txtDiaDiemDon = new System.Windows.Forms.TextBox();
            this.chkBaoHiem = new System.Windows.Forms.CheckBox();
            this.grpNguoiDi = new System.Windows.Forms.GroupBox();
            this.dgvNguoiDi = new System.Windows.Forms.DataGridView();
            this.grpCoc = new System.Windows.Forms.GroupBox();
            this.lblTienCoc = new System.Windows.Forms.Label();
            this.nudTienCoc = new System.Windows.Forms.NumericUpDown();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.btnLapPhieu = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.grpTour.SuspendLayout();
            this.grpDoan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNguoi)).BeginInit();
            this.grpNguoiDi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguoiDi)).BeginInit();
            this.grpCoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTienCoc)).BeginInit();
            this.SuspendLayout();
            //
            // lblTieuDe
            //
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTieuDe.Location = new System.Drawing.Point(0, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(800, 35);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "LẬP PHIẾU ĐĂNG KÝ TOUR THEO ĐOÀN";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // grpTour
            //
            this.grpTour.Controls.Add(this.lblTour);
            this.grpTour.Controls.Add(this.cboTour);
            this.grpTour.Controls.Add(this.lblNgayDi);
            this.grpTour.Controls.Add(this.dtpNgayDi);
            this.grpTour.Controls.Add(this.lblSoNgayDem);
            this.grpTour.Controls.Add(this.txtSoNgayDem);
            this.grpTour.Controls.Add(this.lblDonGia);
            this.grpTour.Controls.Add(this.txtDonGia);
            this.grpTour.Location = new System.Drawing.Point(15, 55);
            this.grpTour.Name = "grpTour";
            this.grpTour.Size = new System.Drawing.Size(770, 100);
            this.grpTour.TabIndex = 1;
            this.grpTour.TabStop = false;
            this.grpTour.Text = "Thông tin tour";
            //
            // lblTour
            //
            this.lblTour.AutoSize = true;
            this.lblTour.Location = new System.Drawing.Point(15, 30);
            this.lblTour.Name = "lblTour";
            this.lblTour.Text = "Tour";
            //
            // cboTour
            //
            this.cboTour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTour.FormattingEnabled = true;
            this.cboTour.Location = new System.Drawing.Point(110, 26);
            this.cboTour.Name = "cboTour";
            this.cboTour.Size = new System.Drawing.Size(380, 23);
            this.cboTour.TabIndex = 0;
            this.cboTour.SelectedIndexChanged += new System.EventHandler(this.cboTour_SelectedIndexChanged);
            //
            // lblNgayDi
            //
            this.lblNgayDi.AutoSize = true;
            this.lblNgayDi.Location = new System.Drawing.Point(510, 30);
            this.lblNgayDi.Name = "lblNgayDi";
            this.lblNgayDi.Text = "Ngày đi";
            //
            // dtpNgayDi
            //
            this.dtpNgayDi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayDi.Location = new System.Drawing.Point(580, 26);
            this.dtpNgayDi.Name = "dtpNgayDi";
            this.dtpNgayDi.Size = new System.Drawing.Size(170, 23);
            this.dtpNgayDi.TabIndex = 1;
            //
            // lblSoNgayDem
            //
            this.lblSoNgayDem.AutoSize = true;
            this.lblSoNgayDem.Location = new System.Drawing.Point(15, 64);
            this.lblSoNgayDem.Name = "lblSoNgayDem";
            this.lblSoNgayDem.Text = "Số ngày / đêm";
            //
            // txtSoNgayDem
            //
            this.txtSoNgayDem.Location = new System.Drawing.Point(110, 60);
            this.txtSoNgayDem.Name = "txtSoNgayDem";
            this.txtSoNgayDem.ReadOnly = true;
            this.txtSoNgayDem.Size = new System.Drawing.Size(130, 23);
            this.txtSoNgayDem.TabStop = false;
            //
            // lblDonGia
            //
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new System.Drawing.Point(270, 64);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Text = "Đơn giá / khách";
            //
            // txtDonGia
            //
            this.txtDonGia.Location = new System.Drawing.Point(375, 60);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.ReadOnly = true;
            this.txtDonGia.Size = new System.Drawing.Size(115, 23);
            this.txtDonGia.TabStop = false;
            this.txtDonGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // grpDoan
            //
            this.grpDoan.Controls.Add(this.lblTenCoQuan);
            this.grpDoan.Controls.Add(this.txtTenCoQuan);
            this.grpDoan.Controls.Add(this.lblDienThoai);
            this.grpDoan.Controls.Add(this.txtDienThoai);
            this.grpDoan.Controls.Add(this.lblDiaChi);
            this.grpDoan.Controls.Add(this.txtDiaChi);
            this.grpDoan.Controls.Add(this.lblNguoiDaiDien);
            this.grpDoan.Controls.Add(this.txtNguoiDaiDien);
            this.grpDoan.Controls.Add(this.lblSoNguoi);
            this.grpDoan.Controls.Add(this.nudSoNguoi);
            this.grpDoan.Controls.Add(this.lblDiaDiemDon);
            this.grpDoan.Controls.Add(this.txtDiaDiemDon);
            this.grpDoan.Location = new System.Drawing.Point(15, 165);
            this.grpDoan.Name = "grpDoan";
            this.grpDoan.Size = new System.Drawing.Size(770, 170);
            this.grpDoan.TabIndex = 2;
            this.grpDoan.TabStop = false;
            this.grpDoan.Text = "Thông tin đoàn";
            //
            // lblTenCoQuan
            //
            this.lblTenCoQuan.AutoSize = true;
            this.lblTenCoQuan.Location = new System.Drawing.Point(15, 30);
            this.lblTenCoQuan.Name = "lblTenCoQuan";
            this.lblTenCoQuan.Text = "Tên cơ quan";
            //
            // txtTenCoQuan
            //
            this.txtTenCoQuan.Location = new System.Drawing.Point(110, 26);
            this.txtTenCoQuan.MaxLength = 200;
            this.txtTenCoQuan.Name = "txtTenCoQuan";
            this.txtTenCoQuan.Size = new System.Drawing.Size(380, 23);
            this.txtTenCoQuan.TabIndex = 0;
            //
            // lblDienThoai
            //
            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Location = new System.Drawing.Point(510, 30);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Text = "Điện thoại";
            //
            // txtDienThoai
            //
            this.txtDienThoai.Location = new System.Drawing.Point(580, 26);
            this.txtDienThoai.MaxLength = 20;
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(170, 23);
            this.txtDienThoai.TabIndex = 1;
            //
            // lblDiaChi
            //
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(15, 64);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Text = "Địa chỉ";
            //
            // txtDiaChi
            //
            this.txtDiaChi.Location = new System.Drawing.Point(110, 60);
            this.txtDiaChi.MaxLength = 300;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(640, 23);
            this.txtDiaChi.TabIndex = 2;
            //
            // lblNguoiDaiDien
            //
            this.lblNguoiDaiDien.AutoSize = true;
            this.lblNguoiDaiDien.Location = new System.Drawing.Point(15, 98);
            this.lblNguoiDaiDien.Name = "lblNguoiDaiDien";
            this.lblNguoiDaiDien.Text = "Người đại diện";
            //
            // txtNguoiDaiDien
            //
            this.txtNguoiDaiDien.Location = new System.Drawing.Point(110, 94);
            this.txtNguoiDaiDien.MaxLength = 100;
            this.txtNguoiDaiDien.Name = "txtNguoiDaiDien";
            this.txtNguoiDaiDien.Size = new System.Drawing.Size(380, 23);
            this.txtNguoiDaiDien.TabIndex = 3;
            //
            // lblSoNguoi
            //
            this.lblSoNguoi.AutoSize = true;
            this.lblSoNguoi.Location = new System.Drawing.Point(510, 98);
            this.lblSoNguoi.Name = "lblSoNguoi";
            this.lblSoNguoi.Text = "Số người";
            //
            // nudSoNguoi
            //
            this.nudSoNguoi.Location = new System.Drawing.Point(580, 94);
            this.nudSoNguoi.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            this.nudSoNguoi.Minimum = new decimal(new int[] { 13, 0, 0, 0 });
            this.nudSoNguoi.Name = "nudSoNguoi";
            this.nudSoNguoi.Size = new System.Drawing.Size(80, 23);
            this.nudSoNguoi.TabIndex = 4;
            this.nudSoNguoi.Value = new decimal(new int[] { 13, 0, 0, 0 });
            this.nudSoNguoi.ValueChanged += new System.EventHandler(this.nudSoNguoi_ValueChanged);
            //
            // lblDiaDiemDon
            //
            this.lblDiaDiemDon.AutoSize = true;
            this.lblDiaDiemDon.Location = new System.Drawing.Point(15, 132);
            this.lblDiaDiemDon.Name = "lblDiaDiemDon";
            this.lblDiaDiemDon.Text = "Địa điểm đón";
            //
            // txtDiaDiemDon
            //
            this.txtDiaDiemDon.Location = new System.Drawing.Point(110, 128);
            this.txtDiaDiemDon.MaxLength = 300;
            this.txtDiaDiemDon.Name = "txtDiaDiemDon";
            this.txtDiaDiemDon.Size = new System.Drawing.Size(640, 23);
            this.txtDiaDiemDon.TabIndex = 5;
            //
            // chkBaoHiem
            //
            this.chkBaoHiem.AutoSize = true;
            this.chkBaoHiem.Location = new System.Drawing.Point(20, 345);
            this.chkBaoHiem.Name = "chkBaoHiem";
            this.chkBaoHiem.TabIndex = 3;
            this.chkBaoHiem.Text = "Đoàn có mua bảo hiểm (kèm danh sách người đi)";
            this.chkBaoHiem.UseVisualStyleBackColor = true;
            this.chkBaoHiem.CheckedChanged += new System.EventHandler(this.chkBaoHiem_CheckedChanged);
            //
            // grpNguoiDi
            //
            this.grpNguoiDi.Controls.Add(this.dgvNguoiDi);
            this.grpNguoiDi.Location = new System.Drawing.Point(15, 372);
            this.grpNguoiDi.Name = "grpNguoiDi";
            this.grpNguoiDi.Size = new System.Drawing.Size(770, 170);
            this.grpNguoiDi.TabIndex = 4;
            this.grpNguoiDi.TabStop = false;
            this.grpNguoiDi.Text = "Danh sách người đi";
            //
            // dgvNguoiDi
            //
            this.dgvNguoiDi.AllowUserToResizeRows = false;
            this.dgvNguoiDi.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvNguoiDi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNguoiDi.Location = new System.Drawing.Point(15, 25);
            this.dgvNguoiDi.Name = "dgvNguoiDi";
            this.dgvNguoiDi.RowHeadersWidth = 30;
            this.dgvNguoiDi.Size = new System.Drawing.Size(740, 135);
            this.dgvNguoiDi.TabIndex = 0;
            //
            // grpCoc
            //
            this.grpCoc.Controls.Add(this.lblTienCoc);
            this.grpCoc.Controls.Add(this.nudTienCoc);
            this.grpCoc.Controls.Add(this.lblTongTien);
            this.grpCoc.Controls.Add(this.txtTongTien);
            this.grpCoc.Location = new System.Drawing.Point(15, 550);
            this.grpCoc.Name = "grpCoc";
            this.grpCoc.Size = new System.Drawing.Size(770, 62);
            this.grpCoc.TabIndex = 5;
            this.grpCoc.TabStop = false;
            this.grpCoc.Text = "Đặt cọc";
            //
            // lblTienCoc
            //
            this.lblTienCoc.AutoSize = true;
            this.lblTienCoc.Location = new System.Drawing.Point(15, 28);
            this.lblTienCoc.Name = "lblTienCoc";
            this.lblTienCoc.Text = "Tiền đặt cọc";
            //
            // nudTienCoc
            //
            this.nudTienCoc.Increment = new decimal(new int[] { 100000, 0, 0, 0 });
            this.nudTienCoc.Location = new System.Drawing.Point(110, 24);
            this.nudTienCoc.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.nudTienCoc.Name = "nudTienCoc";
            this.nudTienCoc.Size = new System.Drawing.Size(150, 23);
            this.nudTienCoc.TabIndex = 0;
            this.nudTienCoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTienCoc.ThousandsSeparator = true;
            //
            // lblTongTien
            //
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Location = new System.Drawing.Point(300, 28);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Text = "Tổng tiền dự kiến";
            //
            // txtTongTien
            //
            this.txtTongTien.Location = new System.Drawing.Point(420, 24);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(170, 23);
            this.txtTongTien.TabStop = false;
            this.txtTongTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // btnLapPhieu
            //
            this.btnLapPhieu.Location = new System.Drawing.Point(440, 628);
            this.btnLapPhieu.Name = "btnLapPhieu";
            this.btnLapPhieu.Size = new System.Drawing.Size(110, 35);
            this.btnLapPhieu.TabIndex = 6;
            this.btnLapPhieu.Text = "Lập phiếu";
            this.btnLapPhieu.UseVisualStyleBackColor = true;
            this.btnLapPhieu.Click += new System.EventHandler(this.btnLapPhieu_Click);
            //
            // btnLamMoi
            //
            this.btnLamMoi.Location = new System.Drawing.Point(560, 628);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 35);
            this.btnLamMoi.TabIndex = 7;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            //
            // btnDong
            //
            this.btnDong.Location = new System.Drawing.Point(680, 628);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(105, 35);
            this.btnDong.TabIndex = 8;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            //
            // frmLapPhieuDangKyDoan
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 680);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnLapPhieu);
            this.Controls.Add(this.grpCoc);
            this.Controls.Add(this.grpNguoiDi);
            this.Controls.Add(this.chkBaoHiem);
            this.Controls.Add(this.grpDoan);
            this.Controls.Add(this.grpTour);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmLapPhieuDangKyDoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lập phiếu đăng ký tour theo đoàn - Công ty du lịch Văn Hóa Việt";
            this.Load += new System.EventHandler(this.frmLapPhieuDangKyDoan_Load);
            this.grpTour.ResumeLayout(false);
            this.grpTour.PerformLayout();
            this.grpDoan.ResumeLayout(false);
            this.grpDoan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNguoi)).EndInit();
            this.grpNguoiDi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguoiDi)).EndInit();
            this.grpCoc.ResumeLayout(false);
            this.grpCoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTienCoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpTour;
        private System.Windows.Forms.Label lblTour;
        private System.Windows.Forms.ComboBox cboTour;
        private System.Windows.Forms.Label lblNgayDi;
        private System.Windows.Forms.DateTimePicker dtpNgayDi;
        private System.Windows.Forms.Label lblSoNgayDem;
        private System.Windows.Forms.TextBox txtSoNgayDem;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.GroupBox grpDoan;
        private System.Windows.Forms.Label lblTenCoQuan;
        private System.Windows.Forms.TextBox txtTenCoQuan;
        private System.Windows.Forms.Label lblDienThoai;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblNguoiDaiDien;
        private System.Windows.Forms.TextBox txtNguoiDaiDien;
        private System.Windows.Forms.Label lblSoNguoi;
        private System.Windows.Forms.NumericUpDown nudSoNguoi;
        private System.Windows.Forms.Label lblDiaDiemDon;
        private System.Windows.Forms.TextBox txtDiaDiemDon;
        private System.Windows.Forms.CheckBox chkBaoHiem;
        private System.Windows.Forms.GroupBox grpNguoiDi;
        private System.Windows.Forms.DataGridView dgvNguoiDi;
        private System.Windows.Forms.GroupBox grpCoc;
        private System.Windows.Forms.Label lblTienCoc;
        private System.Windows.Forms.NumericUpDown nudTienCoc;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Button btnLapPhieu;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;
    }
}
