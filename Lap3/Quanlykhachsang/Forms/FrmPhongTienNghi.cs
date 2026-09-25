using System;
using System.Data;
using System.Windows.Forms;
using Quanlykhachsang;

namespace Quanlykhachsang.Forms
{
    public partial class FrmPhongTienNghi : Form
    {
        public FrmPhongTienNghi()
        {
            InitializeComponent();
            this.Load += FrmPhongTienNghi_Load;
        }

        private void FrmPhongTienNghi_Load(object sender, EventArgs e)
        {
            LoadDataPhong();
        }

        // 1. Lấy danh sách phòng từ CSDL hiển thị lên DataGridView
        private void LoadDataPhong()
        {
            string query = @"SELECT p.SoPhong AS [Phòng], 
                                   k.TenKhuVuc AS [Khu], 
                                   p.SoNguoiToiDa AS [Sức chứa], 
                                   p.DonGiaNgay AS [Đơn giá], 
                                   p.TrangThai AS [Trạng thái] 
                            FROM Phong p 
                            JOIN KhuVuc k ON p.MaKhuVuc = k.MaKhuVuc";

            DataTable dt = Db.GetData(query);
            if (dt != null)
            {
                dgvPhong.DataSource = dt;
            }
        }

        // 2. Click chọn 1 dòng trên DataGridView thì đổ dữ liệu lên các ô TextBox tương ứng
        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhong.Rows[e.RowIndex];

                string soPhong = row.Cells["Phòng"].Value.ToString();
                txtSoPhong.Text = soPhong;
                txtKhuVuc.Text = row.Cells["Khu"].Value.ToString();
                txtSoNguoiToiDa.Text = row.Cells["Sức chứa"].Value.ToString();
                txtDonGiaNgay.Text = row.Cells["Đơn giá"].Value.ToString();

                // Tự động gán mã phòng xuống ô Nhập Phiếu Lắp Đặt
                txtSoPhongLapDat.Text = soPhong;
            }
        }

        // 3. Xử lý Lập phiếu lắp đặt thiết bị / tiện nghi vào phòng
        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            string soPhieu = txtSoPhieuLapDat.Text.Trim();
            string maTienNghi = txtMaTienNghi.Text.Trim();
            string soPhong = txtSoPhongLapDat.Text.Trim();
            string tinhTrang = txtTinhTrang.Text.Trim();

            // Kiểm tra đầu vào
            if (string.IsNullOrEmpty(soPhieu) || string.IsNullOrEmpty(maTienNghi) ||
                string.IsNullOrEmpty(soPhong) || string.IsNullOrEmpty(tinhTrang))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin Lập phiếu lắp đặt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mặc định lấy NV02 (Phục vụ phòng) lập phiếu theo bảng NhanVien
            string maNV = "NV02";

            string query = $@"INSERT INTO PhieuLapDat(SoPhieuLapDat, MaTienNghi, SoPhong, NgayLap, TinhTrang, MaNV) 
                            VALUES ('{soPhieu}', '{maTienNghi}', '{soPhong}', GETDATE(), N'{tinhTrang}', '{maNV}')";

            int result = Db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Lập phiếu lắp đặt thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa trắng ô nhập phiếu lắp đặt sau khi lập thành công
                txtSoPhieuLapDat.Clear();
                txtMaTienNghi.Clear();
                txtTinhTrang.Clear();
            }
        }
    }
}