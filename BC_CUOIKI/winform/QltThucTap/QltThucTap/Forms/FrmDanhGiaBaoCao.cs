using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmDanhGiaBaoCao : Form
    {
        public FrmDanhGiaBaoCao() : this(null) { }
        public FrmDanhGiaBaoCao(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Đánh giá báo cáo";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Đánh giá báo cáo", "uc14", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
