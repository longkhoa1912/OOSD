using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using QltThucTap.WinForms.Models;

namespace QltThucTap.WinForms.Forms
{
    public partial class FrmMain : Form
    {
        private UserSessionDto user;

        public FrmMain()
        {
            InitializeComponent();
            GanSuKien();
            DoiTrangThaiDangNhap();
            DinhDangNutChucNang();
        }

        private void GanSuKien()
        {
            btnDangKyTaiKhoan.Click += (sender, args) =>
                MoCongCu("Đăng ký tài khoản", "uc02");
            btnDangNhap.Click += (sender, args) => DangNhap();
            btnDangXuat.Click += (sender, args) => DangXuat();
            btnThoat.Click += (sender, args) => Close();

            btnHoSo.Click += (sender, args) => YeuCauVaiTro("SinhVien", "Hồ sơ cá nhân");
            btnTimKiemDN.Click += (sender, args) => YeuCauVaiTro("SinhVien", "Tìm kiếm doanh nghiệp");
            btnXemViTri.Click += (sender, args) => ChayTheoVaiTro("SinhVien", MoDanhSachViTri);
            btnDangKyThucTap.Click += (sender, args) => ChayTheoVaiTro("SinhVien", MoFormDangKy);
            btnTheoDoiDon.Click += (sender, args) => YeuCauVaiTro("SinhVien", "Theo dõi đơn thực tập");
            btnNopNhatKy.Click += (sender, args) => YeuCauVaiTro("SinhVien", "Nộp nhật ký");
            btnNopBaoCao.Click += (sender, args) => YeuCauVaiTro("SinhVien", "Nộp báo cáo");
            btnDanhGiaDoanhNghiep.Click += (sender, args) => YeuCauVaiTro("SinhVien", "Đánh giá doanh nghiệp");

            btnQuanLyViTri.Click += (sender, args) => YeuCauVaiTro("DoanhNghiep", "Quản lý vị trí thực tập");
            btnXetDuyetSinhVien.Click += (sender, args) => ChayTheoVaiTro("DoanhNghiep", MoFormXetDuyet);
            btnTheoDoiSinhVien.Click += (sender, args) => YeuCauVaiTro("DoanhNghiep", "Theo dõi sinh viên");
            btnDanhGiaSinhVien.Click += (sender, args) => YeuCauVaiTro("DoanhNghiep", "Đánh giá sinh viên");

            btnQuanLyHuongDan.Click += (sender, args) => YeuCauVaiTro("GiangVien", "Quản lý sinh viên hướng dẫn");
            btnTheoDoiTienDo.Click += (sender, args) => YeuCauVaiTro("GiangVien", "Theo dõi tiến độ");
            btnDanhGiaBaoCao.Click += (sender, args) => YeuCauVaiTro("GiangVien", "Đánh giá báo cáo");

            btnQuanLyTaiKhoan.Click += (sender, args) => YeuCauVaiTro("Admin", "Quản lý tài khoản");
            btnQuanLyDoanhNghiep.Click += (sender, args) => YeuCauVaiTro("Admin", "Quản lý doanh nghiệp");
            btnQuanLyGiangVien.Click += (sender, args) => YeuCauVaiTro("Admin", "Quản lý giảng viên");
            btnQuanLyDot.Click += (sender, args) => YeuCauVaiTro("Admin", "Quản lý đợt thực tập");
            btnThongKe.Click += (sender, args) => YeuCauVaiTro("Admin", "Thống kê");
            btnQuanLyDanhGiaDoanhNghiep.Click += (sender, args) => YeuCauVaiTro("Admin", "Quản lý đánh giá doanh nghiệp");
        }

