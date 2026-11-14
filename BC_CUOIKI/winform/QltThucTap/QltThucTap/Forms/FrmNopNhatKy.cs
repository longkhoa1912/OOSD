using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmNopNhatKy : Form
    {
        public FrmNopNhatKy() : this(null) { }
        public FrmNopNhatKy(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Nộp nhật ký";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Nộp nhật ký", "uc15", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
