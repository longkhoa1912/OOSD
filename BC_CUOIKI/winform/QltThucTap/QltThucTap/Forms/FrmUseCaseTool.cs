using Newtonsoft.Json;
using QltThucTap.WinForms.Api;
using QltThucTap.WinForms.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    // Shared parameter/result screen for UC endpoints whose dedicated form is not present yet.
    public class FrmUseCaseTool : UserControl
    {
        private sealed class ChoiceItem
        {
            public string Text { get; }
            public string Value { get; }
            public ChoiceItem(string text, string value) { Text = text; Value = value; }
            public override string ToString() => Text;
        }

        private readonly string route;
        private readonly UserSessionDto user;
        private readonly Dictionary<string, Control> editors = new Dictionary<string, Control>();
        private readonly Dictionary<string, Panel> fieldPanels = new Dictionary<string, Panel>();
        private readonly DataGridView grid = new DataGridView();
        private readonly Label status = new Label();
        private readonly TextBox confirmPassword = new TextBox();
        private readonly Panel confirmPasswordPanel = new Panel();
        private readonly bool registration;
        private bool bindingResults;

        private static readonly Dictionary<string, string[]> Fields = new Dictionary<string, string[]>
        {
            ["uc02"] = new[] { "TenDangNhap", "MatKhauHash", "VaiTro", "Email", "MaSoSinhVien", "MaSoGiangVien", "HoTen", "Lop", "Nganh", "TenDoanhNghiep", "LinhVuc", "DiaChi", "SoDienThoai" },
            ["uc03-capnhat"] = new[] { "HoTen", "Lop", "Nganh", "SoDienThoai", "DiaChi", "Email" },
            ["uc04"] = new[] { "TuKhoa" },
            ["uc05"] = new[] { "TuKhoa", "MaDot", "ChiDangTuyen (1/0)" },
            ["uc07"] = Array.Empty<string>(),
            ["uc08"] = new[] { "HanhDong (DANHSACH/TAO/CAPNHAT/NGUNG_TUYEN/MO_TUYEN)", "MaViTri", "MaDot", "TenViTri", "MoTa", "YeuCau", "SoLuong", "DiaDiem" },
            ["uc10"] = Array.Empty<string>(),
            ["uc11"] = new[] { "MaThucTap", "Diem", "NhanXet" },
            ["uc12"] = Array.Empty<string>(),
            ["uc13"] = new[] { "MaThucTap (để trống = tất cả)" },
            ["uc14"] = new[] { "MaBaoCao", "Diem", "NhanXet" },
            ["uc15"] = new[] { "MaThucTap", "NgayGhi (yyyy-MM-dd HH:mm:ss)", "NoiDung" },
            ["uc16"] = new[] { "MaThucTap", "TieuDe", "NoiDung", "DuongDanTep" },
            ["uc17"] = new[] { "HanhDong (DANHSACH/TAO/CAPNHAT/KHOA/MOKHOA/XOA)", "MaTK", "TenDangNhap", "MatKhauHash", "VaiTro", "Email" },
            ["uc18"] = new[] { "HanhDong (DANHSACH/TAO/CAPNHAT/KHOA/MOKHOA/XOA)", "MaDN", "MaTK", "MaSoThue", "TenDoanhNghiep", "LinhVuc", "DiaChi", "SoDienThoai", "Website", "MoTa" },
            ["uc19"] = new[] { "HanhDong (DANHSACH/TAO/CAPNHAT/KHOA/MOKHOA/XOA)", "MaGV", "MaTK", "MaSoGiangVien", "HoTen", "Khoa", "SoDienThoai" },
            ["uc20"] = new[] { "HanhDong (DANHSACH/TAO/CAPNHAT/DOITRANGTHAI/XOA)", "MaDot", "TenDot", "NgayBatDau", "NgayKetThuc", "HanDangKy", "TrangThai", "MoTa" },
            ["uc21"] = new[] { "MaDot (để trống = tất cả)" },
            ["uc22"] = new[] { "MaThucTap", "Diem", "NhanXet" },
            ["uc23"] = new[] { "HanhDong (DANHSACH/CAPNHAT/XOA)", "MaDGDN", "Diem", "NhanXet", "TuKhoa" }
        };

        private static readonly HashSet<string> AutoLoadRoutes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "uc04", "uc05", "uc07", "uc08", "uc10", "uc12", "uc13",
            "uc17", "uc18", "uc19", "uc20", "uc21", "uc23"
        };

        public FrmUseCaseTool(string title, string useCase, UserSessionDto currentUser)
        {
            route = useCase;
            user = currentUser;
            registration = useCase == "uc02";
            Text = title;
            Size = new Size(920, 680);
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(244, 247, 251);

            var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 1, RowCount = 4 };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, registration ? 100 : 48));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, registration ? 0 : 52));
            status.Text = registration
                ? "Tạo tài khoản mới — chọn vai trò để nhập hồ sơ tương ứng."
                : "Nhập thông tin cần thiết rồi chọn Thực hiện.";
            if (IsAdminManagementRoute(useCase))
                status.Text = "Danh sách tự tải. Chọn một dòng để sửa hoặc xóa, rồi chọn thao tác.";
            status.Dock = DockStyle.Fill;
            status.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            status.ForeColor = Color.FromArgb(30, 52, 86);
            root.Controls.Add(status, 0, 0);

            var fieldsPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, WrapContents = true, Padding = new Padding(8), BackColor = Color.White };
            foreach (string field in Fields[useCase])
            {
                string key = field.Split(' ')[0];
                bool longText = key == "MoTa" || key == "YeuCau" || key == "NoiDung" || key == "NhanXet" || key == "DiaChi";
                var item = new Panel { Width = 270, Height = longText ? 104 : 66, Margin = new Padding(6), BackColor = Color.White, Tag = key };
                var label = new Label { Text = LabelFor(key, useCase), Dock = DockStyle.Top, Height = 24, AutoEllipsis = true, ForeColor = Color.FromArgb(63, 76, 96), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
                Control editor = CreateEditor(key, useCase);
                editor.Dock = DockStyle.Bottom;
                editor.Height = longText ? 76 : 30;
                if (key == "MatKhauHash" && editor is TextBox passwordBox) passwordBox.UseSystemPasswordChar = true;
                item.Controls.Add(editor);
                item.Controls.Add(label);
                fieldsPanel.Controls.Add(item);
                editors[key] = editor;
                fieldPanels[key] = item;
            }
            if (registration)
            {
                confirmPasswordPanel.Width = 270;
                confirmPasswordPanel.Height = 66;
                confirmPasswordPanel.Margin = new Padding(6);
                confirmPasswordPanel.BackColor = Color.White;
                confirmPassword.UseSystemPasswordChar = true;
                confirmPassword.Dock = DockStyle.Bottom;
                confirmPassword.Height = 30;
                confirmPasswordPanel.Controls.Add(confirmPassword);
                confirmPasswordPanel.Controls.Add(new Label { Text = "Nhập lại mật khẩu", Dock = DockStyle.Top, Height = 24, ForeColor = Color.FromArgb(63, 76, 96), Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
                fieldsPanel.Controls.Add(confirmPasswordPanel);
            }
            root.Controls.Add(fieldsPanel, 0, 1);
            if (registration)
            {
                ((ComboBox)editors["VaiTro"]).SelectedIndexChanged += (s, e) => UpdateRegistrationFields();
                ((ComboBox)editors["VaiTro"]).SelectedIndex = 0;
            }

            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
            var runText = registration ? "Tạo tài khoản"
                : useCase == "uc03-capnhat" ? "Lưu hồ sơ"
                : (useCase == "uc04" || useCase == "uc05") ? "Tìm kiếm"
                : IsAdminManagementRoute(useCase) ? "Thực hiện thao tác"
                : AutoLoadRoutes.Contains(useCase) ? "Tải lại"
                : "Thực hiện";
            var run = new Button { Text = runText, Width = 150, Height = 36, BackColor = Color.FromArgb(36, 93, 173), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand };
            run.FlatAppearance.BorderSize = 0;
            run.Click += async (s, e) => await ExecuteAsync();
            actions.Controls.Add(run);
            root.Controls.Add(actions, 0, 2);

            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AllowUserToAddRows = false;
            grid.RowHeadersVisible = false;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 53, 88);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 230, 250);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 42, 60);
            grid.SelectionChanged += (sender, args) => PopulateEditorsFromSelectedRow();
            grid.Visible = !registration;
            root.Controls.Add(grid, 0, 3);
            Controls.Add(root);

            SetIdentity("MaTKSinhVien", "SinhVien");
            SetIdentity("MaTKDoanhNghiep", "DoanhNghiep");
            SetIdentity("MaTKGiangVien", "GiangVien");
            SetIdentity("MaTKAdmin", "Admin");
            if (useCase == "uc03-capnhat" && user != null)
                editors["MaTK"] = new TextBox { Text = user.MaTK.ToString(CultureInfo.InvariantCulture) };

            Load += async (sender, args) =>
            {
                if (useCase == "uc03-capnhat")
                    await LoadStudentProfileAsync();
                else if (AutoLoadRoutes.Contains(useCase))
                    await ExecuteAsync();
            };
        }

        private void SetIdentity(string key, string role)
        {
            if (user != null && string.Equals(user.VaiTro, role, StringComparison.OrdinalIgnoreCase))
                editors[key] = new TextBox { Text = user.MaTK.ToString(CultureInfo.InvariantCulture) };
        }

        private static Control CreateEditor(string key, string useCase)
        {
            var combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            if (key == "VaiTro")
            {
                combo.Items.AddRange(new object[] {
                    new ChoiceItem("Sinh viên", "SinhVien"),
                    new ChoiceItem("Giảng viên", "GiangVien"),
                    new ChoiceItem("Doanh nghiệp", "DoanhNghiep")
                });
                return combo;
            }
            if (key == "ChiDangTuyen")
            {
                combo.Items.AddRange(new object[]
                {
                    new ChoiceItem("Chỉ vị trí đang tuyển", "1"),
                    new ChoiceItem("Tất cả trạng thái", "0")
                });
                combo.SelectedIndex = 0;
                return combo;
            }
            if (key == "HanhDong")
            {
                string[][] actions = useCase == "uc23"
                    ? new[] { new[] { "Xem danh sách", "DANHSACH" }, new[] { "Cập nhật đánh giá", "CAPNHAT" }, new[] { "Xóa đánh giá", "XOA" } }
                    : useCase == "uc08"
                    ? new[] { new[] { "Xem danh sách", "DANHSACH" }, new[] { "Tạo vị trí", "TAO" }, new[] { "Cập nhật vị trí", "CAPNHAT" }, new[] { "Ngừng tuyển", "NGUNG_TUYEN" }, new[] { "Mở tuyển", "MO_TUYEN" } }
                    : useCase == "uc20"
                        ? new[] { new[] { "Xem danh sách", "DANHSACH" }, new[] { "Tạo đợt", "TAO" }, new[] { "Cập nhật đợt", "CAPNHAT" }, new[] { "Đổi trạng thái", "DOITRANGTHAI" }, new[] { "Xóa đợt", "XOA" } }
                        : new[] { new[] { "Xem danh sách", "DANHSACH" }, new[] { "Tạo mới", "TAO" }, new[] { "Cập nhật", "CAPNHAT" }, new[] { "Khóa tài khoản", "KHOA" }, new[] { "Mở khóa", "MOKHOA" }, new[] { "Xóa / ngừng hoạt động", "XOA" } };
                foreach (string[] action in actions) combo.Items.Add(new ChoiceItem(action[0], action[1]));
                combo.SelectedIndex = 0;
                return combo;
            }
            if (key == "MoTa" || key == "YeuCau" || key == "NoiDung" || key == "NhanXet" || key == "DiaChi")
                return new RichTextBox { BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9F) };
            return new TextBox { BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9F) };
        }

        private static string LabelFor(string key, string useCase)
        {
            switch (key)
            {
                case "TenDangNhap": return "Tên đăng nhập";
                case "MatKhauHash": return useCase == "uc02" ? "Mật khẩu" : "Mật khẩu";
                case "VaiTro": return "Loại tài khoản";
                case "Email": return "Email";
                case "MaSoSinhVien": return "Mã số sinh viên";
                case "MaSoGiangVien": return "Mã số giảng viên";
                case "HoTen": return "Họ và tên";
                case "Lop": return "Lớp";
                case "Nganh": return "Ngành / Khoa";
                case "TenDoanhNghiep": return "Tên doanh nghiệp";
                case "LinhVuc": return "Lĩnh vực hoạt động";
                case "DiaChi": return "Địa chỉ";
                case "SoDienThoai": return "Số điện thoại";
                case "TuKhoa": return "Từ khóa tìm kiếm";
                case "MaDot": return "Mã đợt thực tập";
                case "ChiDangTuyen": return "Chỉ xem vị trí đang tuyển (1/0)";
                case "MaViTri": return "Mã vị trí thực tập";
                case "TenViTri": return "Tên vị trí";
                case "MoTa": return "Mô tả";
                case "YeuCau": return "Yêu cầu ứng tuyển";
                case "SoLuong": return "Số lượng tuyển";
                case "MaThucTap": return "Mã hồ sơ thực tập";
                case "MaBaoCao": return "Mã báo cáo";
                case "Diem": return "Điểm (0–10)";
                case "NhanXet": return "Nhận xét";
                case "NgayGhi": return "Ngày ghi (yyyy-MM-dd HH:mm:ss)";
                case "NoiDung": return "Nội dung";
                case "TieuDe": return "Tiêu đề";
                case "DuongDanTep": return "Đường dẫn tệp";
                case "HanhDong": return "Thao tác";
                case "MaTK": return "Mã tài khoản";
                case "MaDN": return "Mã doanh nghiệp";
                case "MaDGDN": return "Mã đánh giá doanh nghiệp";
                case "MaGV": return "Mã giảng viên";
                case "MaSoThue": return "Mã số thuế";
                case "Khoa": return "Khoa";
                case "Website": return "Website";
                case "NgayBatDau": return "Ngày bắt đầu";
                case "NgayKetThuc": return "Ngày kết thúc";
                case "HanDangKy": return "Hạn đăng ký";
                case "TrangThai": return "Trạng thái";
                default: return key;
            }
        }

        private void UpdateRegistrationFields()
        {
            string role = ((ChoiceItem)((ComboBox)editors["VaiTro"]).SelectedItem).Value;
            foreach (var pair in fieldPanels) pair.Value.Visible = true;
            confirmPasswordPanel.Visible = true;
            string[] studentFields = { "MaSoSinhVien", "HoTen", "Lop", "Nganh", "SoDienThoai", "DiaChi" };
            string[] lecturerFields = { "MaSoGiangVien", "HoTen", "Nganh", "SoDienThoai" };
            string[] companyFields = { "TenDoanhNghiep", "LinhVuc", "DiaChi", "SoDienThoai" };
            var profileFields = role == "SinhVien" ? studentFields : role == "GiangVien" ? lecturerFields : companyFields;
            string[] accountFields = { "TenDangNhap", "MatKhauHash", "VaiTro", "Email" };
            foreach (var pair in fieldPanels)
                pair.Value.Visible = accountFields.Contains(pair.Key) || profileFields.Contains(pair.Key);
            foreach (var pair in fieldPanels)
            {
                if (pair.Key == "Nganh") pair.Value.Controls.OfType<Label>().First().Text = role == "GiangVien" ? "Khoa" : "Ngành";
            }
            status.Text = role == "SinhVien" ? "Tạo tài khoản sinh viên" : role == "GiangVien" ? "Tạo tài khoản giảng viên" : "Tạo tài khoản doanh nghiệp";
        }

        private async Task ExecuteAsync()
        {
            if (registration && editors["MatKhauHash"].Text != confirmPassword.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại chưa khớp.", "Kiểm tra thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var body = new Dictionary<string, object>();
            string actionValue = null;
            foreach (var pair in editors)
            {
                string value = pair.Value is ComboBox combo
                    ? (combo.SelectedItem is ChoiceItem choice ? choice.Value : Convert.ToString(combo.SelectedItem))
                    : pair.Value.Text.Trim();
                if (pair.Key == "HanhDong") actionValue = value;
                if (string.IsNullOrEmpty(value)) { body[pair.Key] = null; continue; }
                if ((pair.Key.StartsWith("Ma", StringComparison.Ordinal) && pair.Key != "MaSoSinhVien" && pair.Key != "MaSoGiangVien" && pair.Key != "MaSoThue") || pair.Key == "SoLuong")
                {
                    if (int.TryParse(value, out int number)) body[pair.Key] = number;
                    else { MessageBox.Show("Trường " + pair.Key + " phải là số nguyên."); return; }
                }
                else if (pair.Key == "Diem")
                {
                    if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal score)) body[pair.Key] = score;
                    else { MessageBox.Show("Điểm phải là số từ 0 đến 10."); return; }
                }
                else if (pair.Key.StartsWith("ChiDangTuyen", StringComparison.Ordinal)) body["ChiDangTuyen"] = value != "0";
                else body[pair.Key] = value;
            }

            if (actionValue == "XOA")
            {
                var confirm = MessageBox.Show(
                    "Bạn có chắc muốn xóa hoặc ngừng hoạt động bản ghi đã chọn?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;
            }

            try
            {
                var result = await ApiClient.PostAsync<List<Dictionary<string, object>>>("/api/uc/" + route, body);
                status.Text = result.Message ?? (result.Success ? "Đã xử lý." : "Yêu cầu thất bại.");
                status.ForeColor = result.Success ? Color.DarkGreen : Color.DarkRed;
                var rows = result.Data;
                bindingResults = true;
                try
                {
                    grid.DataSource = rows == null || rows.Count == 0 ? null : ToDataTable(rows);
                }
                finally
                {
                    bindingResults = false;
                }
                if (result.Success && !registration && (rows == null || rows.Count == 0))
                    status.Text = (result.Message ?? "Đã xử lý.") + " Không có dữ liệu phù hợp.";
                if (result.Success && IsAdminManagementRoute(route) &&
                    (actionValue == "TAO" || actionValue == "CAPNHAT" || actionValue == "XOA" || actionValue == "KHOA" || actionValue == "MOKHOA" || actionValue == "DOITRANGTHAI"))
                {
                    ComboBox actionEditor;
                    if (editors.TryGetValue("HanhDong", out var editor) && (actionEditor = editor as ComboBox) != null)
                    {
                        var listChoice = actionEditor.Items.Cast<ChoiceItem>().FirstOrDefault(item => item.Value == "DANHSACH");
                        if (listChoice != null) actionEditor.SelectedItem = listChoice;
                        await ExecuteAsync();
                    }
                }
                if (registration && result.Success)
                {
                    MessageBox.Show("Đăng ký tài khoản thành công. Bạn có thể đăng nhập ngay.", "Đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FindForm()?.Close();
                }
            }
            catch (HttpRequestException ex) { MessageBox.Show("Không kết nối được backend: " + ex.Message); }
            catch (Exception ex) { MessageBox.Show("Lỗi xử lý: " + ex.Message); }
        }

        private bool IsAdminManagementRoute(string useCase)
        {
            return useCase == "uc17" || useCase == "uc18" || useCase == "uc19" ||
                   useCase == "uc20" || useCase == "uc23";
        }

        private void PopulateEditorsFromSelectedRow()
        {
            if (bindingResults || !IsAdminManagementRoute(route) || grid.CurrentRow == null)
                return;

            var rowView = grid.CurrentRow.DataBoundItem as DataRowView;
            if (rowView == null)
                return;

            foreach (var pair in editors)
            {
                if (pair.Key == "HanhDong" || !rowView.Row.Table.Columns.Contains(pair.Key) || pair.Value is ComboBox)
                    continue;

                var value = rowView[pair.Key];
                if (value == null || value == DBNull.Value)
                    pair.Value.Text = string.Empty;
                else if (value is DateTime date)
                    pair.Value.Text = date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                else
                    pair.Value.Text = Convert.ToString(value, CultureInfo.InvariantCulture);
            }

            status.Text = "Đã chọn dòng. Chọn Cập nhật để sửa hoặc Xóa / ngừng hoạt động.";
            status.ForeColor = Color.FromArgb(30, 52, 86);
        }

        private async Task LoadStudentProfileAsync()
        {
            if (user == null)
            {
                status.Text = "Hãy đăng nhập bằng tài khoản sinh viên để xem hồ sơ.";
                status.ForeColor = Color.DarkRed;
                return;
            }

            try
            {
                var body = new Dictionary<string, object> { ["MaTK"] = user.MaTK };
                var result = await ApiClient.PostAsync<List<Dictionary<string, object>>>("/api/uc/uc03-xem", body);
                if (!result.Success || result.Data == null || result.Data.Count == 0)
                {
                    status.Text = result.Message ?? "Không tải được hồ sơ sinh viên.";
                    status.ForeColor = Color.DarkRed;
                    return;
                }

                var profile = result.Data[0];
                foreach (var field in new[] { "HoTen", "Lop", "Nganh", "SoDienThoai", "DiaChi", "Email" })
                {
                    Control editor;
                    object value;
                    if (editors.TryGetValue(field, out editor) && profile.TryGetValue(field, out value))
                        editor.Text = value == null ? string.Empty : Convert.ToString(value, CultureInfo.CurrentCulture);
                }

                status.Text = "Đã tải hồ sơ. Chỉnh sửa thông tin rồi chọn Lưu hồ sơ.";
                status.ForeColor = Color.DarkGreen;
            }
            catch (HttpRequestException ex)
            {
                status.Text = "Không kết nối được backend: " + ex.Message;
                status.ForeColor = Color.DarkRed;
            }
            catch (Exception ex)
            {
                status.Text = "Lỗi tải hồ sơ: " + ex.Message;
                status.ForeColor = Color.DarkRed;
            }
        }

        private static DataTable ToDataTable(List<Dictionary<string, object>> rows)
        {
            var table = new DataTable();
            if (rows == null || rows.Count == 0)
                return table;

            var columns = rows
                .Where(row => row != null)
                .SelectMany(row => row.Keys)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            foreach (var column in columns)
                table.Columns.Add(column, typeof(object));

            foreach (var row in rows)
            {
                if (row == null)
                    continue;

                var dataRow = table.NewRow();
                foreach (var column in columns)
                {
                    var pair = row.FirstOrDefault(item =>
                        string.Equals(item.Key, column, StringComparison.OrdinalIgnoreCase));
                    dataRow[column] = pair.Value ?? DBNull.Value;
                }
                table.Rows.Add(dataRow);
            }

            return table;
        }
    }
}
