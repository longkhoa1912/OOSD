using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmDangKyTaiKhoan : Form
    {
        public FrmDangKyTaiKhoan() : this(null) { }
        public FrmDangKyTaiKhoan(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Đăng ký tài khoản";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Đăng ký tài khoản", "uc02", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
