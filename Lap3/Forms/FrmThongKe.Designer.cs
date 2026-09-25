namespace Quanlykhachsang.Forms
{
    partial class FrmThongKe
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
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnThongKe = new System.Windows.Forms.Button();

            this.lblPhieuDat = new System.Windows.Forms.Label();
            this.lblDangO = new System.Windows.Forms.Label();
            this.lblHoaDon = new System.Windows.Forms.Label();
            this.lblDoanhThuHD = new System.Windows.Forms.Label();
            this.lblTongDenBu = new System.Windows.Forms.Label();

            this.lblDichVuSuDung = new System.Windows.Forms.Label();
            this.dgvThongKeDichVu = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeDichVu)).BeginInit();
            this.SuspendLayout();

            // lblTuNgay
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTuNgay.Location = new System.Drawing.Point(30, 25);
            this.lblTuNgay.Text = "Từ ngày:";

            // dtpTuNgay
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTuNgay.Location = new System.Drawing.Point(100, 22);
            this.dtpTuNgay.Size = new System.Drawing.Size(130, 30);

            // lblDenNgay
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDenNgay.Location = new System.Drawing.Point(260, 25);
            this.lblDenNgay.Text = "Đến ngày:";

            // dtpDenNgay
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDenNgay.Location = new System.Drawing.Point(340, 22);
            this.dtpDenNgay.Size = new System.Drawing.Size(130, 30);

            // btnThongKe
            this.btnThongKe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btnThongKe.Location = new System.Drawing.Point(500, 20);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(110, 34);
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);

            // lblPhieuDat
            this.lblPhieuDat.AutoSize = true;
            this.lblPhieuDat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPhieuDat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(129)))));
            this.lblPhieuDat.Location = new System.Drawing.Point(30, 80);
            this.lblPhieuDat.Text = "Phiếu đặt: 0";

            // lblDangO
            this.lblDangO.AutoSize = true;
            this.lblDangO.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDangO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(129)))));
            this.lblDangO.Location = new System.Drawing.Point(390, 80);
            this.lblDangO.Text = "Đang ở: 0";

            // lblHoaDon
            this.lblHoaDon.AutoSize = true;
            this.lblHoaDon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHoaDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(129)))));
            this.lblHoaDon.Location = new System.Drawing.Point(30, 125);
            this.lblHoaDon.Text = "Hóa đơn: 0";

            // lblDoanhThuHD
            this.lblDoanhThuHD.AutoSize = true;
            this.lblDoanhThuHD.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDoanhThuHD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(129)))));
            this.lblDoanhThuHD.Location = new System.Drawing.Point(390, 125);
            this.lblDoanhThuHD.Text = "Doanh thu HĐ: 0 đ";

            // lblTongDenBu
            this.lblTongDenBu.AutoSize = true;
            this.lblTongDenBu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongDenBu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(129)))));
            this.lblTongDenBu.Location = new System.Drawing.Point(30, 170);
            this.lblTongDenBu.Text = "Tổng đền bù: 0 đ";

            // lblDichVuSuDung
            this.lblDichVuSuDung.AutoSize = true;
            this.lblDichVuSuDung.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDichVuSuDung.Location = new System.Drawing.Point(30, 220);
            this.lblDichVuSuDung.Text = "Dịch vụ sử dụng:";

            // dgvThongKeDichVu
            this.dgvThongKeDichVu.AllowUserToAddRows = false;
            this.dgvThongKeDichVu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThongKeDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongKeDichVu.Location = new System.Drawing.Point(30, 250);
            this.dgvThongKeDichVu.Name = "dgvThongKeDichVu";
            this.dgvThongKeDichVu.ReadOnly = true;
            this.dgvThongKeDichVu.RowHeadersWidth = 30;
            this.dgvThongKeDichVu.Size = new System.Drawing.Size(680, 200);

            // FrmThongKe
            this.ClientSize = new System.Drawing.Size(740, 480);
            this.Controls.Add(this.lblTuNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.lblDenNgay);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.lblPhieuDat);
            this.Controls.Add(this.lblDangO);
            this.Controls.Add(this.lblHoaDon);
            this.Controls.Add(this.lblDoanhThuHD);
            this.Controls.Add(this.lblTongDenBu);
            this.Controls.Add(this.lblDichVuSuDung);
            this.Controls.Add(this.dgvThongKeDichVu);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê khách sạn";
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeDichVu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Label lblPhieuDat;
        private System.Windows.Forms.Label lblDangO;
        private System.Windows.Forms.Label lblHoaDon;
        private System.Windows.Forms.Label lblDoanhThuHD;
        private System.Windows.Forms.Label lblTongDenBu;
        private System.Windows.Forms.Label lblDichVuSuDung;
        private System.Windows.Forms.DataGridView dgvThongKeDichVu;
    }
}