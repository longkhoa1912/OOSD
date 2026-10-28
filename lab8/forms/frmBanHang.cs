using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyNGK.Models;
using QuanLyNGK.Services;

namespace QuanLyNGK.UI
{
    /// <summary>Form 2: Bán hàng &amp; Lập hóa đơn (UC04, UC05, UC06).</summary>
    public partial class frmBanHang : Form
    {
        private readonly DanhMucService _danhMuc = new DanhMucService();
        private readonly BanHangService _banHang = new BanHangService();

        private List<KhachHang> _dsKhach = new List<KhachHang>();
        private List<NuocGiaiKhat> _dsNGK = new List<NuocGiaiKhat>();
        private readonly BindingList<ChiTietBan> _gio = new BindingList<ChiTietBan>();
        private bool _dangNap;

        public frmBanHang()
        {
            InitializeComponent();
        }

        private KhachHang KhachDangChon { get { return cboKhachHang.SelectedItem as KhachHang; } }
        private decimal TongTien { get { return _gio.Sum(x => x.ThanhTien); } }

        // ================= LOAD =================
        private void frmBanHang_Load(object sender, EventArgs e)
        {
            try
            {
                CauHinhLuoi();
                NapDanhMuc();
                LamMoiPhieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được cơ sở dữ liệu:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CauHinhLuoi()
        {
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.Columns.Clear();
            ThemCot("MaNGK", "Mã NGK", 80, null, false, false);
            ThemCot("TenNGK", "Tên NGK", 100, null, false, true);
            ThemCot("DonViTinh", "ĐVT", 70, null, false, false);
            ThemCot("SoLuongTon", "Tồn kho", 80, "N0", true, false);
            ThemCot("SoLuong", "Số lượng", 80, "N0", true, false);
            ThemCot("DonGia", "Đơn giá", 100, "N0", true, false);
            ThemCot("ThanhTien", "Thành tiền", 120, "N0", true, false);
            ThemCot("SoLuongThieu", "Thiếu (hẹn giao)", 110, "N0", true, false);
            dgvChiTiet.DataSource = _gio;
        }

        private void ThemCot(string prop, string header, int width, string format, bool canPhai, bool fill)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = "col" + prop,
                DataPropertyName = prop,
                HeaderText = header,
                Width = width,
                ReadOnly = true
            };
            if (fill) col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if (format != null) col.DefaultCellStyle.Format = format;
            if (canPhai) col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChiTiet.Columns.Add(col);
        }

        private void NapDanhMuc()
        {
            _dangNap = true;

            _dsKhach = _danhMuc.LayKhachHang();
            cboKhachHang.DisplayMember = "HienThi";
            cboKhachHang.ValueMember = "MaKH";
            cboKhachHang.DataSource = _dsKhach;
            cboKhachHang.SelectedIndex = -1;

            cboNhanVien.DisplayMember = "HienThi";
            cboNhanVien.ValueMember = "MaNV";
            cboNhanVien.DataSource = _danhMuc.LayNhanVien();
            cboNhanVien.SelectedIndex = -1;

            NapNGK();

            _dangNap = false;
        }

        /// <summary>Nạp lại danh sách NGK (cập nhật tồn kho mới nhất).</summary>
        private void NapNGK()
        {
            bool dangNapTruoc = _dangNap;
            _dangNap = true;

            string dangChon = cboNGK.SelectedValue as string;
            _dsNGK = _banHang.LayDanhSachNGK();
            cboNGK.DataSource = null;
            cboNGK.DisplayMember = "HienThi";
            cboNGK.ValueMember = "MaNGK";
            cboNGK.DataSource = _dsNGK;
            if (dangChon != null) cboNGK.SelectedValue = dangChon;

            _dangNap = dangNapTruoc;
            HienThongTinNGK();
        }

        // ================= KHÁCH HÀNG =================
        private void btnTimKH_Click(object sender, EventArgs e)
        {
            string kw = txtTimKH.Text.Trim();
            if (kw.Length == 0)
            {
                ThongBao("Nhập số điện thoại hoặc mã khách hàng cần tìm.", MessageBoxIcon.Information);
                return;
            }

            KhachHang kh = _dsKhach.FirstOrDefault(x =>
                x.MaKH.Equals(kw, StringComparison.OrdinalIgnoreCase) || x.SoDienThoai == kw);
            if (kh == null)
            {
                ThongBao("Không tìm thấy khách hàng '" + kw + "'.", MessageBoxIcon.Warning);
                return;
            }
            cboKhachHang.SelectedValue = kh.MaKH;
            lblTrangThai.Text = "Đã chọn khách hàng " + kh.TenKH + ".";
        }

