using System;
using System.Data;
using System.Windows.Forms;
using Quanlykhachsang;

namespace Quanlykhachsang.Forms
{
    public partial class FrmDanhMuc : Form
    {
        public FrmDanhMuc()
        {
            InitializeComponent();
            // Đăng ký sự kiện Load để tự động lấy dữ liệu khi mở Form
            this.Load += FrmDanhMuc_Load;
        }

        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        // 1. Tải tất cả các bảng dữ liệu lên DataGridView
        private void LoadAllData()
        {
            LoadDataKhuVuc();
            LoadDataDichVu();
            LoadDataLoaiTN();
        }

        private void LoadDataKhuVuc()
        {
            string query = "SELECT MaKhuVuc AS [Mã Khu], TenKhuVuc AS [Tên Khu Vực] FROM KhuVuc";
            DataTable dt = Db.GetData(query);
            if (dt != null)
            {
                dgvKhuVuc.DataSource = dt;
            }
        }

        private void LoadDataDichVu()
        {
            string query = "SELECT MaDV AS [Mã DV], TenDV AS [Tên Dịch Vụ], DonViTinh AS [ĐVT], DonGia AS [Đơn Giá] FROM DichVu";
            DataTable dt = Db.GetData(query);
            if (dt != null)
            {
                dgvDichVu.DataSource = dt;
            }
        }

        private void LoadDataLoaiTN()
        {
            string query = "SELECT MaLoaiTN AS [Mã Loại], TenLoaiTN AS [Tên Loại Tiện Nghi] FROM LoaiTienNghi";
            DataTable dt = Db.GetData(query);
            if (dt != null)
            {
                dgvLoaiTN.DataSource = dt;
            }
        }

        // 2. Xử lý nút Thêm Khu
        private void btnThemKhu_Click(object sender, EventArgs e)
        {
            string maKhu = txtMaKhu.Text.Trim();
            string tenKhu = txtTenKhu.Text.Trim();

            if (string.IsNullOrEmpty(maKhu) || string.IsNullOrEmpty(tenKhu))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã khu và Tên khu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"INSERT INTO KhuVuc(MaKhuVuc, TenKhuVuc) VALUES ('{maKhu}', N'{tenKhu}')";

            int result = Db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Thêm khu vực thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataKhuVuc();
                txtMaKhu.Clear();
                txtTenKhu.Clear();
                txtMaKhu.Focus();
            }
        }
    }
}