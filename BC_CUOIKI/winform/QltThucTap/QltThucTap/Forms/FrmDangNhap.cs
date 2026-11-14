using QltThucTap.WinForms.Api;
using QltThucTap.WinForms.Models;
using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
public sealed class FrmDangNhap : Form
{
    private readonly TextBox txtTenDangNhap = new TextBox();
    private readonly TextBox txtMatKhau = new TextBox();
    private readonly Button btnDangNhap = new Button();
    private readonly Label lblThongBao = new Label();

    public FrmDangNhap()
    {
        Text = "Đăng nhập";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(390, 240);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20),
            ColumnCount = 2,
            RowCount = 5
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        txtTenDangNhap.Dock = DockStyle.Fill;
        txtMatKhau.Dock = DockStyle.Fill;
        txtMatKhau.UseSystemPasswordChar = true;

        btnDangNhap.Text = "Đăng nhập";
        btnDangNhap.AutoSize = true;
        btnDangNhap.Click += async (sender, args) => await DangNhapAsync();

        lblThongBao.AutoSize = true;
        lblThongBao.ForeColor = Color.DarkRed;

        layout.Controls.Add(new Label { Text = "Tên đăng nhập", AutoSize = true }, 0, 0);
        layout.Controls.Add(txtTenDangNhap, 1, 0);
        layout.Controls.Add(new Label { Text = "Mật khẩu", AutoSize = true }, 0, 1);
        layout.Controls.Add(txtMatKhau, 1, 1);
        layout.Controls.Add(btnDangNhap, 1, 2);
        layout.Controls.Add(lblThongBao, 0, 3);
        layout.SetColumnSpan(lblThongBao, 2);

        Controls.Add(layout);
        AcceptButton = btnDangNhap;
    }

    private async Task DangNhapAsync()
    {
        lblThongBao.Text = "";

        if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
            string.IsNullOrWhiteSpace(txtMatKhau.Text))
        {
            lblThongBao.Text = "Vui lòng nhập tên đăng nhập và mật khẩu.";
            return;
        }

        btnDangNhap.Enabled = false;

        try
        {
            var result = await ApiClient.PostAsync<UserSessionDto>(
                "/api/auth/login",
                new
                {
                    TenDangNhap = txtTenDangNhap.Text.Trim(),

                    // Backend hiện nhận trường MatKhauHash.
                    // Xem lưu ý về cách băm mật khẩu ở cuối hướng dẫn.
                    MatKhauHash = txtMatKhau.Text
                });

            if (!result.Success || result.Data is null)
            {
                lblThongBao.Text = result.Message ?? "Đăng nhập thất bại.";
                return;
            }

            AppSession.CurrentUser = result.Data;

            AppSession.CurrentUser = result.Data;
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show(
                $"Không gọi được backend: {ex.Message}",
                "Lỗi kết nối",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Lỗi đăng nhập: {ex.Message}",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            btnDangNhap.Enabled = true;
        }
    }
}
}
