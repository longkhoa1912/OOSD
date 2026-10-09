using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyTourDuLich.Models;

namespace QuanLyTourDuLich.DAL
{
    public class TourDAL
    {
        public List<Tour> LayDanhSach()
        {
            var ds = new List<Tour>();
            const string sql = "SELECT MaTour, TenTour, SoNgay, SoDem, DonGia FROM Tour ORDER BY MaTour";

            using (var conn = Database.TaoKetNoi())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        ds.Add(new Tour
                        {
                            MaTour = rd.GetString(0),
                            TenTour = rd.GetString(1),
                            SoNgay = rd.GetInt32(2),
                            SoDem = rd.GetInt32(3),
                            DonGia = rd.GetDecimal(4)
                        });
                    }
                }
            }
            return ds;
        }
    }
}
