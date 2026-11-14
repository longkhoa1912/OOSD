using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmTimKiemDoanhNghiep : Form
    {
        public FrmTimKiemDoanhNghiep() : this(null) { }
        public FrmTimKiemDoanhNghiep(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Tìm kiếm doanh nghiệp";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Tìm kiếm doanh nghiệp", "uc04", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
