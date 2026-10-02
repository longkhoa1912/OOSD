using eShoppingWinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace e_shoping
{
    public partial class FrmDangNhap : Form
    {
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        // Xử lý sự kiện bấm nút Đăng Nhập
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            // 1. Kiểm tra đầu vào
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Truy vấn tài khoản trong CSDL eSHOPPING_DB
                string sql = "SELECT MaKH, HoTen FROM KHACHHANG WHERE TenDangNhap = @User AND MatKhau = @Pass";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@User", tenDangNhap),
                    new SqlParameter("@Pass", matKhau) // Có thể áp dụng SHA256/MD5 mã hóa ở đây nếu DB dùng mã hóa
                };

                DataTable dt = Database.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count > 0)
                {
                    // 3. Đăng nhập thành công -> Cập nhật thông tin Session vào FrmMain
                    FrmMain.CurrentMaKH = Convert.ToInt32(dt.Rows[0]["MaKH"]);
                    FrmMain.CurrentHoTen = dt.Rows[0]["HoTen"].ToString();

                    MessageBox.Show($"Đăng nhập thành công! Chào mừng {FrmMain.CurrentHoTen}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Chuyển sang Form Đăng ký (UC01) khi nhấp vào liên kết
        private void lblDangKy_Click(object sender, EventArgs e)
        {
            // FrmDangKy frm = new FrmDangKy();
            // frm.ShowDialog();
            MessageBox.Show("Mở Form Đăng Ký Tài Khoản (UC01)", "Thông báo");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}