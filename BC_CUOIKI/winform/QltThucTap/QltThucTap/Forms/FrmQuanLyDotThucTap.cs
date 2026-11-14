using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmQuanLyDotThucTap : Form
    {
        public FrmQuanLyDotThucTap() : this(null) { }
        public FrmQuanLyDotThucTap(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Quản lý đợt thực tập";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Quản lý đợt thực tập", "uc20", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
