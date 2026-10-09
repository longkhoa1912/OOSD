using System;
using System.Windows.Forms;
using QuanLyTourDuLich.GUI;

namespace QuanLyTourDuLich
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLapPhieuDangKyDoan());
        }
    }
}
