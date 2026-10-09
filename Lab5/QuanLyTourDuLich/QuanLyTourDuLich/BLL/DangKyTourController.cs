using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using QuanLyTourDuLich.DAL;
using QuanLyTourDuLich.Models;

namespace QuanLyTourDuLich.BLL
{
    /// <summary>Lỗi vi phạm quy tắc nghiệp vụ, thông báo có thể hiển thị trực tiếp cho người dùng.</summary>
    public class NghiepVuException : Exception
    {
        public NghiepVuException(string message) : base(message) { }
    }

    /// <summary>Bộ điều khiển "Đăng ký tour" (lớp control trong Sequence Diagram).</summary>
    public class DangKyTourController
    {
        public const int SoNguoiToiThieuCuaDoan = 13; // khách theo đoàn: trên 12 người

        private readonly TourDAL _tourDal = new TourDAL();
        private readonly PhieuDangKyDAL _phieuDal = new PhieuDangKyDAL();

        public List<Tour> LayDanhSachTour()
        {
            return _tourDal.LayDanhSach();
        }

        public int LapPhieu(DoanKhach doan, PhieuDangKyDoan phieu, List<NguoiDi> dsNguoiDi, decimal tienCoc)
        {
            KiemTraHopLe(doan, phieu, dsNguoiDi, tienCoc);
            return _phieuDal.LapPhieu(doan, phieu, dsNguoiDi, tienCoc);
        }

        private static void KiemTraHopLe(DoanKhach doan, PhieuDangKyDoan phieu, List<NguoiDi> dsNguoiDi, decimal tienCoc)
        {
            if (string.IsNullOrWhiteSpace(phieu.MaTour))
                throw new NghiepVuException("Vui lòng chọn tour.");
            if (phieu.NgayDi.Date < DateTime.Today)
                throw new NghiepVuException("Ngày đi không được nhỏ hơn ngày hiện tại.");

            if (string.IsNullOrWhiteSpace(doan.TenCoQuan))
                throw new NghiepVuException("Vui lòng nhập tên cơ quan (hoặc tên đại diện gia đình).");
            if (string.IsNullOrWhiteSpace(doan.DiaChi))
                throw new NghiepVuException("Vui lòng nhập địa chỉ.");
            if (string.IsNullOrWhiteSpace(doan.DienThoai) || !Regex.IsMatch(doan.DienThoai.Trim(), @"^[0-9 .+\-]{8,15}$"))
                throw new NghiepVuException("Số điện thoại không hợp lệ (8-15 ký tự gồm số, khoảng trắng, +, -).");
            if (string.IsNullOrWhiteSpace(doan.NguoiDaiDien))
                throw new NghiepVuException("Vui lòng nhập tên người đại diện.");
            if (phieu.SoNguoi < SoNguoiToiThieuCuaDoan)
                throw new NghiepVuException("Khách theo đoàn phải đi trên 12 người. Khách dưới 12 người vui lòng đăng ký theo chuyến.");
            if (string.IsNullOrWhiteSpace(phieu.DiaDiemDon))
                throw new NghiepVuException("Vui lòng nhập địa điểm đón của đoàn.");

            if (phieu.CoBaoHiem)
            {
                if (dsNguoiDi == null || dsNguoiDi.Count == 0)
                    throw new NghiepVuException("Đoàn có mua bảo hiểm phải kèm danh sách người đi.");
                if (dsNguoiDi.Count != phieu.SoNguoi)
                    throw new NghiepVuException(string.Format(
                        "Danh sách người đi có {0} người nhưng số người của đoàn là {1}.", dsNguoiDi.Count, phieu.SoNguoi));

                int namHienTai = DateTime.Today.Year;
                for (int i = 0; i < dsNguoiDi.Count; i++)
                {
                    var n = dsNguoiDi[i];
                    if (string.IsNullOrWhiteSpace(n.HoTen))
                        throw new NghiepVuException("Họ tên ở dòng " + (i + 1) + " của danh sách người đi đang trống.");
                    if (n.NamSinh < 1900 || n.NamSinh > namHienTai)
                        throw new NghiepVuException("Năm sinh ở dòng " + (i + 1) + " của danh sách người đi không hợp lệ.");
                    if (string.IsNullOrWhiteSpace(n.GioiTinh))
                        throw new NghiepVuException("Vui lòng chọn giới tính ở dòng " + (i + 1) + " của danh sách người đi.");
                }
            }

            if (tienCoc <= 0)
                throw new NghiepVuException("Khách đăng ký theo đoàn phải đặt cọc trước, số tiền cọc phải lớn hơn 0.");
        }
    }
}
