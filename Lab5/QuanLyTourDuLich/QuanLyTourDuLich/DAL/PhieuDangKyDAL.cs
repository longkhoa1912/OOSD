using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyTourDuLich.Models;

namespace QuanLyTourDuLich.DAL
{
    public class PhieuDangKyDAL
    {
        /// <summary>
        /// Lưu đoàn khách, phiếu đăng ký, danh sách người đi (nếu có) và tiền đặt cọc
        /// trong cùng một transaction. Trả về mã phiếu vừa tạo.
        /// </summary>
        public int LapPhieu(DoanKhach doan, PhieuDangKyDoan phieu, List<NguoiDi> dsNguoiDi, decimal tienCoc)
        {
            using (var conn = Database.TaoKetNoi())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int maDoan;
                        const string sqlDoan =
                            "INSERT INTO DoanKhach(TenCoQuan, DiaChi, DienThoai, NguoiDaiDien) " +
                            "VALUES (@ten, @diachi, @dt, @daidien); " +
                            "SELECT CAST(SCOPE_IDENTITY() AS int);";
                        using (var cmd = new SqlCommand(sqlDoan, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@ten", doan.TenCoQuan);
                            cmd.Parameters.AddWithValue("@diachi", doan.DiaChi);
                            cmd.Parameters.AddWithValue("@dt", doan.DienThoai);
                            cmd.Parameters.AddWithValue("@daidien", doan.NguoiDaiDien);
                            maDoan = (int)cmd.ExecuteScalar();
                        }

                        int maPhieu;
                        const string sqlPhieu =
                            "INSERT INTO PhieuDangKyDoan(MaDoan, MaTour, NgayDi, SoNguoi, DiaDiemDon, CoBaoHiem) " +
                            "VALUES (@madoan, @matour, @ngaydi, @songuoi, @diadiemdon, @baohiem); " +
                            "SELECT CAST(SCOPE_IDENTITY() AS int);";
                        using (var cmd = new SqlCommand(sqlPhieu, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@madoan", maDoan);
                            cmd.Parameters.AddWithValue("@matour", phieu.MaTour);
                            cmd.Parameters.AddWithValue("@ngaydi", phieu.NgayDi);
                            cmd.Parameters.AddWithValue("@songuoi", phieu.SoNguoi);
                            cmd.Parameters.AddWithValue("@diadiemdon", phieu.DiaDiemDon);
                            cmd.Parameters.AddWithValue("@baohiem", phieu.CoBaoHiem);
                            maPhieu = (int)cmd.ExecuteScalar();
                        }

                        if (phieu.CoBaoHiem && dsNguoiDi != null)
                        {
                            const string sqlNguoi =
                                "INSERT INTO DanhSachNguoiDi(MaPhieu, HoTen, NamSinh, GioiTinh) " +
                                "VALUES (@maphieu, @hoten, @namsinh, @gioitinh);";
                            foreach (var nguoi in dsNguoiDi)
                            {
                                using (var cmd = new SqlCommand(sqlNguoi, conn, tran))
                                {
                                    cmd.Parameters.AddWithValue("@maphieu", maPhieu);
                                    cmd.Parameters.AddWithValue("@hoten", nguoi.HoTen);
                                    cmd.Parameters.AddWithValue("@namsinh", nguoi.NamSinh);
                                    cmd.Parameters.AddWithValue("@gioitinh", nguoi.GioiTinh);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        const string sqlCoc = "INSERT INTO DatCoc(MaPhieu, SoTien) VALUES (@maphieu, @sotien);";
                        using (var cmd = new SqlCommand(sqlCoc, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@maphieu", maPhieu);
                            cmd.Parameters.AddWithValue("@sotien", tienCoc);
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                        return maPhieu;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
