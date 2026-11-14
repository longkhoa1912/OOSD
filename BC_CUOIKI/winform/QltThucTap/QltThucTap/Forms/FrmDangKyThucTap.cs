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
public sealed class FrmDangKyThucTap : Form
{
    private readonly UserSessionDto user;
    private readonly ComboBox cboViTri = new ComboBox();
    private readonly RichTextBox rtbThuGioiThieu = new RichTextBox();
    private readonly Button btnNopDon = new Button();

    public FrmDangKyThucTap(UserSessionDto currentUser)
    {
        user = currentUser;

        Text = "Đăng ký thực tập";
        ClientSize = new Size(600, 430);

        cboViTri.Dock = DockStyle.Top;
        cboViTri.DropDownStyle = ComboBoxStyle.DropDownList;

        rtbThuGioiThieu.Dock = DockStyle.Fill;

        btnNopDon.Text = "Nộp đơn";
        btnNopDon.AutoSize = true;
        btnNopDon.Click += async (sender, args) => await NopDonAsync();

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            ColumnCount = 1,
            RowCount = 4
        };
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        layout.Controls.Add(new Label
        {
            Text = "Chọn vị trí thực tập",
            AutoSize = true
        }, 0, 0);
        layout.Controls.Add(cboViTri, 0, 1);
        layout.Controls.Add(new Label
        {
            Text = "Thư giới thiệu",
            AutoSize = true
        }, 0, 2);
        layout.Controls.Add(rtbThuGioiThieu, 0, 3);

        var pnlButton = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 48,
            FlowDirection = FlowDirection.RightToLeft
        };
        pnlButton.Controls.Add(btnNopDon);

        Controls.Add(layout);
        Controls.Add(pnlButton);

        Load += async (sender, args) => await TaiViTriAsync();
    }

    private async Task TaiViTriAsync()
    {
        try
        {
            var result = await ApiClient.GetAsync<List<ViTriDto>>(
                "/api/vitri/danh-sach");

            if (!result.Success || result.Data is null)
            {
                MessageBox.Show(result.Message ?? "Không tải được vị trí.");
                return;
            }

            var dangTuyen = result.Data
                .Where(v => v.TrangThai == "DangTuyen")
                .ToList();

            cboViTri.DataSource = dangTuyen;
            cboViTri.DisplayMember = "TenViTri";
            cboViTri.ValueMember = "MaVT";
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Lỗi kết nối: {ex.Message}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải vị trí: {ex.Message}");
        }
    }

    private async Task NopDonAsync()
    {
        var viTri = cboViTri.SelectedItem as ViTriDto;
        if (viTri == null)
        {
            MessageBox.Show("Vui lòng chọn vị trí thực tập.");
            return;
        }

        try
        {
            var result = await ApiClient.PostAsync<object>(
                "/api/dondangky/tao",
                new
                {
                    MaTKSinhVien = user.MaTK,
                    MaViTri = viTri.MaVT,
                    ThuGioiThieu = rtbThuGioiThieu.Text.Trim()
                });

            MessageBox.Show(
                result.Message ?? (result.Success ? "Nộp đơn thành công." : "Nộp đơn thất bại."),
                "Đăng ký thực tập",
                MessageBoxButtons.OK,
                result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Lỗi kết nối: {ex.Message}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi nộp đơn: {ex.Message}");
        }
    }
}
}
