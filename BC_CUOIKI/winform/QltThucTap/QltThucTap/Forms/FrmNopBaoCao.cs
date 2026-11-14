using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmNopBaoCao : Form
    {
        public FrmNopBaoCao() : this(null) { }
        public FrmNopBaoCao(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Nộp báo cáo";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Nộp báo cáo", "uc16", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
