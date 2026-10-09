using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyTourDuLich.BLL;
using QuanLyTourDuLich.Models;

namespace QuanLyTourDuLich.GUI
{
    public partial class frmLapPhieuDangKyDoan : Form
    {
        private readonly DangKyTourController _controller = new DangKyTourController();

        public frmLapPhieuDangKyDoan()
        {
            InitializeComponent();
        }

        private void frmLapPhieuDangKyDoan_Load(object sender, EventArgs e)
        {
            dtpNgayDi.MinDate = DateTime.Today;
            KhoiTaoLuoi();
            TaiDanhSachTour();
            CapNhatTrangThaiBaoHiem();
        }

        private void KhoiTaoLuoi()
        {
            dgvNguoiDi.AutoGenerateColumns = false;
            dgvNguoiDi.Columns.Clear();

            dgvNguoiDi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHoTen",
                HeaderText = "Họ và tên",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvNguoiDi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNamSinh",
                HeaderText = "Năm sinh",
                Width = 110
            });

            var colGioiTinh = new DataGridViewComboBoxColumn
            {
                Name = "colGioiTinh",
                HeaderText = "Giới tính",
                Width = 110
            };
            colGioiTinh.Items.Add("Nam");
            colGioiTinh.Items.Add("Nữ");
            dgvNguoiDi.Columns.Add(colGioiTinh);
        }

        private void TaiDanhSachTour()
        {
            try
            {
                List<Tour> dsTour = _controller.LayDanhSachTour();
                cboTour.DataSource = dsTour;
                cboTour.DisplayMember = "TenHienThi";
                cboTour.ValueMember = "MaTour";
                CapNhatThongTinTour();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Không kết nối được cơ sở dữ liệu. Hãy kiểm tra chuỗi kết nối trong App.config " +
                    "và đã chạy script TaoCSDL.sql chưa.\n\nChi tiết: " + ex.Message,
                    "Lỗi cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboTour_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatThongTinTour();
        }

        private void nudSoNguoi_ValueChanged(object sender, EventArgs e)
        {
            CapNhatTongTien();
        }

        private void chkBaoHiem_CheckedChanged(object sender, EventArgs e)
        {
            CapNhatTrangThaiBaoHiem();
        }

        private void CapNhatThongTinTour()
        {
            var tour = cboTour.SelectedItem as Tour;
            if (tour == null)
            {
                txtSoNgayDem.Text = string.Empty;
                txtDonGia.Text = string.Empty;
            }
            else
            {
                txtSoNgayDem.Text = tour.SoNgay + " ngày " + tour.SoDem + " đêm";
                txtDonGia.Text = tour.DonGia.ToString("N0") + " đ";
            }
            CapNhatTongTien();
        }

        private void CapNhatTongTien()
        {
            var tour = cboTour.SelectedItem as Tour;
            if (tour == null)
            {
                txtTongTien.Text = string.Empty;
                return;
            }
            decimal tong = tour.DonGia * nudSoNguoi.Value;
            txtTongTien.Text = tong.ToString("N0") + " đ";
        }

        private void CapNhatTrangThaiBaoHiem()
        {
            grpNguoiDi.Enabled = chkBaoHiem.Checked;
        }

        private List<NguoiDi> LayDanhSachNguoiDi()
        {
            var ds = new List<NguoiDi>();
            foreach (DataGridViewRow row in dgvNguoiDi.Rows)
            {
                if (row.IsNewRow) continue;

                string hoTen = (row.Cells[0].Value ?? string.Empty).ToString().Trim();
                string namSinhText = (row.Cells[1].Value ?? string.Empty).ToString().Trim();
                string gioiTinh = (row.Cells[2].Value ?? string.Empty).ToString().Trim();

                if (hoTen.Length == 0 && namSinhText.Length == 0 && gioiTinh.Length == 0) continue;

                int namSinh;
                if (!int.TryParse(namSinhText, out namSinh))
                    throw new NghiepVuException("Năm sinh ở dòng " + (row.Index + 1) + " của danh sách người đi không hợp lệ.");

                ds.Add(new NguoiDi { HoTen = hoTen, NamSinh = namSinh, GioiTinh = gioiTinh });
            }
            return ds;
        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            var tour = cboTour.SelectedItem as Tour;

            var doan = new DoanKhach
            {
                TenCoQuan = txtTenCoQuan.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                DienThoai = txtDienThoai.Text.Trim(),
                NguoiDaiDien = txtNguoiDaiDien.Text.Trim()
            };

            var phieu = new PhieuDangKyDoan
            {
                MaTour = tour == null ? null : tour.MaTour,
                NgayDi = dtpNgayDi.Value.Date,
                SoNguoi = (int)nudSoNguoi.Value,
                DiaDiemDon = txtDiaDiemDon.Text.Trim(),
                CoBaoHiem = chkBaoHiem.Checked
            };

            try
            {
                List<NguoiDi> dsNguoiDi = chkBaoHiem.Checked ? LayDanhSachNguoiDi() : new List<NguoiDi>();
                decimal tienCoc = nudTienCoc.Value;

                int maPhieu = _controller.LapPhieu(doan, phieu, dsNguoiDi, tienCoc);

                MessageBox.Show(
                    "Lập phiếu đăng ký thành công!\n\n" +
                    "Mã phiếu: " + maPhieu + "\n" +
                    "Đoàn: " + doan.TenCoQuan + " (" + phieu.SoNguoi + " người)\n" +
                    "Tour: " + tour.TenHienThi + "\n" +
                    "Ngày đi: " + phieu.NgayDi.ToString("dd/MM/yyyy") + "\n" +
                    "Tổng tiền dự kiến: " + txtTongTien.Text + "\n" +
                    "Đã đặt cọc: " + tienCoc.ToString("N0") + " đ",
                    "Lập phiếu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LamMoi();
            }
            catch (NghiepVuException ex)
            {
                MessageBox.Show(ex.Message, "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lưu được phiếu đăng ký.\n\nChi tiết: " + ex.Message,
                    "Lỗi cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LamMoi()
        {
            txtTenCoQuan.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtNguoiDaiDien.Clear();
            txtDiaDiemDon.Clear();
            nudSoNguoi.Value = nudSoNguoi.Minimum;
            nudTienCoc.Value = 0;
            chkBaoHiem.Checked = false;
            dgvNguoiDi.Rows.Clear();
            dtpNgayDi.Value = DateTime.Today;
            if (cboTour.Items.Count > 0) cboTour.SelectedIndex = 0;
            CapNhatTongTien();
            txtTenCoQuan.Focus();
        }
    }
}
