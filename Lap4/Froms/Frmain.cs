using eShoppingWinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace e_shoping
{
    public partial class FrmMain : Form
    {
        // Lưu trữ phiên đăng nhập của Khách hàng
        public static int CurrentMaKH = -1;
        public static string CurrentHoTen = "";

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            CapNhatTrangThaiDangNhap();
            TaiDanhSachNhomSP();
            TaiDanhSachSanPham();
        }

        /// <summary>
        /// Cập nhật thông tin giao diện theo trạng thái đăng nhập
        /// </summary>
        public void CapNhatTrangThaiDangNhap()
        {
            if (CurrentMaKH > 0)
            {
                lblXinChao.Text = $"Xin chào, {CurrentHoTen}";
                btnDangNhap.Text = "Đăng xuất";
                CapNhatSoLuongGioHang();
            }
            else
            {
                lblXinChao.Text = "Chưa đăng nhập";
                btnDangNhap.Text = "Đăng nhập";
                btnGioHang.Text = "Giỏ hàng (0)";
            }
        }

        /// <summary>
        /// Nạp danh sách Nhóm sản phẩm vào ComboBox lọc
        /// </summary>
        private void TaiDanhSachNhomSP()
        {
            try
            {
                string sql = "SELECT MaNhom, TenNhom FROM NHOMSANPHAM";
                DataTable dt = Database.ExecuteQuery(sql);

                // Thêm dòng chọn tất cả
                DataRow dr = dt.NewRow();
                dr["MaNhom"] = "ALL";
                dr["TenNhom"] = "-- Tất cả danh mục --";
                dt.Rows.InsertAt(dr, 0);

                cboNhomSP.DataSource = dt;
                cboNhomSP.DisplayMember = "TenNhom";
                cboNhomSP.ValueMember = "MaNhom";
                cboNhomSP.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nạp danh sách sản phẩm lên DataGridView theo điều kiện lọc
        /// </summary>
        private void TaiDanhSachSanPham(string tuKhoa = "", string maNhom = "ALL")
        {
            try
            {
                string sql = @"SELECT sp.MaSP, sp.TenSP, nsp.TenNhom, sp.NhaSanXuat, sp.GiaBan, sp.SoLuongTon, sp.TrangThai 
                               FROM SANPHAM sp
                               JOIN NHOMSANPHAM nsp ON sp.MaNhom = nsp.MaNhom
                               WHERE 1=1";

                var parameters = new System.Collections.Generic.List<SqlParameter>();

                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    sql += " AND (sp.TenSP LIKE @TuKhoa OR sp.NhaSanXuat LIKE @TuKhoa)";
                    parameters.Add(new SqlParameter("@TuKhoa", "%" + tuKhoa.Trim() + "%"));
                }

                if (maNhom != "ALL" && !string.IsNullOrEmpty(maNhom))
                {
                    sql += " AND sp.MaNhom = @MaNhom";
                    parameters.Add(new SqlParameter("@MaNhom", maNhom));
                }

                DataTable dt = Database.ExecuteQuery(sql, parameters.ToArray());
                dgvSanPham.DataSource = dt;

                // Định dạng hiển thị DataGridView
                DinhDangGridSanPham();

                lblStatus.Text = $"Đang hiển thị {dt.Rows.Count} sản phẩm.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Định dạng tiêu đề và độ rộng cột trên DataGridView
        /// </summary>
        private void DinhDangGridSanPham()
        {
            if (dgvSanPham.Columns["MaSP"] != null) dgvSanPham.Columns["MaSP"].HeaderText = "Mã SP";
            if (dgvSanPham.Columns["TenSP"] != null) dgvSanPham.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            if (dgvSanPham.Columns["TenNhom"] != null) dgvSanPham.Columns["TenNhom"].HeaderText = "Danh mục";
            if (dgvSanPham.Columns["NhaSanXuat"] != null) dgvSanPham.Columns["NhaSanXuat"].HeaderText = "Nhà sản xuất";
            if (dgvSanPham.Columns["GiaBan"] != null)
            {
                dgvSanPham.Columns["GiaBan"].HeaderText = "Giá bán (VNĐ)";
                dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgvSanPham.Columns["SoLuongTon"] != null) dgvSanPham.Columns["SoLuongTon"].HeaderText = "Tồn kho";
            if (dgvSanPham.Columns["TrangThai"] != null) dgvSanPham.Columns["TrangThai"].HeaderText = "Trạng thái";

            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Đếm số lượng món đồ trong giỏ hàng của tài khoản hiện tại
        /// </summary>
        public void CapNhatSoLuongGioHang()
        {
            if (CurrentMaKH <= 0) return;

            string sql = @"SELECT ISNULL(SUM(ct.SoLuong), 0) 
                           FROM CHITIETGIOHANG ct
                           JOIN GIOHANG gh ON ct.MaGioHang = gh.MaGioHang
                           WHERE gh.MaKH = @MaKH";

            SqlParameter[] pars = { new SqlParameter("@MaKH", CurrentMaKH) };
            object count = Database.ExecuteScalar(sql, pars);
            btnGioHang.Text = $"Giỏ hàng ({count})";
        }

        // Sự kiện Bấm tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text;
            string maNhom = cboNhomSP.SelectedValue != null ? cboNhomSP.SelectedValue.ToString() : "ALL";
            TaiDanhSachSanPham(tuKhoa, maNhom);
        }

        // Sự kiện đổi danh mục lọc
        private void cboNhomSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNhomSP.SelectedValue != null)
            {
                string tuKhoa = txtTimKiem.Text;
                string maNhom = cboNhomSP.SelectedValue.ToString();
                TaiDanhSachSanPham(tuKhoa, maNhom);
            }
        }

        // Sự kiện Đăng nhập / Đăng xuất
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (CurrentMaKH > 0)
            {
                // Thực hiện Đăng xuất
                CurrentMaKH = -1;
                CurrentHoTen = "";
                CapNhatTrangThaiDangNhap();
                MessageBox.Show("Đã đăng xuất tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Mở Form Đăng nhập
                FrmDangNhap frm = new FrmDangNhap();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CapNhatTrangThaiDangNhap(); // Cập nhật tên người dùng & giỏ hàng lên màn hình chính
                }
            }
        }

        // Sự kiện Thêm sản phẩm được chọn vào Giỏ hàng
        private void btnThemVaoGio_Click(object sender, EventArgs e)
        {
            if (CurrentMaKH <= 0)
            {
                MessageBox.Show("Vui lòng đăng nhập để thực hiện thêm sản phẩm vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvSanPham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maSP = dgvSanPham.CurrentRow.Cells["MaSP"].Value.ToString();
            int soLuongTon = Convert.ToInt32(dgvSanPham.CurrentRow.Cells["SoLuongTon"].Value);

            if (soLuongTon <= 0)
            {
                MessageBox.Show("Sản phẩm này hiện đã hết hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Lấy hoặc Tạo mới Giỏ hàng cho Khách hàng
                string sqlGetCart = "SELECT MaGioHang FROM GIOHANG WHERE MaKH = @MaKH";
                SqlParameter[] p1 = { new SqlParameter("@MaKH", CurrentMaKH) };
                object cartObj = Database.ExecuteScalar(sqlGetCart, p1);

                int maGioHang = 0;
                if (cartObj == null)
                {
                    string sqlCreateCart = "INSERT INTO GIOHANG (MaKH) OUTPUT INSERTED.MaGioHang VALUES (@MaKH)";
                    maGioHang = Convert.ToInt32(Database.ExecuteScalar(sqlCreateCart, p1));
                }
                else
                {
                    maGioHang = Convert.ToInt32(cartObj);
                }

                // 2. Thêm hoặc cập nhật số lượng trong CHITIETGIOHANG
                string sqlCheckItem = "SELECT SoLuong FROM CHITIETGIOHANG WHERE MaGioHang = @MaGioHang AND MaSP = @MaSP";
                SqlParameter[] p2 = {
                    new SqlParameter("@MaGioHang", maGioHang),
                    new SqlParameter("@MaSP", maSP)
                };

                object itemExist = Database.ExecuteScalar(sqlCheckItem, p2);

                if (itemExist == null)
                {
                    string sqlAddItem = "INSERT INTO CHITIETGIOHANG (MaGioHang, MaSP, SoLuong) VALUES (@MaGioHang, @MaSP, 1)";
                    Database.ExecuteNonQuery(sqlAddItem, p2);
                }
                else
                {
                    string sqlUpdateItem = "UPDATE CHITIETGIOHANG SET SoLuong = SoLuong + 1 WHERE MaGioHang = @MaGioHang AND MaSP = @MaSP";
                    Database.ExecuteNonQuery(sqlUpdateItem, p2);
                }

                CapNhatSoLuongGioHang();
                MessageBox.Show("Đã thêm sản phẩm vào giỏ hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm vào giỏ hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện Xem Giỏ hàng
        private void btnGioHang_Click(object sender, EventArgs e)
        {
            if (CurrentMaKH <= 0)
            {
                MessageBox.Show("Vui lòng đăng nhập để xem giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở Form Giỏ hàng
            // FrmGioHang frm = new FrmGioHang();
            // frm.ShowDialog();
            // CapNhatSoLuongGioHang();
            MessageBox.Show("Chuyển tới Form Giỏ hàng & Đặt hàng", "Thông báo");
        }
    }
}