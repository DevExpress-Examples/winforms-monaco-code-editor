using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;

namespace CodeEditorExample {
    internal static class Program {
        [STAThread]
        static void Main() {
            WindowsFormsSettings.SetPerMonitorDpiAware();
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
