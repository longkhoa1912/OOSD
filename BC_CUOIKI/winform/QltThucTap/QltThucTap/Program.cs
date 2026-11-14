using QltThucTap.WinForms.Forms;
using System;
using System.Windows.Forms;

namespace QltThucTap.WinForms
{
internal static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FrmMain());
    }
}
}
