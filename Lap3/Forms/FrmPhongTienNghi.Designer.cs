namespace Quanlykhachsang.Forms
{
    partial class FrmPhongTienNghi
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblNav = new System.Windows.Forms.Label();
            this.lblSoPhong = new System.Windows.Forms.Label();
            this.lblKhuVuc = new System.Windows.Forms.Label();
            this.lblSoNguoi = new System.Windows.Forms.Label();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtSoPhong = new System.Windows.Forms.TextBox();
            this.txtKhuVuc = new System.Windows.Forms.TextBox();
            this.txtSoNguoiToiDa = new System.Windows.Forms.TextBox();
            this.txtDonGiaNgay = new System.Windows.Forms.TextBox();
            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.lblSoPhieuLD = new System.Windows.Forms.Label();
            this.lblTienNghi = new System.Windows.Forms.Label();
            this.lblPhongLD = new System.Windows.Forms.Label();
            this.lblTinhTrang = new System.Windows.Forms.Label();
            this.txtSoPhieuLapDat = new System.Windows.Forms.TextBox();
            this.txtMaTienNghi = new System.Windows.Forms.TextBox();
            this.txtSoPhongLapDat = new System.Windows.Forms.TextBox();
            this.txtTinhTrang = new System.Windows.Forms.TextBox();
            this.btnLapPhieu = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            this.SuspendLayout();

            // lblNav
            this.lblNav.AutoSize = true;
            this.lblNav.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblNav.ForeColor = System.Drawing.Color.Navy;
            this.lblNav.Location = new System.Drawing.Point(30, 20);
            this.lblNav.Text = "[Phòng]  [Tiện nghi]  [Lắp đặt / luân chuyển]";

            // Hàng 1 - Thông tin phòng
            this.lblSoPhong.Text = "Số phòng:";
            this.lblSoPhong.Location = new System.Drawing.Point(30, 60);
            this.txtSoPhong.Location = new System.Drawing.Point(100, 57);
            this.txtSoPhong.Size = new System.Drawing.Size(110, 22);

            this.lblKhuVuc.Text = "Khu vực:";
            this.lblKhuVuc.Location = new System.Drawing.Point(230, 60);
            this.txtKhuVuc.Location = new System.Drawing.Point(290, 57);
            this.txtKhuVuc.Size = new System.Drawing.Size(120, 22);

            this.lblSoNguoi.Text = "Số người tối đa:";
            this.lblSoNguoi.Location = new System.Drawing.Point(430, 60);
            this.txtSoNguoiToiDa.Location = new System.Drawing.Point(530, 57);
            this.txtSoNguoiToiDa.Size = new System.Drawing.Size(50, 22);

            this.lblDonGia.Text = "Đơn giá/ngày:";
            this.lblDonGia.Location = new System.Drawing.Point(600, 60);
            this.txtDonGiaNgay.Location = new System.Drawing.Point(690, 57);
            this.txtDonGiaNgay.Size = new System.Drawing.Size(110, 22);

            // dgvPhong
            this.dgvPhong.AllowUserToAddRows = false;
            this.dgvPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhong.Location = new System.Drawing.Point(30, 100);
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.ReadOnly = true;
            this.dgvPhong.RowHeadersWidth = 51;
            this.dgvPhong.Size = new System.Drawing.Size(770, 220);
            this.dgvPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhong_CellClick);

            // Hàng 2 - Phiếu lắp đặt
            this.lblSoPhieuLD.Text = "Phiếu lắp đặt:";
            this.lblSoPhieuLD.Location = new System.Drawing.Point(30, 345);
            this.txtSoPhieuLapDat.Location = new System.Drawing.Point(115, 342);
            this.txtSoPhieuLapDat.Size = new System.Drawing.Size(100, 22);

            this.lblTienNghi.Text = "Tiện nghi:";
            this.lblTienNghi.Location = new System.Drawing.Point(230, 345);
            this.txtMaTienNghi.Location = new System.Drawing.Point(295, 342);
            this.txtMaTienNghi.Size = new System.Drawing.Size(100, 22);

            this.lblPhongLD.Text = "Phòng:";
            this.lblPhongLD.Location = new System.Drawing.Point(415, 345);
            this.txtSoPhongLapDat.Location = new System.Drawing.Point(465, 342);
            this.txtSoPhongLapDat.Size = new System.Drawing.Size(100, 22);

            this.lblTinhTrang.Text = "Tình trạng:";
            this.lblTinhTrang.Location = new System.Drawing.Point(580, 345);
            this.txtTinhTrang.Location = new System.Drawing.Point(650, 342);
            this.txtTinhTrang.Size = new System.Drawing.Size(150, 22);

            // Button Lập phiếu
            this.btnLapPhieu.Location = new System.Drawing.Point(680, 380);
            this.btnLapPhieu.Name = "btnLapPhieu";
            this.btnLapPhieu.Size = new System.Drawing.Size(120, 30);
            this.btnLapPhieu.Text = "Lập phiếu";
            this.btnLapPhieu.UseVisualStyleBackColor = true;
            this.btnLapPhieu.Click += new System.EventHandler(this.btnLapPhieu_Click);

            // FrmPhongTienNghi
            this.ClientSize = new System.Drawing.Size(830, 430);
            this.Controls.Add(this.lblNav);
            this.Controls.Add(this.lblSoPhong);
            this.Controls.Add(this.txtSoPhong);
            this.Controls.Add(this.lblKhuVuc);
            this.Controls.Add(this.txtKhuVuc);
            this.Controls.Add(this.lblSoNguoi);
            this.Controls.Add(this.txtSoNguoiToiDa);
            this.Controls.Add(this.lblDonGia);
            this.Controls.Add(this.txtDonGiaNgay);
            this.Controls.Add(this.dgvPhong);
            this.Controls.Add(this.lblSoPhieuLD);
            this.Controls.Add(this.txtSoPhieuLapDat);
            this.Controls.Add(this.lblTienNghi);
            this.Controls.Add(this.txtMaTienNghi);
            this.Controls.Add(this.lblPhongLD);
            this.Controls.Add(this.txtSoPhongLapDat);
            this.Controls.Add(this.lblTinhTrang);
            this.Controls.Add(this.txtTinhTrang);
            this.Controls.Add(this.btnLapPhieu);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phòng - Tiện nghi - Phiếu lắp đặt";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblNav;
        private System.Windows.Forms.Label lblSoPhong;
        private System.Windows.Forms.Label lblKhuVuc;
        private System.Windows.Forms.Label lblSoNguoi;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtSoPhong;
        private System.Windows.Forms.TextBox txtKhuVuc;
        private System.Windows.Forms.TextBox txtSoNguoiToiDa;
        private System.Windows.Forms.TextBox txtDonGiaNgay;
        private System.Windows.Forms.DataGridView dgvPhong;
        private System.Windows.Forms.Label lblSoPhieuLD;
        private System.Windows.Forms.Label lblTienNghi;
        private System.Windows.Forms.Label lblPhongLD;
        private System.Windows.Forms.Label lblTinhTrang;
        private System.Windows.Forms.TextBox txtSoPhieuLapDat;
        private System.Windows.Forms.TextBox txtMaTienNghi;
        private System.Windows.Forms.TextBox txtSoPhongLapDat;
        private System.Windows.Forms.TextBox txtTinhTrang;
        private System.Windows.Forms.Button btnLapPhieu;
    }
}