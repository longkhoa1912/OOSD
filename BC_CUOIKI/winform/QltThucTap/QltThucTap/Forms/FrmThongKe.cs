using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmThongKe : Form
    {
        public FrmThongKe() : this(null) { }
        public FrmThongKe(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Thống kê";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Thống kê", "uc21", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
