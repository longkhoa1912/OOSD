using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmDanhGiaDoanhNghiep : Form
    {
        public FrmDanhGiaDoanhNghiep() : this(null) { }
        public FrmDanhGiaDoanhNghiep(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Đánh giá doanh nghiệp";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Đánh giá doanh nghiệp", "uc22", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
