using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmSinhVienDuocPhanCong : Form
    {
        public FrmSinhVienDuocPhanCong() : this(null) { }
        public FrmSinhVienDuocPhanCong(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Sinh viên được phân công";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Sinh viên được phân công", "uc12", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