        private void DinhDangNutChucNang()
        {
            StyleButton(btnHoSo, Color.FromArgb(220, 235, 255), Color.FromArgb(42, 92, 170));
            StyleButton(btnTimKiemDN, Color.FromArgb(220, 235, 255), Color.FromArgb(42, 92, 170));
            StyleButton(btnXemViTri, Color.FromArgb(220, 235, 255), Color.FromArgb(42, 92, 170));
            StyleButton(btnDangKyThucTap, Color.FromArgb(220, 235, 255), Color.FromArgb(42, 92, 170));
            StyleButton(btnTheoDoiDon, Color.FromArgb(220, 235, 255), Color.FromArgb(42, 92, 170));
            StyleButton(btnNopNhatKy, Color.FromArgb(220, 235, 255), Color.FromArgb(42, 92, 170));
            StyleButton(btnNopBaoCao, Color.FromArgb(220, 235, 255), Color.FromArgb(42, 92, 170));
            StyleButton(btnDanhGiaDoanhNghiep, Color.FromArgb(220, 235, 255), Color.FromArgb(42, 92, 170));

            StyleButton(btnQuanLyViTri, Color.FromArgb(218, 244, 240), Color.FromArgb(16, 120, 117));
            StyleButton(btnXetDuyetSinhVien, Color.FromArgb(218, 244, 240), Color.FromArgb(16, 120, 117));
            StyleButton(btnTheoDoiSinhVien, Color.FromArgb(218, 244, 240), Color.FromArgb(16, 120, 117));
            StyleButton(btnDanhGiaSinhVien, Color.FromArgb(218, 244, 240), Color.FromArgb(16, 120, 117));

            StyleButton(btnQuanLyHuongDan, Color.FromArgb(237, 229, 251), Color.FromArgb(105, 69, 166));
            StyleButton(btnTheoDoiTienDo, Color.FromArgb(237, 229, 251), Color.FromArgb(105, 69, 166));
            StyleButton(btnDanhGiaBaoCao, Color.FromArgb(237, 229, 251), Color.FromArgb(105, 69, 166));

            StyleButton(btnQuanLyTaiKhoan, Color.FromArgb(255, 239, 218), Color.FromArgb(164, 96, 24));
            StyleButton(btnQuanLyDoanhNghiep, Color.FromArgb(255, 239, 218), Color.FromArgb(164, 96, 24));
            StyleButton(btnQuanLyGiangVien, Color.FromArgb(255, 239, 218), Color.FromArgb(164, 96, 24));
            StyleButton(btnQuanLyDot, Color.FromArgb(255, 239, 218), Color.FromArgb(164, 96, 24));
            StyleButton(btnThongKe, Color.FromArgb(255, 239, 218), Color.FromArgb(164, 96, 24));
            StyleButton(btnQuanLyDanhGiaDoanhNghiep, Color.FromArgb(255, 239, 218), Color.FromArgb(164, 96, 24));
        }

