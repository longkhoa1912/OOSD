using QltThucTap.WinForms.Api;
using QltThucTap.WinForms.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
public sealed class FrmDanhSachViTri : Form
{
    private readonly DataGridView dgvViTri = new DataGridView();
    private readonly TextBox txtTuKhoa = new TextBox();
    private readonly Button btnTimKiem = new Button();
    private readonly Button btnLamMoi = new Button();

    private List<ViTriDto> tatCaViTri = new List<ViTriDto>();

    public FrmDanhSachViTri()
    {
        Text = "Danh sách vị trí thực tập";

        var pnlTop = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 45,
            Padding = new Padding(8)
        };

        txtTuKhoa.Width = 240;
        btnTimKiem.Text = "Tìm kiếm";
        btnLamMoi.Text = "Làm mới";

        btnTimKiem.Click += (sender, args) => LocDanhSach();
        btnLamMoi.Click += async (sender, args) => await TaiDanhSachAsync();

        pnlTop.Controls.Add(new Label
        {
            Text = "Từ khóa:",
            AutoSize = true,
            Padding = new Padding(0, 7, 0, 0)
        });
        pnlTop.Controls.Add(txtTuKhoa);
        pnlTop.Controls.Add(btnTimKiem);
        pnlTop.Controls.Add(btnLamMoi);

        dgvViTri.Dock = DockStyle.Fill;
        dgvViTri.ReadOnly = true;
        dgvViTri.AutoGenerateColumns = true;
        dgvViTri.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvViTri.MultiSelect = false;

        Controls.Add(dgvViTri);
        Controls.Add(pnlTop);

        Load += async (sender, args) => await TaiDanhSachAsync();
    }

    private async Task TaiDanhSachAsync()
    {
        try
        {
            var result = await ApiClient.GetAsync<List<ViTriDto>>(
                "/api/vitri/danh-sach");

            if (!result.Success || result.Data is null)
            {
                MessageBox.Show(result.Message ?? "Không tải được danh sách vị trí.");
                return;
            }

            tatCaViTri = result.Data;
            dgvViTri.DataSource = tatCaViTri.ToList();
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Lỗi kết nối: {ex.Message}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải danh sách: {ex.Message}");
        }
    }

    private void LocDanhSach()
    {
        var keyword = txtTuKhoa.Text.Trim();

        if (string.IsNullOrEmpty(keyword))
        {
            dgvViTri.DataSource = tatCaViTri.ToList();
            return;
        }

        dgvViTri.DataSource = tatCaViTri
            .Where(v =>
                Contains(v.TenViTri, keyword) ||
                Contains(v.TenDoanhNghiep, keyword) ||
                Contains(v.LinhVuc, keyword) ||
                Contains(v.DiaDiem, keyword))
            .ToList();
    }

    private static bool Contains(string value, string keyword)
    {
        return !string.IsNullOrEmpty(value) &&
               value.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}
}
