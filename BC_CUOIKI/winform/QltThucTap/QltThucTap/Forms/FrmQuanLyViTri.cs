using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmQuanLyViTri : Form
    {
        public FrmQuanLyViTri() : this(null) { }
        public FrmQuanLyViTri(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Quản lý vị trí thực tập";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Quản lý vị trí thực tập", "uc08", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
