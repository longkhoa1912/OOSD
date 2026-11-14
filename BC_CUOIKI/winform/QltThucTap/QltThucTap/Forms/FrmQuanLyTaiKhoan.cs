using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmQuanLyTaiKhoan : Form
    {
        public FrmQuanLyTaiKhoan() : this(null) { }
        public FrmQuanLyTaiKhoan(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Quản lý tài khoản";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Quản lý tài khoản", "uc17", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
