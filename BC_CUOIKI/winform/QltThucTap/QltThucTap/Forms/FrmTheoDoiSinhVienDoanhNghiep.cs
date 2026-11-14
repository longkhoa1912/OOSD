using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmTheoDoiSinhVienDoanhNghiep : Form
    {
        public FrmTheoDoiSinhVienDoanhNghiep() : this(null) { }
        public FrmTheoDoiSinhVienDoanhNghiep(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Theo dõi sinh viên";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Theo dõi sinh viên", "uc10", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
