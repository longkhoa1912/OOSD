using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmTheoDoiDonDangKy : Form
    {
        public FrmTheoDoiDonDangKy() : this(null) { }
        public FrmTheoDoiDonDangKy(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Theo dõi đơn thực tập";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Theo dõi đơn thực tập", "uc07", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