        private void txtTimKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTimKH.PerformClick();
            }
        }

        private void cboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dangNap) return;
            CapNhatLoaiKhach();
            if (KhachDangChon != null && !KhachDangChon.LaKhachQuen)
                DieuChinhChoKhachVangLai();
            CapNhatTongKet(true);
        }

        private void CapNhatLoaiKhach()
        {
            KhachHang kh = KhachDangChon;
            if (kh == null)
            {
                lblLoaiKhach.Text = "";
                return;
            }
            if (kh.LaKhachQuen)
            {
                lblLoaiKhach.Text = "Khách quen: được hẹn giao nhiều đợt & ghi nợ";
                lblLoaiKhach.ForeColor = Color.SeaGreen;
            }
            else
            {
                lblLoaiKhach.Text = "Khách vãng lai: thanh toán đủ, giao ngay";
                lblLoaiKhach.ForeColor = Color.DarkOrange;
            }
        }

        /// <summary>BR06: đổi sang khách vãng lai thì chỉ được mua tối đa bằng tồn kho.</summary>
        private void DieuChinhChoKhachVangLai()
        {
            bool coDoi = false;
            foreach (ChiTietBan d in _gio.ToList())
            {
                if (d.SoLuong <= d.SoLuongTon) continue;
                coDoi = true;
                if (d.SoLuongTon <= 0) _gio.Remove(d);
                else d.SoLuong = d.SoLuongTon;
            }
            if (coDoi)
            {
                _gio.ResetBindings();
                ThongBao("Khách vãng lai chỉ được mua theo số lượng hiện có trong kho. " +
                         "Giỏ hàng đã được điều chỉnh.", MessageBoxIcon.Warning);
            }
        }

        // ================= CHỌN HÀNG =================
        private void cboNGK_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dangNap) return;
            HienThongTinNGK();
        }

        private void HienThongTinNGK()
        {
            NuocGiaiKhat n = cboNGK.SelectedItem as NuocGiaiKhat;
            lblThongTinNGK.Text = n == null
                ? ""
                : "Tồn: " + n.SoLuongTon.ToString("N0") + " " + n.DonViTinh + " | Giá: " + n.DonGiaBan.ToString("N0");
        }

        private void btnThemHang_Click(object sender, EventArgs e)
        {
            KhachHang kh = KhachDangChon;
            if (kh == null)
            {
                ThongBao("Vui lòng chọn khách hàng trước khi chọn hàng.", MessageBoxIcon.Warning);
                return;
            }
            NuocGiaiKhat ngk = cboNGK.SelectedItem as NuocGiaiKhat;
            if (ngk == null)
            {
                ThongBao("Vui lòng chọn mặt hàng.", MessageBoxIcon.Warning);
                return;
            }

            int sl = (int)nudSoLuong.Value;
            ChiTietBan dong = _gio.FirstOrDefault(x => x.MaNGK == ngk.MaNGK);
            int tongSL = sl + (dong != null ? dong.SoLuong : 0);

            // BR04: kiểm tra tồn kho
            if (tongSL > ngk.SoLuongTon)
            {
                if (!kh.LaKhachQuen)
                {
                    if (ngk.SoLuongTon <= 0)
                    {
                        ThongBao("'" + ngk.TenNGK + "' đã hết hàng. Khách vãng lai không thể đặt trước.", MessageBoxIcon.Warning);
                        return;
                    }
                    tongSL = ngk.SoLuongTon;
                    ThongBao("Kho chỉ còn " + ngk.SoLuongTon + " " + ngk.DonViTinh +
                             ". Khách vãng lai chỉ được mua theo số lượng hiện có nên số lượng đã được điều chỉnh.",
                             MessageBoxIcon.Warning);
                }
                else
                {
                    ThongBao("Kho chỉ còn " + ngk.SoLuongTon + " " + ngk.DonViTinh +
                             ". Phần thiếu " + (tongSL - ngk.SoLuongTon) + " sẽ được lập Phiếu hẹn giao.",
                             MessageBoxIcon.Information);
                }
            }

            if (dong == null)
            {
                _gio.Add(new ChiTietBan
                {
                    MaNGK = ngk.MaNGK,
                    TenNGK = ngk.TenNGK,
                    DonViTinh = ngk.DonViTinh,
                    SoLuongTon = ngk.SoLuongTon,
                    SoLuong = tongSL,
                    DonGia = ngk.DonGiaBan
                });
            }
            else
            {
                dong.SoLuong = tongSL;
                _gio.ResetBindings();
            }

            nudSoLuong.Value = 1;
            CapNhatTongKet(true);
        }

        private void btnXoaHang_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow == null)
            {
                ThongBao("Chọn dòng cần xóa.", MessageBoxIcon.Information);
                return;
            }
            _gio.RemoveAt(dgvChiTiet.CurrentRow.Index);
            CapNhatTongKet(true);
        }

        // ================= TỔNG KẾT / CÔNG NỢ =================
        private void CapNhatTongKet(bool datLaiKhachTra)
        {
            decimal tong = TongTien;
            lblTongTien.Text = tong.ToString("N0") + " đ";

            bool quen = KhachDangChon != null && KhachDangChon.LaKhachQuen;

            nudKhachTra.Maximum = tong;
            if (datLaiKhachTra || !quen) nudKhachTra.Value = tong;   // vãng lai: trả 100% (BR08)
            nudKhachTra.Enabled = quen && tong > 0;

            CapNhatNo();
        }

        private void nudKhachTra_ValueChanged(object sender, EventArgs e)
        {
            CapNhatNo();
        }

        private void CapNhatNo()
        {
            decimal no = TongTien - nudKhachTra.Value;
            lblConNo.Text = no.ToString("N0") + " đ";

            bool coNo = no > 0;
            dtpNgayTra.Enabled = coNo;
            txtLyDoNo.Enabled = coNo;
            if (!coNo) txtLyDoNo.Clear();

            bool coThieu = _gio.Any(x => x.SoLuongThieu > 0);
            dtpNgayHen.Enabled = coThieu;
            lblCanhBao.Text = coThieu
                ? "Kho thiếu hàng: hệ thống sẽ lập Phiếu hẹn giao cho phần thiếu vào ngày hẹn giao đã chọn."
                : "";
        }

        // ================= LẬP HÓA ĐƠN =================
        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            KhachHang kh = KhachDangChon;
            NhanVien nv = cboNhanVien.SelectedItem as NhanVien;

            var rq = new LapHoaDonRequest
            {
                MaKH = kh != null ? kh.MaKH : null,
                MaNV = nv != null ? nv.MaNV : null,
                NgayLap = dtpNgayLap.Value.Date,
                ChiTiet = _gio.ToList(),
                SoTienKhachTra = nudKhachTra.Value,
                NgayHenGiao = dtpNgayHen.Value.Date,
                NgayDuKienTra = dtpNgayTra.Value.Date,
                LyDoNo = txtLyDoNo.Text.Trim()
            };

            if (rq.ChiTiet.Count > 0 &&
                MessageBox.Show("Xác nhận lập hóa đơn với tổng tiền " + TongTien.ToString("N0") + " đ?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            ServiceResult kq = _banHang.LapHoaDon(rq);
            lblTrangThai.Text = kq.Message.Replace("\n", " ");

            if (!kq.Success)
            {
                MessageBox.Show(kq.Message, "Không lập được hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(kq.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NapNGK();          // cập nhật tồn kho mới
            LamMoiPhieu();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiPhieu();
        }

        private void LamMoiPhieu()
        {
            _gio.Clear();
            txtMaHD.Text = "(tự động)";
            dtpNgayLap.Value = DateTime.Today;
            dtpNgayHen.Value = DateTime.Today.AddDays(3);
            dtpNgayTra.Value = DateTime.Today.AddDays(7);
            txtLyDoNo.Clear();
            nudSoLuong.Value = 1;
            txtTimKH.Clear();
            CapNhatLoaiKhach();
            CapNhatTongKet(true);
        }

        // ================= KHÁC =================
        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            using (var f = new frmNuocGiaiKhat())
            {
                f.ShowDialog(this);
            }
            NapNGK();   // làm mới danh sách NGK sau khi chỉnh danh mục
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ThongBao(string noiDung, MessageBoxIcon icon)
        {
            lblTrangThai.Text = noiDung;
            MessageBox.Show(noiDung, "Thông báo", MessageBoxButtons.OK, icon);
        }
    }
}
