using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmQuanLyGiangVien : Form
    {
        public FrmQuanLyGiangVien() : this(null) { }
        public FrmQuanLyGiangVien(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Quản lý giảng viên";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Quản lý giảng viên", "uc19", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
