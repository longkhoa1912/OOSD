using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyNhanSuDongA
{
    /// <summary>Lớp truy cập dữ liệu dùng chung (DAL đơn giản).</summary>
    public static class Db
    {
        private static readonly string ConnStr =
            ConfigurationManager.ConnectionStrings["QLNS"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnStr);
        }

        public static DataTable GetTable(string sql, params SqlParameter[] ps)
        {
            using (var con = GetConnection())
            using (var da = new SqlDataAdapter(sql, con))
            {
                if (ps != null) da.SelectCommand.Parameters.AddRange(ps);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static object Scalar(string sql, params SqlParameter[] ps)
        {
            using (var con = GetConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                con.Open();
                return cmd.ExecuteScalar();
            }
        }

        public static int Execute(string sql, params SqlParameter[] ps)
        {
            using (var con = GetConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
