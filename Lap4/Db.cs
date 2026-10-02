using System;
using System.Data;
using System.Data.SqlClient; // Nếu dùng .NET Core / .NET 6+ thì đổi thành Microsoft.Data.SqlClient
using System.Configuration;

namespace eShoppingWinForms
{
    public class Database
    {
        // Lấy chuỗi kết nối từ file App.config
        private static readonly string strConn = ConfigurationManager.ConnectionStrings["eShoppingConn"].ConnectionString;

        /// <summary>
        /// Khởi tạo và trả về một đối tượng SqlConnection
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(strConn);
        }

        /// <summary>
        /// Thực thi câu lệnh SELECT trả về bảng dữ liệu (DataTable)
        /// Thích hợp dùng để hiển thị dữ liệu lên DataGridView, ComboBox, ListView...
        /// </summary>
        public static DataTable ExecuteQuery(string sql, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn dữ liệu: " + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// Thực thi câu lệnh INSERT, UPDATE, DELETE
        /// Trả về số dòng (records) bị ảnh hưởng trong CSDL
        /// </summary>
        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            int affectedRows = 0;
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        affectedRows = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật CSDL: " + ex.Message);
            }
            return affectedRows;
        }

        /// <summary>
        /// Thực thi câu lệnh trả về 1 giá trị đơn lẻ (COUNT, SUM, MAX, hoặc kiểm tra tồn tại)
        /// </summary>
        public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            object result = null;
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        result = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn giá trị đơn: " + ex.Message);
            }
            return result;
        }
    }
}