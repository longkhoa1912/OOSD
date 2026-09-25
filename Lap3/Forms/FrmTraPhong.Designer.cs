namespace Quanlykhachsang.Forms
{
    partial class FrmTraPhong
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
            this.lblPhieuDangO = new System.Windows.Forms.Label();
            this.txtPhieuDangO = new System.Windows.Forms.TextBox();

            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.dgvTienNghi = new System.Windows.Forms.DataGridView();
            this.dgvTienNghiDenBu = new System.Windows.Forms.DataGridView();

            this.lblSoPhieuDenBu = new System.Windows.Forms.Label();
            this.txtSoPhieuDenBu = new System.Windows.Forms.TextBox();
            this.lblMucDo = new System.Windows.Forms.Label();
            this.txtMucDo = new System.Windows.Forms.TextBox();
            this.lblSoTienDenBu = new System.Windows.Forms.Label();
            this.txtSoTienDenBu = new System.Windows.Forms.TextBox();
            this.btnLapPhieuDenBu = new System.Windows.Forms.Button();

            this.lblSoHoaDon = new System.Windows.Forms.Label();
            this.txtSoHoaDon = new System.Windows.Forms.TextBox();
            this.lblSoNgayTinhTien = new System.Windows.Forms.Label();
            this.txtSoNgayTinhTien = new System.Windows.Forms.TextBox();
            this.btnLapHoaDon = new System.Windows.Forms.Button();

            this.dgvHoaDon = new System.Windows.Forms.DataGridView();

            this.lblHinhThuc = new System.Windows.Forms.Label();
            this.txtHinhThuc = new System.Windows.Forms.TextBox();
            this.lblSoTienThanhToan = new System.Windows.Forms.Label();
            this.txtSoTienThanhToan = new System.Windows.Forms.TextBox();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnHoanTatTraPhong = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTienNghi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTienNghiDenBu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.SuspendLayout();

            // lblPhieuDangO & txtPhieuDangO
            this.lblPhieuDangO.AutoSize = true;
            this.lblPhieuDangO.Location = new System.Drawing.Point(25, 20);
            this.lblPhieuDangO.Text = "Phiếu đang ở:";

            this.txtPhieuDangO.Location = new System.Drawing.Point(120, 17);
            this.txtPhieuDangO.Size = new System.Drawing.Size(120, 22);
            this.txtPhieuDangO.TextChanged += new System.EventHandler(this.txtPhieuDangO_TextChanged);

            // 3 Grids Top Section
            this.dgvPhong.AllowUserToAddRows = false;
            this.dgvPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhong.Location = new System.Drawing.Point(25, 55);
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.ReadOnly = true;
            this.dgvPhong.RowHeadersWidth = 30;
            this.dgvPhong.Size = new System.Drawing.Size(260, 140);
            this.dgvPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhong_CellClick);

            this.dgvTienNghi.AllowUserToAddRows = false;
            this.dgvTienNghi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTienNghi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTienNghi.Location = new System.Drawing.Point(300, 55);
            this.dgvTienNghi.Name = "dgvTienNghi";
            this.dgvTienNghi.ReadOnly = true;
            this.dgvTienNghi.RowHeadersWidth = 30;
            this.dgvTienNghi.Size = new System.Drawing.Size(280, 140);
            this.dgvTienNghi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTienNghi_CellClick);

            this.dgvTienNghiDenBu.AllowUserToAddRows = false;
            this.dgvTienNghiDenBu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTienNghiDenBu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTienNghiDenBu.Location = new System.Drawing.Point(595, 55);
            this.dgvTienNghiDenBu.Name = "dgvTienNghiDenBu";
            this.dgvTienNghiDenBu.ReadOnly = true;
            this.dgvTienNghiDenBu.RowHeadersWidth = 30;
            this.dgvTienNghiDenBu.Size = new System.Drawing.Size(280, 140);

            // Row Đền bù
            this.lblSoPhieuDenBu.Text = "Số phiếu đền bù:";
            this.lblSoPhieuDenBu.Location = new System.Drawing.Point(25, 215);
            this.lblSoPhieuDenBu.AutoSize = true;

            this.txtSoPhieuDenBu.Location = new System.Drawing.Point(125, 212);
            this.txtSoPhieuDenBu.Size = new System.Drawing.Size(110, 22);

            this.lblMucDo.Text = "Mức độ:";
            this.lblMucDo.Location = new System.Drawing.Point(250, 215);
            this.lblMucDo.AutoSize = true;

            this.txtMucDo.Location = new System.Drawing.Point(310, 212);
            this.txtMucDo.Size = new System.Drawing.Size(120, 22);

            this.lblSoTienDenBu.Text = "Số tiền:";
            this.lblSoTienDenBu.Location = new System.Drawing.Point(445, 215);
            this.lblSoTienDenBu.AutoSize = true;

            this.txtSoTienDenBu.Location = new System.Drawing.Point(500, 212);
            this.txtSoTienDenBu.Size = new System.Drawing.Size(110, 22);

            this.btnLapPhieuDenBu.Location = new System.Drawing.Point(725, 208);
            this.btnLapPhieuDenBu.Size = new System.Drawing.Size(150, 30);
            this.btnLapPhieuDenBu.Text = "Lập phiếu đền bù";
            this.btnLapPhieuDenBu.UseVisualStyleBackColor = true;
            this.btnLapPhieuDenBu.Click += new System.EventHandler(this.btnLapPhieuDenBu_Click);

            // Row Hóa đơn
            this.lblSoHoaDon.Text = "Số hóa đơn:";
            this.lblSoHoaDon.Location = new System.Drawing.Point(25, 255);
            this.lblSoHoaDon.AutoSize = true;

            this.txtSoHoaDon.Location = new System.Drawing.Point(110, 252);
            this.txtSoHoaDon.Size = new System.Drawing.Size(110, 22);

            this.lblSoNgayTinhTien.Text = "Số ngày tính tiền:";
            this.lblSoNgayTinhTien.Location = new System.Drawing.Point(235, 255);
            this.lblSoNgayTinhTien.AutoSize = true;

            this.txtSoNgayTinhTien.Location = new System.Drawing.Point(345, 252);
            this.txtSoNgayTinhTien.Size = new System.Drawing.Size(70, 22);

            this.btnLapHoaDon.Location = new System.Drawing.Point(520, 248);
            this.btnLapHoaDon.Size = new System.Drawing.Size(120, 30);
            this.btnLapHoaDon.Text = "Lập hóa đơn";
            this.btnLapHoaDon.UseVisualStyleBackColor = true;
            this.btnLapHoaDon.Click += new System.EventHandler(this.btnLapHoaDon_Click);

            // DataGridView Hóa Đơn
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Location = new System.Drawing.Point(25, 290);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.ReadOnly = true;
            this.dgvHoaDon.RowHeadersWidth = 30;
            this.dgvHoaDon.Size = new System.Drawing.Size(850, 140);
            this.dgvHoaDon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHoaDon_CellClick);

            // Row Bottom - Thanh toán & Hoàn tất
            this.lblHinhThuc.Text = "Hình thức:";
            this.lblHinhThuc.Location = new System.Drawing.Point(25, 450);
            this.lblHinhThuc.AutoSize = true;

            this.txtHinhThuc.Location = new System.Drawing.Point(95, 447);
            this.txtHinhThuc.Size = new System.Drawing.Size(110, 22);

            this.lblSoTienThanhToan.Text = "Số tiền:";
            this.lblSoTienThanhToan.Location = new System.Drawing.Point(220, 450);
            this.lblSoTienThanhToan.AutoSize = true;

            this.txtSoTienThanhToan.Location = new System.Drawing.Point(275, 447);
            this.txtSoTienThanhToan.Size = new System.Drawing.Size(110, 22);

            this.btnThanhToan.Location = new System.Drawing.Point(480, 443);
            this.btnThanhToan.Size = new System.Drawing.Size(110, 30);
            this.btnThanhToan.Text = "Thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);

            this.btnHoanTatTraPhong.Location = new System.Drawing.Point(610, 443);
            this.btnHoanTatTraPhong.Size = new System.Drawing.Size(150, 30);
            this.btnHoanTatTraPhong.Text = "Hoàn tất trả phòng";
            this.btnHoanTatTraPhong.UseVisualStyleBackColor = true;
            this.btnHoanTatTraPhong.Click += new System.EventHandler(this.btnHoanTatTraPhong_Click);

            // FrmTraPhong
            this.ClientSize = new System.Drawing.Size(900, 495);
            this.Controls.Add(this.lblPhieuDangO);
            this.Controls.Add(this.txtPhieuDangO);
            this.Controls.Add(this.dgvPhong);
            this.Controls.Add(this.dgvTienNghi);
            this.Controls.Add(this.dgvTienNghiDenBu);
            this.Controls.Add(this.lblSoPhieuDenBu);
            this.Controls.Add(this.txtSoPhieuDenBu);
            this.Controls.Add(this.lblMucDo);
            this.Controls.Add(this.txtMucDo);
            this.Controls.Add(this.lblSoTienDenBu);
            this.Controls.Add(this.txtSoTienDenBu);
            this.Controls.Add(this.btnLapPhieuDenBu);
            this.Controls.Add(this.lblSoHoaDon);
            this.Controls.Add(this.txtSoHoaDon);
            this.Controls.Add(this.lblSoNgayTinhTien);
            this.Controls.Add(this.txtSoNgayTinhTien);
            this.Controls.Add(this.btnLapHoaDon);
            this.Controls.Add(this.dgvHoaDon);
            this.Controls.Add(this.lblHinhThuc);
            this.Controls.Add(this.txtHinhThuc);
            this.Controls.Add(this.lblSoTienThanhToan);
            this.Controls.Add(this.txtSoTienThanhToan);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.btnHoanTatTraPhong);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trả phòng - Đền bù - Hóa đơn - Thanh toán";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTienNghi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTienNghiDenBu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblPhieuDangO;
        private System.Windows.Forms.TextBox txtPhieuDangO;
        private System.Windows.Forms.DataGridView dgvPhong;
        private System.Windows.Forms.DataGridView dgvTienNghi;
        private System.Windows.Forms.DataGridView dgvTienNghiDenBu;
        private System.Windows.Forms.Label lblSoPhieuDenBu;
        private System.Windows.Forms.TextBox txtSoPhieuDenBu;
        private System.Windows.Forms.Label lblMucDo;
        private System.Windows.Forms.TextBox txtMucDo;
        private System.Windows.Forms.Label lblSoTienDenBu;
        private System.Windows.Forms.TextBox txtSoTienDenBu;
        private System.Windows.Forms.Button btnLapPhieuDenBu;
        private System.Windows.Forms.Label lblSoHoaDon;
        private System.Windows.Forms.TextBox txtSoHoaDon;
        private System.Windows.Forms.Label lblSoNgayTinhTien;
        private System.Windows.Forms.TextBox txtSoNgayTinhTien;
        private System.Windows.Forms.Button btnLapHoaDon;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.Label lblHinhThuc;
        private System.Windows.Forms.TextBox txtHinhThuc;
        private System.Windows.Forms.Label lblSoTienThanhToan;
        private System.Windows.Forms.TextBox txtSoTienThanhToan;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Button btnHoanTatTraPhong;
    }
}