using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmDanhGiaSinhVien : Form
    {
        public FrmDanhGiaSinhVien() : this(null) { }
        public FrmDanhGiaSinhVien(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Đánh giá sinh viên";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Đánh giá sinh viên", "uc11", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
