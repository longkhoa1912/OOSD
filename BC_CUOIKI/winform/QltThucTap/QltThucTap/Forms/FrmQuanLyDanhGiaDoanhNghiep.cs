using QltThucTap.WinForms.Models;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QltThucTap.WinForms.Forms
{
    [DesignerCategory("Form")]
    public partial class FrmQuanLyDanhGiaDoanhNghiep : Form
    {
        public FrmQuanLyDanhGiaDoanhNghiep() : this(null) { }
        public FrmQuanLyDanhGiaDoanhNghiep(UserSessionDto currentUser)
        {
            InitializeComponent();
            Text = "Quản lý đánh giá doanh nghiệp";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(760, 520);
            var screen = new FrmUseCaseTool("Quản lý đánh giá doanh nghiệp", "uc23", currentUser) { Dock = DockStyle.Fill };
            Controls.Add(screen);
        }
    }
}
