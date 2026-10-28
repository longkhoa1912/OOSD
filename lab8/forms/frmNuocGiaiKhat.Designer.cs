namespace QuanLyNGK.UI
{
    partial class frmNuocGiaiKhat
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
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblMaNGK = new System.Windows.Forms.Label();
            this.txtMaNGK = new System.Windows.Forms.TextBox();
            this.lblTenNGK = new System.Windows.Forms.Label();
            this.txtTenNGK = new System.Windows.Forms.TextBox();
            this.lblDonViTinh = new System.Windows.Forms.Label();
            this.cboDonViTinh = new System.Windows.Forms.ComboBox();
            this.lblSoLuongTon = new System.Windows.Forms.Label();
            this.nudSoLuongTon = new System.Windows.Forms.NumericUpDown();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.nudDonGia = new System.Windows.Forms.NumericUpDown();
            this.lblNhanHieu = new System.Windows.Forms.Label();
            this.cboNhanHieu = new System.Windows.Forms.ComboBox();
            this.lblNhaCungUng = new System.Windows.Forms.Label();
            this.cboNhaCungUng = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dgvNGK = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTrangThai = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongTon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNGK)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // grpThongTin
            //
            this.grpThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTin.Controls.Add(this.lblMaNGK);
            this.grpThongTin.Controls.Add(this.txtMaNGK);
            this.grpThongTin.Controls.Add(this.lblTenNGK);
            this.grpThongTin.Controls.Add(this.txtTenNGK);
            this.grpThongTin.Controls.Add(this.lblDonViTinh);
            this.grpThongTin.Controls.Add(this.cboDonViTinh);
            this.grpThongTin.Controls.Add(this.lblSoLuongTon);
            this.grpThongTin.Controls.Add(this.nudSoLuongTon);
            this.grpThongTin.Controls.Add(this.lblDonGia);
            this.grpThongTin.Controls.Add(this.nudDonGia);
            this.grpThongTin.Controls.Add(this.lblNhanHieu);
            this.grpThongTin.Controls.Add(this.cboNhanHieu);
            this.grpThongTin.Controls.Add(this.lblNhaCungUng);
            this.grpThongTin.Controls.Add(this.cboNhaCungUng);
            this.grpThongTin.Location = new System.Drawing.Point(12, 12);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(876, 170);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin nước giải khát";
            //
            // lblMaNGK
            //
            this.lblMaNGK.AutoSize = true;
            this.lblMaNGK.Location = new System.Drawing.Point(20, 30);
            this.lblMaNGK.Name = "lblMaNGK";
            this.lblMaNGK.Text = "Mã NGK:";
            //
            // txtMaNGK
            //
            this.txtMaNGK.Location = new System.Drawing.Point(130, 27);
            this.txtMaNGK.MaxLength = 10;
            this.txtMaNGK.Name = "txtMaNGK";
            this.txtMaNGK.Size = new System.Drawing.Size(220, 23);
            this.txtMaNGK.TabIndex = 0;
            //
            // lblTenNGK
            //
            this.lblTenNGK.AutoSize = true;
            this.lblTenNGK.Location = new System.Drawing.Point(20, 62);
            this.lblTenNGK.Name = "lblTenNGK";
            this.lblTenNGK.Text = "Tên NGK:";
            //
            // txtTenNGK
            //
            this.txtTenNGK.Location = new System.Drawing.Point(130, 59);
            this.txtTenNGK.MaxLength = 100;
            this.txtTenNGK.Name = "txtTenNGK";
            this.txtTenNGK.Size = new System.Drawing.Size(220, 23);
            this.txtTenNGK.TabIndex = 1;
            //
            // lblDonViTinh
            //
            this.lblDonViTinh.AutoSize = true;
            this.lblDonViTinh.Location = new System.Drawing.Point(20, 94);
            this.lblDonViTinh.Name = "lblDonViTinh";
            this.lblDonViTinh.Text = "Đơn vị tính:";
            //
            // cboDonViTinh
            //
            this.cboDonViTinh.Location = new System.Drawing.Point(130, 91);
            this.cboDonViTinh.Name = "cboDonViTinh";
            this.cboDonViTinh.Size = new System.Drawing.Size(220, 23);
            this.cboDonViTinh.TabIndex = 2;
            //
            // lblSoLuongTon
            //
            this.lblSoLuongTon.AutoSize = true;
            this.lblSoLuongTon.Location = new System.Drawing.Point(20, 126);
            this.lblSoLuongTon.Name = "lblSoLuongTon";
            this.lblSoLuongTon.Text = "Số lượng tồn:";
            //
            // nudSoLuongTon
            //
            this.nudSoLuongTon.Location = new System.Drawing.Point(130, 123);
            this.nudSoLuongTon.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudSoLuongTon.Name = "nudSoLuongTon";
            this.nudSoLuongTon.Size = new System.Drawing.Size(220, 23);
            this.nudSoLuongTon.TabIndex = 3;
            this.nudSoLuongTon.ThousandsSeparator = true;
            //
            // lblDonGia
            //
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new System.Drawing.Point(460, 30);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Text = "Đơn giá bán:";
            //
            // nudDonGia
            //
            this.nudDonGia.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            this.nudDonGia.Location = new System.Drawing.Point(570, 27);
            this.nudDonGia.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.nudDonGia.Name = "nudDonGia";
            this.nudDonGia.Size = new System.Drawing.Size(270, 23);
            this.nudDonGia.TabIndex = 4;
            this.nudDonGia.ThousandsSeparator = true;
            //
            // lblNhanHieu
            //
            this.lblNhanHieu.AutoSize = true;
            this.lblNhanHieu.Location = new System.Drawing.Point(460, 62);
            this.lblNhanHieu.Name = "lblNhanHieu";
            this.lblNhanHieu.Text = "Nhãn hiệu:";
            //
            // cboNhanHieu
            //
            this.cboNhanHieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanHieu.Location = new System.Drawing.Point(570, 59);
            this.cboNhanHieu.Name = "cboNhanHieu";
            this.cboNhanHieu.Size = new System.Drawing.Size(270, 23);
            this.cboNhanHieu.TabIndex = 5;
            //
            // lblNhaCungUng
            //
            this.lblNhaCungUng.AutoSize = true;
            this.lblNhaCungUng.Location = new System.Drawing.Point(460, 94);
            this.lblNhaCungUng.Name = "lblNhaCungUng";
            this.lblNhaCungUng.Text = "Nhà cung ứng:";
            //
            // cboNhaCungUng
            //
            this.cboNhaCungUng.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhaCungUng.Location = new System.Drawing.Point(570, 91);
            this.cboNhaCungUng.Name = "cboNhaCungUng";
            this.cboNhaCungUng.Size = new System.Drawing.Size(270, 23);
            this.cboNhaCungUng.TabIndex = 6;
            //
            // btnThem
            //
            this.btnThem.Location = new System.Drawing.Point(12, 192);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 32);
            this.btnThem.TabIndex = 1;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            //
            // btnSua
            //
            this.btnSua.Location = new System.Drawing.Point(122, 192);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 32);
            this.btnSua.TabIndex = 2;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            //
            // btnXoa
            //
            this.btnXoa.Location = new System.Drawing.Point(232, 192);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 32);
            this.btnXoa.TabIndex = 3;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            //
            // btnLamMoi
            //
            this.btnLamMoi.Location = new System.Drawing.Point(342, 192);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 32);
            this.btnLamMoi.TabIndex = 4;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            //
            // lblTimKiem
            //
            this.lblTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(560, 200);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Text = "Tìm kiếm:";
            //
            // txtTimKiem
            //
            this.txtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiem.Location = new System.Drawing.Point(630, 197);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(170, 23);
            this.txtTimKiem.TabIndex = 5;
            this.txtTimKiem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimKiem_KeyDown);
            //
            // btnTimKiem
            //
            this.btnTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimKiem.Location = new System.Drawing.Point(808, 195);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(80, 28);
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            //
            // dgvNGK
            //
            this.dgvNGK.AllowUserToAddRows = false;
            this.dgvNGK.AllowUserToDeleteRows = false;
            this.dgvNGK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNGK.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNGK.BackgroundColor = System.Drawing.Color.White;
            this.dgvNGK.Location = new System.Drawing.Point(12, 235);
            this.dgvNGK.MultiSelect = false;
            this.dgvNGK.Name = "dgvNGK";
            this.dgvNGK.ReadOnly = true;
            this.dgvNGK.RowHeadersVisible = false;
            this.dgvNGK.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNGK.Size = new System.Drawing.Size(876, 325);
            this.dgvNGK.TabIndex = 7;
            this.dgvNGK.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNGK_CellClick);
            //
            // statusStrip1
            //
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblTrangThai });
            this.statusStrip1.Location = new System.Drawing.Point(0, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(900, 22);
            this.statusStrip1.TabIndex = 8;
            //
            // lblTrangThai
            //
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(39, 17);
            this.lblTrangThai.Text = "Sẵn sàng";
            //
            // frmNuocGiaiKhat
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvNGK);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.lblTimKiem);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.grpThongTin);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(916, 639);
            this.Name = "frmNuocGiaiKhat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Nước giải khát";
            this.Load += new System.EventHandler(this.frmNuocGiaiKhat_Load);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongTon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNGK)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblMaNGK;
        private System.Windows.Forms.TextBox txtMaNGK;
        private System.Windows.Forms.Label lblTenNGK;
        private System.Windows.Forms.TextBox txtTenNGK;
        private System.Windows.Forms.Label lblDonViTinh;
        private System.Windows.Forms.ComboBox cboDonViTinh;
        private System.Windows.Forms.Label lblSoLuongTon;
        private System.Windows.Forms.NumericUpDown nudSoLuongTon;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.NumericUpDown nudDonGia;
        private System.Windows.Forms.Label lblNhanHieu;
        private System.Windows.Forms.ComboBox cboNhanHieu;
        private System.Windows.Forms.Label lblNhaCungUng;
        private System.Windows.Forms.ComboBox cboNhaCungUng;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvNGK;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTrangThai;
    }
}
