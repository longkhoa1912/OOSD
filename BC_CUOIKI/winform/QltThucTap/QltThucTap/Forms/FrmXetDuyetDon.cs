using QltThucTap.WinForms.Api;
using QltThucTap.WinForms.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
public sealed class FrmXetDuyetDon : Form
{
    private readonly UserSessionDto user;
    private readonly DataGridView dgvDonDangKy = new DataGridView();
    private readonly TextBox txtGhiChu = new TextBox();
    private readonly Button btnDuyet = new Button();
    private readonly Button btnTuChoi = new Button();

    public FrmXetDuyetDon(UserSessionDto currentUser)
    {
        user = currentUser;

        Text = "Xét duyệt đơn đăng ký";

        dgvDonDangKy.Dock = DockStyle.Fill;
        dgvDonDangKy.ReadOnly = true;
        dgvDonDangKy.AutoGenerateColumns = true;
        dgvDonDangKy.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDonDangKy.MultiSelect = false;

        txtGhiChu.Multiline = true;
        txtGhiChu.Dock = DockStyle.Fill;

        btnDuyet.Text = "Duyệt";
        btnTuChoi.Text = "Từ chối";

        btnDuyet.Click += async (sender, args) => await XuLyDonAsync("DaDuyet");
        btnTuChoi.Click += async (sender, args) => await XuLyDonAsync("TuChoi");

        var pnlBottom = new TableLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 105,
            Padding = new Padding(8),
            ColumnCount = 2,
            RowCount = 2
        };
        pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        pnlBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        pnlBottom.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var buttons = new FlowLayoutPanel
        {
            AutoSize = true,
            FlowDirection = FlowDirection.RightToLeft
        };
        buttons.Controls.Add(btnTuChoi);
        buttons.Controls.Add(btnDuyet);

        pnlBottom.Controls.Add(txtGhiChu, 0, 0);
        pnlBottom.SetRowSpan(txtGhiChu, 2);
        pnlBottom.Controls.Add(new Label
        {
            Text = "Ghi chú:",
            AutoSize = true
        }, 1, 0);
        pnlBottom.Controls.Add(buttons, 1, 1);

        Controls.Add(dgvDonDangKy);
        Controls.Add(pnlBottom);

        Load += async (sender, args) => await TaiDanhSachAsync();
    }

    private async Task TaiDanhSachAsync()
    {
        try
        {
            string url =
                $"/api/dondangky/danh-sach?maTKDoanhNghiep={user.MaTK}";

            var result = await ApiClient.GetAsync<List<DonDangKyDto>>(url);

            if (!result.Success || result.Data is null)
            {
                MessageBox.Show(result.Message ?? "Không tải được danh sách đơn.");
                return;
            }

            dgvDonDangKy.DataSource = result.Data;
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Lỗi kết nối: {ex.Message}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải danh sách đơn: {ex.Message}");
        }
    }

    private async Task XuLyDonAsync(string trangThaiDon)
    {
        var don = dgvDonDangKy.CurrentRow == null
            ? null
            : dgvDonDangKy.CurrentRow.DataBoundItem as DonDangKyDto;

        if (don == null)
        {
            MessageBox.Show("Vui lòng chọn một đơn đăng ký.");
            return;
        }

        string hanhDong = trangThaiDon == "DaDuyet" ? "duyệt" : "từ chối";

        var confirm = MessageBox.Show(
            $"Bạn có chắc muốn {hanhDong} đơn #{don.MaDon}?",
            "Xác nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
            return;

        try
        {
            var result = await ApiClient.PostAsync<object>(
                "/api/dondangky/duyet",
                new
                {
                    MaTKDoanhNghiep = user.MaTK,
                    MaDon = don.MaDon,
                    TrangThaiDon = trangThaiDon,
                    GhiChu = txtGhiChu.Text.Trim()
                });

            MessageBox.Show(
                result.Message ?? (result.Success ? "Đã xử lý đơn." : "Xử lý đơn thất bại."),
                "Xét duyệt đơn",
                MessageBoxButtons.OK,
                result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (result.Success)
                await TaiDanhSachAsync();
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Lỗi kết nối: {ex.Message}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xử lý đơn: {ex.Message}");
        }
    }
}
}
