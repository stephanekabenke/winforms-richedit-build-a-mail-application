using System;
using System.Windows.Forms;

namespace RichEditSendMail {
    static class Program {
        [STAThread]
        static void Main() {
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("WXI", "Office White");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
