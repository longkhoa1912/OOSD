using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmHoSoCaNhan : Form
    {
        public FrmHoSoCaNhan() : this(null) { }
        public FrmHoSoCaNhan(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Hồ sơ cá nhân";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Hồ sơ cá nhân", "uc03-capnhat", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
