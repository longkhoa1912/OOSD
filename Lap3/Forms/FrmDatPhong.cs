using System;
using System.Data;
using System.Windows.Forms;
using Quanlykhachsang;

namespace Quanlykhachsang.Forms
{
    public partial class FrmDatPhong : Form
    {
        private DataTable dtPhongChon;

        public FrmDatPhong()
        {
            InitializeComponent();
            this.Load += FrmDatPhong_Load;
        }

        private void FrmDatPhong_Load(object sender, EventArgs e)
        {
            InitSelectedTable();
            LoadDataPhong();
            LoadDataPhieuDat();
        }

        private void InitSelectedTable()
        {
            dtPhongChon = new DataTable();
            dtPhongChon.Columns.Add("Phòng chọn", typeof(string));
            dtPhongChon.Columns.Add("Số người", typeof(int));
            dtPhongChon.Columns.Add("Đơn giá/ngày", typeof(decimal));

            dgvPhongChon.DataSource = dtPhongChon;
        }

        // Tải danh sách phòng lên dgvPhong
        private void LoadDataPhong()
        {
            string query = @"SELECT p.SoPhong AS [Phòng], 
                                   k.TenKhuVuc AS [Khu], 
                                   p.SoNguoiToiDa AS [Sức chứa], 
                                   p.DonGiaNgay AS [Đơn giá] 
                            FROM Phong p 
                            JOIN KhuVuc k ON p.MaKhuVuc = k.MaKhuVuc";

            DataTable dt = Db.GetData(query);
            if (dt != null)
            {
                dgvPhong.DataSource = dt;
            }
        }

        // Tải danh sách phiếu đặt phòng lên dgvPhieuDat
        private void LoadDataPhieuDat()
        {
            string query = @"SELECT pd.SoPhieuDat AS [Số phiếu], 
                                   kh.HoTen AS [Khách], 
                                   CONVERT(VARCHAR(10), pd.NgayNhan, 103) AS [Ngày nhận], 
                                   CONVERT(VARCHAR(10), pd.NgayTraDuKien, 103) AS [Ngày trả dự kiến], 
                                   pd.TienCoc AS [Cọc], 
                                   pd.KenhDat AS [Kênh], 
                                   pd.TrangThai AS [Trạng thái] 
                            FROM PhieuDatPhong pd 
                            JOIN KhachHang kh ON pd.MaKhach = kh.MaKhach";

            DataTable dt = Db.GetData(query);
            if (dt != null)
            {
                dgvPhieuDat.DataSource = dt;
            }
        }

        // Bấm chọn phòng từ danh sách bên trái đưa sang danh sách chọn bên phải
        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhong.Rows[e.RowIndex];
                string soPhong = row.Cells["Phòng"].Value.ToString();
                int soNguoi = Convert.ToInt32(row.Cells["Sức chứa"].Value);
                decimal donGia = Convert.ToDecimal(row.Cells["Đơn giá"].Value);

                foreach (DataRow dr in dtPhongChon.Rows)
                {
                    if (dr["Phòng chọn"].ToString() == soPhong)
                    {
                        MessageBox.Show("Phòng này đã được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                dtPhongChon.Rows.Add(soPhong, soNguoi, donGia);
            }
        }

        // Xử lý Lập Phiếu Đặt
        private void btnLapPhieuDat_Click(object sender, EventArgs e)
        {
            string soPhieu = txtSoPhieu.Text.Trim();
            string tenKhach = txtKhach.Text.Trim();
            string kenhDat = txtKenhDat.Text.Trim();
            string tienCocStr = txtTienCoc.Text.Trim();

            if (string.IsNullOrEmpty(soPhieu) || string.IsNullOrEmpty(tenKhach) || string.IsNullOrEmpty(kenhDat))
            {
                MessageBox.Show("Vui lòng điền đầy đủ Số phiếu, Tên khách và Kênh đặt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtPhongChon.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal.TryParse(tienCocStr, out decimal tienCoc);

            // 1. Kiểm tra hoặc thêm Khách hàng mới vào bảng KhachHang
            string maKhach = "KH_" + DateTime.Now.ToString("fff");
            string checkKhachSql = $"SELECT MaKhach FROM KhachHang WHERE HoTen = N'{tenKhach}'";
            DataTable dtKhach = Db.GetData(checkKhachSql);

            if (dtKhach != null && dtKhach.Rows.Count > 0)
            {
                maKhach = dtKhach.Rows[0]["MaKhach"].ToString();
            }
            else
            {
                string insertKhachSql = $"INSERT INTO KhachHang(MaKhach, HoTen, SoCMND, QuocTich) VALUES ('{maKhach}', N'{tenKhach}', '{DateTime.Now.Ticks.ToString().Substring(0, 9)}', N'Việt Nam')";
                Db.ExecuteNonQuery(insertKhachSql);
            }

            // 2. Thêm vào bảng PhieuDatPhong
            string maNVLeTan = "NV01"; // Mặc định NV01 từ file SQL
            string insertPhieuSql = $@"INSERT INTO PhieuDatPhong(SoPhieuDat, MaKhach, MaNVLeTan, NgayLap, NgayNhan, NgayTraDuKien, TienCoc, KenhDat, TrangThai) 
                                      VALUES ('{soPhieu}', '{maKhach}', '{maNVLeTan}', GETDATE(), CAST(GETDATE() AS DATE), CAST(DATEADD(day, 1, GETDATE()) AS DATE), {tienCoc}, N'{kenhDat}', N'Đã đặt')";

            int resPhieu = Db.ExecuteNonQuery(insertPhieuSql);

            if (resPhieu > 0)
            {
                // 3. Thêm chi tiết phòng vào ChiTietDatPhong
                foreach (DataRow row in dtPhongChon.Rows)
                {
                    string soPhong = row["Phòng chọn"].ToString();
                    int soNguoi = Convert.ToInt32(row["Số người"]);

                    string insertCTSql = $"INSERT INTO ChiTietDatPhong(SoPhieuDat, SoPhong, SoNguoi) VALUES ('{soPhieu}', '{soPhong}', {soNguoi})";
                    Db.ExecuteNonQuery(insertCTSql);

                    // Cập nhật trạng thái phòng
                    string updatePhongSql = $"UPDATE Phong SET TrangThai = N'Đã đặt' WHERE SoPhong = '{soPhong}'";
                    Db.ExecuteNonQuery(updatePhongSql);
                }

                MessageBox.Show("Lập phiếu đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDataPhong();
                LoadDataPhieuDat();

                txtSoPhieu.Clear();
                txtKhach.Clear();
                txtKenhDat.Clear();
                txtTienCoc.Clear();
                dtPhongChon.Clear();
            }
        }
    }
}