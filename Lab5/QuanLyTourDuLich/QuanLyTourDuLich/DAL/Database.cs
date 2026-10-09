using System.Configuration;
using System.Data.SqlClient;

namespace QuanLyTourDuLich.DAL
{
    public static class Database
    {
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["QLTourDB"].ConnectionString; }
        }

        public static SqlConnection TaoKetNoi()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