        private static void StyleButton(Button button, Color backColor, Color foreColor)
        {
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.UseVisualStyleBackColor = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.FromArgb(220, 226, 237);
            button.FlatAppearance.BorderSize = 1;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        private void ChayTheoVaiTro(string vaiTro, Action hanhDong)
        {
            if (!KiemTraVaiTro(vaiTro))
                return;
            hanhDong();
        }

        private void YeuCauVaiTro(string vaiTro, string tenChucNang)
        {
            if (KiemTraVaiTro(vaiTro))
            {
                var routes = new Dictionary<string, string>
                {
                    ["Hồ sơ cá nhân"] = "uc03-capnhat",
                    ["Tìm kiếm doanh nghiệp"] = "uc04",
                    ["Theo dõi đơn thực tập"] = "uc07",
                    ["Nộp nhật ký"] = "uc15",
                    ["Nộp báo cáo"] = "uc16",
                    ["Đánh giá doanh nghiệp"] = "uc22",
                    ["Quản lý vị trí thực tập"] = "uc08",
                    ["Theo dõi sinh viên"] = "uc10",
                    ["Đánh giá sinh viên"] = "uc11",
                    ["Quản lý sinh viên hướng dẫn"] = "uc12",
                    ["Theo dõi tiến độ"] = "uc13",
                    ["Đánh giá báo cáo"] = "uc14",
                    ["Quản lý tài khoản"] = "uc17",
                    ["Quản lý doanh nghiệp"] = "uc18",
                    ["Quản lý giảng viên"] = "uc19",
                    ["Quản lý đợt thực tập"] = "uc20",
                    ["Thống kê"] = "uc21",
                    ["Quản lý đánh giá doanh nghiệp"] = "uc23"
                };
                if (routes.TryGetValue(tenChucNang, out string route))
                    MoCongCu(tenChucNang, route);
            }
        }

        private void MoCongCu(string title, string route)
        {
            Form form;
            var currentUser = AppSession.CurrentUser;
            switch (route)
            {
                case "uc02": form = new FrmDangKyTaiKhoan(currentUser); break;
                case "uc03-capnhat": form = new FrmHoSoCaNhan(currentUser); break;
                case "uc04": form = new FrmTimKiemDoanhNghiep(currentUser); break;
                case "uc07": form = new FrmTheoDoiDonDangKy(currentUser); break;
                case "uc08": form = new FrmQuanLyViTri(currentUser); break;
                case "uc10": form = new FrmTheoDoiSinhVienDoanhNghiep(currentUser); break;
                case "uc11": form = new FrmDanhGiaSinhVien(currentUser); break;
                case "uc12": form = new FrmSinhVienDuocPhanCong(currentUser); break;
                case "uc13": form = new FrmTheoDoiTienDo(currentUser); break;
                case "uc14": form = new FrmDanhGiaBaoCao(currentUser); break;
                case "uc15": form = new FrmNopNhatKy(currentUser); break;
                case "uc16": form = new FrmNopBaoCao(currentUser); break;
                case "uc17": form = new FrmQuanLyTaiKhoan(currentUser); break;
                case "uc18": form = new FrmQuanLyDoanhNghiep(currentUser); break;
                case "uc19": form = new FrmQuanLyGiangVien(currentUser); break;
                case "uc20": form = new FrmQuanLyDotThucTap(currentUser); break;
                case "uc21": form = new FrmThongKe(currentUser); break;
                case "uc22": form = new FrmDanhGiaDoanhNghiep(currentUser); break;
                case "uc23": form = new FrmQuanLyDanhGiaDoanhNghiep(currentUser); break;
                default: form = null; break;
            }
            if (form == null)
            {
                MessageBox.Show("Màn hình chức năng chưa được cấu hình.", title,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (form)
                form.ShowDialog(this);
        }

        private bool KiemTraVaiTro(string vaiTro)
        {
            if (AppSession.CurrentUser == null)
            {
                var confirm = MessageBox.Show(
                    "Bạn cần đăng nhập để mở chức năng này. Đăng nhập ngay?",
                    "Yêu cầu đăng nhập", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (confirm != DialogResult.Yes)
                    return false;

                DangNhap();
            }

            if (AppSession.CurrentUser == null)
                return false;

            if (!string.Equals(AppSession.CurrentUser.VaiTro, vaiTro,
                    StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Tài khoản hiện tại không có quyền truy cập chức năng này.",
                    "Không đủ quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            user = AppSession.CurrentUser;
            DoiTrangThaiDangNhap();
            return true;
        }

        private void DangNhap()
        {
            if (AppSession.CurrentUser == null)
            {
                using (var form = new FrmDangNhap())
                {
                    form.ShowDialog(this);
                }
            }

            user = AppSession.CurrentUser;
            DoiTrangThaiDangNhap();
        }

        private void DangXuat()
        {
            if (AppSession.CurrentUser == null)
            {
                MessageBox.Show("Bạn đang sử dụng với quyền khách.",
                    "Đăng xuất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AppSession.CurrentUser = null;
            user = null;
            DoiTrangThaiDangNhap();
        }

        private void DoiTrangThaiDangNhap()
        {
            bool dangNhap = AppSession.CurrentUser != null;
            btnDangNhap.Enabled = !dangNhap;
            btnDangXuat.Enabled = dangNhap;

            lblSession.Text = dangNhap
                ? "Đang đăng nhập: " + AppSession.CurrentUser.TenDangNhap +
                  "  |  Vai trò: " + AppSession.CurrentUser.VaiTro
                : "Khách - hãy đăng nhập để sử dụng chức năng";
        }

        private void MoDanhSachViTri()
        {
            var form = new FrmDanhSachViTri();
            form.StartPosition = FormStartPosition.CenterParent;
            form.Show(this);
        }

        private void MoFormDangKy()
        {
            var form = new FrmDangKyThucTap(user);
            form.StartPosition = FormStartPosition.CenterParent;
            form.Show(this);
        }

        private void MoFormXetDuyet()
        {
            var form = new FrmXetDuyetDon(user);
            form.StartPosition = FormStartPosition.CenterParent;
            form.Show(this);
        }
    }
}
