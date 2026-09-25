namespace Quanlykhachsang.Forms
{
    partial class FrmDichVu
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
            this.lblPhieuLuuTru = new System.Windows.Forms.Label();
            this.lblPhong = new System.Windows.Forms.Label();
            this.lblDichVu = new System.Windows.Forms.Label();
            this.lblNgaySuDung = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtPhieuLuuTru = new System.Windows.Forms.TextBox();
            this.txtPhong = new System.Windows.Forms.TextBox();
            this.txtDichVu = new System.Windows.Forms.TextBox();
            this.txtNgaySuDung = new System.Windows.Forms.TextBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.btnGhiNhan = new System.Windows.Forms.Button();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.SuspendLayout();

            // lblPhieuLuuTru
            this.lblPhieuLuuTru.AutoSize = true;
            this.lblPhieuLuuTru.Location = new System.Drawing.Point(30, 30);
            this.lblPhieuLuuTru.Text = "Phiếu lưu trú:";

            // txtPhieuLuuTru
            this.txtPhieuLuuTru.Location = new System.Drawing.Point(125, 27);
            this.txtPhieuLuuTru.Size = new System.Drawing.Size(130, 22);

            // lblPhong
            this.lblPhong.AutoSize = true;
            this.lblPhong.Location = new System.Drawing.Point(280, 30);
            this.lblPhong.Text = "Phòng:";

            // txtPhong
            this.txtPhong.Location = new System.Drawing.Point(340, 27);
            this.txtPhong.Size = new System.Drawing.Size(100, 22);

            // lblDichVu
            this.lblDichVu.AutoSize = true;
            this.lblDichVu.Location = new System.Drawing.Point(465, 30);
            this.lblDichVu.Text = "Dịch vụ:";

            // txtDichVu
            this.txtDichVu.Location = new System.Drawing.Point(530, 27);
            this.txtDichVu.Size = new System.Drawing.Size(160, 22);

            // lblNgaySuDung
            this.lblNgaySuDung.AutoSize = true;
            this.lblNgaySuDung.Location = new System.Drawing.Point(30, 75);
            this.lblNgaySuDung.Text = "Ngày sử dụng:";

            // txtNgaySuDung
            this.txtNgaySuDung.Location = new System.Drawing.Point(125, 72);
            this.txtNgaySuDung.Size = new System.Drawing.Size(130, 22);

            // lblSoLuong
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(280, 75);
            this.lblSoLuong.Text = "Số lượng:";

            // txtSoLuong
            this.txtSoLuong.Location = new System.Drawing.Point(340, 72);
            this.txtSoLuong.Size = new System.Drawing.Size(100, 22);

            // btnGhiNhan
            this.btnGhiNhan.Location = new System.Drawing.Point(530, 67);
            this.btnGhiNhan.Name = "btnGhiNhan";
            this.btnGhiNhan.Size = new System.Drawing.Size(120, 32);
            this.btnGhiNhan.Text = "Ghi nhận";
            this.btnGhiNhan.UseVisualStyleBackColor = true;
            this.btnGhiNhan.Click += new System.EventHandler(this.btnGhiNhan_Click);

            // dgvDichVu
            this.dgvDichVu.AllowUserToAddRows = false;
            this.dgvDichVu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Location = new System.Drawing.Point(33, 125);
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.ReadOnly = true;
            this.dgvDichVu.RowHeadersWidth = 51;
            this.dgvDichVu.Size = new System.Drawing.Size(660, 280);

            // FrmDichVu
            this.ClientSize = new System.Drawing.Size(725, 435);
            this.Controls.Add(this.lblPhieuLuuTru);
            this.Controls.Add(this.txtPhieuLuuTru);
            this.Controls.Add(this.lblPhong);
            this.Controls.Add(this.txtPhong);
            this.Controls.Add(this.lblDichVu);
            this.Controls.Add(this.txtDichVu);
            this.Controls.Add(this.lblNgaySuDung);
            this.Controls.Add(this.txtNgaySuDung);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.btnGhiNhan);
            this.Controls.Add(this.dgvDichVu);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sử dụng dịch vụ";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblPhieuLuuTru;
        private System.Windows.Forms.Label lblPhong;
        private System.Windows.Forms.Label lblDichVu;
        private System.Windows.Forms.Label lblNgaySuDung;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtPhieuLuuTru;
        private System.Windows.Forms.TextBox txtPhong;
        private System.Windows.Forms.TextBox txtDichVu;
        private System.Windows.Forms.TextBox txtNgaySuDung;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Button btnGhiNhan;
        private System.Windows.Forms.DataGridView dgvDichVu;
    }
}