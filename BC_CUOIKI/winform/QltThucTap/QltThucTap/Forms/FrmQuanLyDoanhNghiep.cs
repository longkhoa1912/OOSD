using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmQuanLyDoanhNghiep : Form
    {
        public FrmQuanLyDoanhNghiep() : this(null) { }
        public FrmQuanLyDoanhNghiep(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Quản lý doanh nghiệp";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Quản lý doanh nghiệp", "uc18", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
