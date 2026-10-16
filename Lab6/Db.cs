using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyBenhAn
{
    internal class Db
    {
        // Chuỗi kết nối chỉ định đúng Database QuanLyBenhAn
        private static string connectionString = @"Data Source=.;Initial Catalog=QuanLyBenhAn;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static DataTable GetData(string query, params SqlParameter[] ps)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return table;
        }

        public static int ExecuteNonQuery(string query, params SqlParameter[] ps)
        {
            int rows = 0;
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thực thi SQL: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return rows;
        }

        public static object ExecuteScalar(string query, params SqlParameter[] ps)
        {
            object result = null;
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                    result = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thực thi SQL: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return result;
        }

        // ===== Các hàm dùng chung giữa nhiều form =====

        // Lấy thông tin hồ sơ: họ tên, ngày sinh, hạn hồ sơ (ngày hết hạn của lệ phí mới nhất)
        public static bool LayThongTinHoSo(string ma, out string hoTen, out DateTime? ngaySinh, out DateTime? hetHan)
        {
            hoTen = null; ngaySinh = null; hetHan = null;
            DataTable dt = GetData(
                @"SELECT b.HoTen, b.NgaySinh,
                         (SELECT MAX(d.NgayHetHan) FROM DangKy d WHERE d.MaHoSo = b.MaHoSo) AS HetHan
                  FROM BenhNhan b WHERE b.MaHoSo = @ma",
                new SqlParameter("@ma", ma));
            if (dt.Rows.Count == 0) return false;

            DataRow r = dt.Rows[0];
            hoTen = r["HoTen"].ToString();
            if (r["NgaySinh"] != DBNull.Value) ngaySinh = Convert.ToDateTime(r["NgaySinh"]);
            if (r["HetHan"] != DBNull.Value) hetHan = Convert.ToDateTime(r["HetHan"]);
            return true;
        }

        // Lịch sử các lần khám của 1 hồ sơ (lý lịch bệnh án)
        public static DataTable GetLichSuKham(string maHoSo)
        {
            string query = @"SELECT pk.MaPhieuKham AS [Mã phiếu],
                                    CONVERT(VARCHAR(10), pk.NgayGioKham, 103) + ' ' + CONVERT(VARCHAR(5), pk.NgayGioKham, 108) AS [Ngày giờ khám],
                                    pk.LoaiKham AS [Loại khám],
                                    k.TenKhoa AS [Khoa],
                                    bs.HoTen AS [Bác sĩ],
                                    pk.TrieuChung AS [Triệu chứng],
                                    pk.ChanDoan AS [Chẩn đoán],
                                    pk.PhanUngPhu AS [Phản ứng phụ],
                                    pk.VienPhi AS [Viện phí]
                             FROM PhieuKham pk
                             JOIN BacSi bs ON pk.MaBS = bs.MaBS
                             JOIN Khoa k ON pk.MaKhoa = k.MaKhoa
                             WHERE pk.MaHoSo = @ma
                             ORDER BY pk.NgayGioKham DESC";
            return GetData(query, new SqlParameter("@ma", maHoSo));
        }

        public static DataTable GetToaThuoc(int maPhieuKham)
        {
            string query = @"SELECT t.TenThuoc AS [Thuốc], tt.SoLuong AS [Số lượng], t.DonViTinh AS [ĐVT], tt.CachDung AS [Cách dùng]
                             FROM ToaThuoc tt JOIN Thuoc t ON tt.MaThuoc = t.MaThuoc
                             WHERE tt.MaPhieuKham = @id";
            return GetData(query, new SqlParameter("@id", maPhieuKham));
        }

        public static DataTable GetKetQuaXN(int maPhieuKham)
        {
            string query = @"SELECT x.TenXN AS [Xét nghiệm], kq.KetQua AS [Kết quả]
                             FROM KetQuaXN kq JOIN XetNghiem x ON kq.MaXN = x.MaXN
                             WHERE kq.MaPhieuKham = @id";
            return GetData(query, new SqlParameter("@id", maPhieuKham));
        }
    }
}
