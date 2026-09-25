using System.Drawing;
using System.Windows.Forms;

namespace Quanlykhachsang.Forms
{
    partial class FrmDanhMuc
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
            this.lblMaKhu = new System.Windows.Forms.Label();
            this.lblTenKhu = new System.Windows.Forms.Label();
            this.lblMaDV = new System.Windows.Forms.Label();
            this.lblTenDV = new System.Windows.Forms.Label();
            this.lblDVT = new System.Windows.Forms.Label();
            this.lblDonGia = new System.Windows.Forms.Label();

            this.dgvKhuVuc = new System.Windows.Forms.DataGridView();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.dgvLoaiTN = new System.Windows.Forms.DataGridView();
            this.txtMaKhu = new System.Windows.Forms.TextBox();
            this.txtTenKhu = new System.Windows.Forms.TextBox();
            this.txtMaDV = new System.Windows.Forms.TextBox();
            this.txtTenDV = new System.Windows.Forms.TextBox();
            this.txtDVT = new System.Windows.Forms.TextBox();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.btnThemKhu = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvKhuVuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiTN)).BeginInit();
            this.SuspendLayout();

            // 
            // lblNav
            // 
            this.lblNav.AutoSize = true;
            this.lblNav.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNav.ForeColor = System.Drawing.Color.Navy;
            this.lblNav.Location = new System.Drawing.Point(20, 15);
            this.lblNav.Name = "lblNav";
            this.lblNav.Size = new System.Drawing.Size(530, 16);
            this.lblNav.TabIndex = 0;
            this.lblNav.Text = "[ Khu vực ]  [ Nhân viên ]  [ Loại tiện nghi ]  [ Dịch vụ ]  [ Quy định đền bù ]";

            // 
            // dgvKhuVuc
            // 
            this.dgvKhuVuc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhuVuc.Location = new System.Drawing.Point(20, 50);
            this.dgvKhuVuc.Name = "dgvKhuVuc";
            this.dgvKhuVuc.RowHeadersWidth = 51;
            this.dgvKhuVuc.Size = new System.Drawing.Size(280, 180);
            this.dgvKhuVuc.TabIndex = 1;

            // 
            // dgvDichVu
            // 
            this.dgvDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Location = new System.Drawing.Point(320, 50);
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.RowHeadersWidth = 51;
            this.dgvDichVu.Size = new System.Drawing.Size(280, 180);
            this.dgvDichVu.TabIndex = 2;

            // 
            // dgvLoaiTN
            // 
            this.dgvLoaiTN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoaiTN.Location = new System.Drawing.Point(620, 50);
            this.dgvLoaiTN.Name = "dgvLoaiTN";
            this.dgvLoaiTN.RowHeadersWidth = 51;
            this.dgvLoaiTN.Size = new System.Drawing.Size(280, 180);
            this.dgvLoaiTN.TabIndex = 3;

            // 
            // lblMaKhu
            // 
            this.lblMaKhu.AutoSize = true;
            this.lblMaKhu.Location = new System.Drawing.Point(20, 248);
            this.lblMaKhu.Name = "lblMaKhu";
            this.lblMaKhu.Size = new System.Drawing.Size(30, 16);
            this.lblMaKhu.Text = "Mã:";

            // 
            // txtMaKhu
            // 
            this.txtMaKhu.Location = new System.Drawing.Point(55, 245);
            this.txtMaKhu.Name = "txtMaKhu";
            this.txtMaKhu.Size = new System.Drawing.Size(80, 22);
            this.txtMaKhu.TabIndex = 4;

            // 
            // lblTenKhu
            // 
            this.lblTenKhu.AutoSize = true;
            this.lblTenKhu.Location = new System.Drawing.Point(145, 248);
            this.lblTenKhu.Name = "lblTenKhu";
            this.lblTenKhu.Size = new System.Drawing.Size(34, 16);
            this.lblTenKhu.Text = "Tên:";

            // 
            // txtTenKhu
            // 
            this.txtTenKhu.Location = new System.Drawing.Point(185, 245);
            this.txtTenKhu.Name = "txtTenKhu";
            this.txtTenKhu.Size = new System.Drawing.Size(115, 22);
            this.txtTenKhu.TabIndex = 5;

            // 
            // btnThemKhu
            // 
            this.btnThemKhu.Location = new System.Drawing.Point(20, 280);
            this.btnThemKhu.Name = "btnThemKhu";
            this.btnThemKhu.Size = new System.Drawing.Size(85, 30);
            this.btnThemKhu.TabIndex = 6;
            this.btnThemKhu.Text = "Thêm Khu";
            this.btnThemKhu.UseVisualStyleBackColor = true;
            this.btnThemKhu.Click += new System.EventHandler(this.btnThemKhu_Click);

            // 
            // lblMaDV
            // 
            this.lblMaDV.AutoSize = true;
            this.lblMaDV.Location = new System.Drawing.Point(320, 248);
            this.lblMaDV.Name = "lblMaDV";
            this.lblMaDV.Size = new System.Drawing.Size(30, 16);
            this.lblMaDV.Text = "Mã:";

            // 
            // txtMaDV
            // 
            this.txtMaDV.Location = new System.Drawing.Point(355, 245);
            this.txtMaDV.Name = "txtMaDV";
            this.txtMaDV.Size = new System.Drawing.Size(80, 22);
            this.txtMaDV.TabIndex = 7;

            // 
            // lblTenDV
            // 
            this.lblTenDV.AutoSize = true;
            this.lblTenDV.Location = new System.Drawing.Point(445, 248);
            this.lblTenDV.Name = "lblTenDV";
            this.lblTenDV.Size = new System.Drawing.Size(34, 16);
            this.lblTenDV.Text = "Tên:";

            // 
            // txtTenDV
            // 
            this.txtTenDV.Location = new System.Drawing.Point(485, 245);
            this.txtTenDV.Name = "txtTenDV";
            this.txtTenDV.Size = new System.Drawing.Size(115, 22);
            this.txtTenDV.TabIndex = 8;

            // 
            // lblDVT
            // 
            this.lblDVT.AutoSize = true;
            this.lblDVT.Location = new System.Drawing.Point(320, 280);
            this.lblDVT.Name = "lblDVT";
            this.lblDVT.Size = new System.Drawing.Size(96, 16);
            this.lblDVT.Text = "Đơn vị / Vai trò:";

            // 
            // txtDVT
            // 
            this.txtDVT.Location = new System.Drawing.Point(420, 277);
            this.txtDVT.Name = "txtDVT";
            this.txtDVT.Size = new System.Drawing.Size(180, 22);
            this.txtDVT.TabIndex = 9;

            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new System.Drawing.Point(320, 312);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(95, 16);
            this.lblDonGia.Text = "Đơn giá / Mức:";

            // 
            // txtDonGia
            // 
            this.txtDonGia.Location = new System.Drawing.Point(420, 309);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(180, 22);
            this.txtDonGia.TabIndex = 10;

            // 
            // FrmDanhMuc
            // 
            this.ClientSize = new System.Drawing.Size(930, 370);
            this.Controls.Add(this.lblNav);
            this.Controls.Add(this.lblMaKhu);
            this.Controls.Add(this.lblTenKhu);
            this.Controls.Add(this.lblMaDV);
            this.Controls.Add(this.lblTenDV);
            this.Controls.Add(this.lblDVT);
            this.Controls.Add(this.lblDonGia);
            this.Controls.Add(this.btnThemKhu);
            this.Controls.Add(this.txtDonGia);
            this.Controls.Add(this.txtDVT);
            this.Controls.Add(this.txtTenDV);
            this.Controls.Add(this.txtMaDV);
            this.Controls.Add(this.txtTenKhu);
            this.Controls.Add(this.txtMaKhu);
            this.Controls.Add(this.dgvLoaiTN);
            this.Controls.Add(this.dgvDichVu);
            this.Controls.Add(this.dgvKhuVuc);
            this.Name = "FrmDanhMuc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục khách sạn";
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhuVuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiTN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblNav;
        private System.Windows.Forms.Label lblMaKhu;
        private System.Windows.Forms.Label lblTenKhu;
        private System.Windows.Forms.Label lblMaDV;
        private System.Windows.Forms.Label lblTenDV;
        private System.Windows.Forms.Label lblDVT;
        private System.Windows.Forms.Label lblDonGia;

        private System.Windows.Forms.DataGridView dgvKhuVuc;
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.DataGridView dgvLoaiTN;
        private System.Windows.Forms.TextBox txtMaKhu;
        private System.Windows.Forms.TextBox txtTenKhu;
        private System.Windows.Forms.TextBox txtMaDV;
        private System.Windows.Forms.TextBox txtTenDV;
        private System.Windows.Forms.TextBox txtDVT;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Button btnThemKhu;
    }
}