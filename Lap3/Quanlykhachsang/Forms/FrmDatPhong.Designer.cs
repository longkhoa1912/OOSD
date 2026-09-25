namespace Quanlykhachsang.Forms
{
    partial class FrmDatPhong
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
            this.lblSoPhieu = new System.Windows.Forms.Label();
            this.lblKhach = new System.Windows.Forms.Label();
            this.lblKenhDat = new System.Windows.Forms.Label();
            this.lblTienCoc = new System.Windows.Forms.Label();
            this.txtSoPhieu = new System.Windows.Forms.TextBox();
            this.txtKhach = new System.Windows.Forms.TextBox();
            this.txtKenhDat = new System.Windows.Forms.TextBox();
            this.txtTienCoc = new System.Windows.Forms.TextBox();
            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.dgvPhongChon = new System.Windows.Forms.DataGridView();
            this.btnLapPhieuDat = new System.Windows.Forms.Button();
            this.lblTitlePhieuDat = new System.Windows.Forms.Label();
            this.dgvPhieuDat = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongChon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuDat)).BeginInit();
            this.SuspendLayout();

            // lblNav
            this.lblNav.AutoSize = true;
            this.lblNav.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblNav.ForeColor = System.Drawing.Color.Navy;
            this.lblNav.Location = new System.Drawing.Point(25, 15);
            this.lblNav.Text = "[Khách hàng]  [Đặt phòng]  [Nhận phòng / Người lưu trú]";

            // Hàng nhập liệu thông tin đặt phòng
            this.lblSoPhieu.Text = "Số phiếu đặt:";
            this.lblSoPhieu.Location = new System.Drawing.Point(25, 52);
            this.txtSoPhieu.Location = new System.Drawing.Point(115, 49);
            this.txtSoPhieu.Size = new System.Drawing.Size(120, 22);

            this.lblKhach.Text = "Khách:";
            this.lblKhach.Location = new System.Drawing.Point(260, 52);
            this.txtKhach.Location = new System.Drawing.Point(315, 49);
            this.txtKhach.Size = new System.Drawing.Size(160, 22);

            this.lblKenhDat.Text = "Kênh đặt:";
            this.lblKenhDat.Location = new System.Drawing.Point(500, 52);
            this.txtKenhDat.Location = new System.Drawing.Point(570, 49);
            this.txtKenhDat.Size = new System.Drawing.Size(120, 22);

            this.lblTienCoc.Text = "Tiền cọc:";
            this.lblTienCoc.Location = new System.Drawing.Point(710, 52);
            this.txtTienCoc.Location = new System.Drawing.Point(775, 49);
            this.txtTienCoc.Size = new System.Drawing.Size(100, 22);

            // dgvPhong (Danh sách phòng sẵn có)
            this.dgvPhong.AllowUserToAddRows = false;
            this.dgvPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhong.Location = new System.Drawing.Point(28, 90);
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.ReadOnly = true;
            this.dgvPhong.RowHeadersWidth = 51;
            this.dgvPhong.Size = new System.Drawing.Size(420, 190);
            this.dgvPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhong_CellClick);

            // dgvPhongChon (Danh sách phòng được chọn)
            this.dgvPhongChon.AllowUserToAddRows = false;
            this.dgvPhongChon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhongChon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhongChon.Location = new System.Drawing.Point(465, 90);
            this.dgvPhongChon.Name = "dgvPhongChon";
            this.dgvPhongChon.RowHeadersWidth = 51;
            this.dgvPhongChon.Size = new System.Drawing.Size(410, 190);

            // Button Lập phiếu đặt
            this.btnLapPhieuDat.Location = new System.Drawing.Point(725, 292);
            this.btnLapPhieuDat.Name = "btnLapPhieuDat";
            this.btnLapPhieuDat.Size = new System.Drawing.Size(150, 32);
            this.btnLapPhieuDat.Text = "Lập phiếu đặt";
            this.btnLapPhieuDat.UseVisualStyleBackColor = true;
            this.btnLapPhieuDat.Click += new System.EventHandler(this.btnLapPhieuDat_Click);

            // Title danh sách phiếu đặt
            this.lblTitlePhieuDat.AutoSize = true;
            this.lblTitlePhieuDat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular);
            this.lblTitlePhieuDat.Location = new System.Drawing.Point(25, 335);
            this.lblTitlePhieuDat.Text = "Phiếu đặt phòng:";

            // dgvPhieuDat (Danh sách các phiếu đặt phòng)
            this.dgvPhieuDat.AllowUserToAddRows = false;
            this.dgvPhieuDat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhieuDat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhieuDat.Location = new System.Drawing.Point(28, 360);
            this.dgvPhieuDat.Name = "dgvPhieuDat";
            this.dgvPhieuDat.ReadOnly = true;
            this.dgvPhieuDat.RowHeadersWidth = 51;
            this.dgvPhieuDat.Size = new System.Drawing.Size(847, 180);

            // FrmDatPhong
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.Controls.Add(this.lblNav);
            this.Controls.Add(this.lblSoPhieu);
            this.Controls.Add(this.txtSoPhieu);
            this.Controls.Add(this.lblKhach);
            this.Controls.Add(this.txtKhach);
            this.Controls.Add(this.lblKenhDat);
            this.Controls.Add(this.txtKenhDat);
            this.Controls.Add(this.lblTienCoc);
            this.Controls.Add(this.txtTienCoc);
            this.Controls.Add(this.dgvPhong);
            this.Controls.Add(this.dgvPhongChon);
            this.Controls.Add(this.btnLapPhieuDat);
            this.Controls.Add(this.lblTitlePhieuDat);
            this.Controls.Add(this.dgvPhieuDat);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Khách hàng - Đặt phòng - Nhận phòng";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongChon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuDat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblNav;
        private System.Windows.Forms.Label lblSoPhieu;
        private System.Windows.Forms.Label lblKhach;
        private System.Windows.Forms.Label lblKenhDat;
        private System.Windows.Forms.Label lblTienCoc;
        private System.Windows.Forms.TextBox txtSoPhieu;
        private System.Windows.Forms.TextBox txtKhach;
        private System.Windows.Forms.TextBox txtKenhDat;
        private System.Windows.Forms.TextBox txtTienCoc;
        private System.Windows.Forms.DataGridView dgvPhong;
        private System.Windows.Forms.DataGridView dgvPhongChon;
        private System.Windows.Forms.Button btnLapPhieuDat;
        private System.Windows.Forms.Label lblTitlePhieuDat;
        private System.Windows.Forms.DataGridView dgvPhieuDat;
    }
}