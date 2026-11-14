using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmTheoDoiTienDo : Form
    {
        public FrmTheoDoiTienDo() : this(null) { }
        public FrmTheoDoiTienDo(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Theo dõi tiến độ";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Theo dõi tiến độ", "uc13", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
