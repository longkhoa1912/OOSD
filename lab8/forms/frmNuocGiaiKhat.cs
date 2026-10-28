using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyNGK.Models;
using QuanLyNGK.Services;

namespace QuanLyNGK.UI
{
    /// <summary>Form 1: Quản lý danh mục Nước giải khát (UC01).</summary>
    public partial class frmNuocGiaiKhat : Form
    {
        private readonly NuocGiaiKhatService _service = new NuocGiaiKhatService();
        private readonly DanhMucService _danhMuc = new DanhMucService();

        public frmNuocGiaiKhat()
        {
            InitializeComponent();
        }

        private void frmNuocGiaiKhat_Load(object sender, EventArgs e)
        {
            try
            {
                cboDonViTinh.Items.AddRange(new object[] { "Lon", "Chai", "Thùng", "Két" });

                // ComboBox khóa ngoại: Nhãn hiệu, Nhà cung ứng
                cboNhanHieu.DisplayMember = "TenLoai";
                cboNhanHieu.ValueMember = "MaLoai";
                cboNhanHieu.DataSource = _danhMuc.LayNhanHieu();

                cboNhaCungUng.DisplayMember = "TenNCU";
                cboNhaCungUng.ValueMember = "MaNCU";
                cboNhaCungUng.DataSource = _danhMuc.LayNhaCungUng();

                NapLuoi();
                DatLaiForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được cơ sở dữ liệu:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Lưới ----------
        private void NapLuoi()
        {
            dgvNGK.DataSource = _service.TimKiem(txtTimKiem.Text.Trim());

            var tieuDe = new Dictionary<string, string>
            {
                { "maNGK", "Mã NGK" }, { "tenNGK", "Tên NGK" }, { "donViTinh", "ĐVT" },
                { "soLuongTon", "Tồn kho" }, { "donGiaBan", "Đơn giá bán" },
                { "tenLoai", "Nhãn hiệu" }, { "tenNCU", "Nhà cung ứng" }
            };
            foreach (DataGridViewColumn c in dgvNGK.Columns)
            {
                string t;
                if (tieuDe.TryGetValue(c.Name, out t)) c.HeaderText = t;
            }
            dgvNGK.Columns["maLoai"].Visible = false;
            dgvNGK.Columns["maNCU"].Visible = false;
            dgvNGK.Columns["donGiaBan"].DefaultCellStyle.Format = "N0";
            dgvNGK.Columns["donGiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvNGK.Columns["soLuongTon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvNGK.ClearSelection();
        }

        private void dgvNGK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow r = dgvNGK.Rows[e.RowIndex];

            txtMaNGK.Text = Convert.ToString(r.Cells["maNGK"].Value);
            txtTenNGK.Text = Convert.ToString(r.Cells["tenNGK"].Value);
            cboDonViTinh.Text = Convert.ToString(r.Cells["donViTinh"].Value);
            nudSoLuongTon.Value = Convert.ToDecimal(r.Cells["soLuongTon"].Value);
            nudDonGia.Value = Convert.ToDecimal(r.Cells["donGiaBan"].Value);
            cboNhanHieu.SelectedValue = Convert.ToString(r.Cells["maLoai"].Value);
            cboNhaCungUng.SelectedValue = Convert.ToString(r.Cells["maNCU"].Value);

            // Chế độ chỉnh sửa: khóa mã NGK, khóa tồn kho (tồn chỉ đổi qua nhập/bán hàng)
            txtMaNGK.ReadOnly = true;
            nudSoLuongTon.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        // ---------- Nút chức năng ----------
        private void btnThem_Click(object sender, EventArgs e)
        {
            ServiceResult kq = _service.Them(LayDuLieu());
            HienKetQua(kq);
            if (kq.Success)
            {
                NapLuoi();
                DatLaiForm();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ServiceResult kq = _service.Sua(LayDuLieu());
            HienKetQua(kq);
            if (kq.Success)
            {
                NapLuoi();
                DatLaiForm();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaNGK.Text.Trim();
            if (MessageBox.Show("Bạn có chắc muốn xóa nước giải khát '" + ma + "'?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            ServiceResult kq = _service.Xoa(ma);
            HienKetQua(kq);
            if (kq.Success)
            {
                NapLuoi();
                DatLaiForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            NapLuoi();
            DatLaiForm();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            NapLuoi();
            lblTrangThai.Text = "Tìm thấy " + dgvNGK.Rows.Count + " mặt hàng.";
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTimKiem.PerformClick();
            }
        }

        // ---------- Hỗ trợ ----------
        private NuocGiaiKhat LayDuLieu()
        {
            return new NuocGiaiKhat
            {
                MaNGK = txtMaNGK.Text.Trim(),
                TenNGK = txtTenNGK.Text.Trim(),
                DonViTinh = cboDonViTinh.Text.Trim(),
                SoLuongTon = (int)nudSoLuongTon.Value,
                DonGiaBan = nudDonGia.Value,
                MaLoai = cboNhanHieu.SelectedValue as string,
                MaNCU = cboNhaCungUng.SelectedValue as string
            };
        }

        private void DatLaiForm()
        {
            txtMaNGK.Clear();
            txtMaNGK.ReadOnly = false;
            txtTenNGK.Clear();
            cboDonViTinh.SelectedIndex = -1;
            cboDonViTinh.Text = "";
            nudSoLuongTon.Value = 0;
            nudSoLuongTon.Enabled = true;
            nudDonGia.Value = 0;
            cboNhanHieu.SelectedIndex = -1;
            cboNhaCungUng.SelectedIndex = -1;

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            dgvNGK.ClearSelection();
            txtMaNGK.Focus();
        }

        private void HienKetQua(ServiceResult kq)
        {
            lblTrangThai.Text = kq.Message;
            MessageBox.Show(kq.Message, kq.Success ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, kq.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }
    }
}
