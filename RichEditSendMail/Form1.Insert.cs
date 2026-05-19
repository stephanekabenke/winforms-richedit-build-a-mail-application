using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.Office.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

namespace RichEditSendMail
{
    // Einfuegen von Bildern und Hyperlinks in das Dokument.
    public partial class Form1
    {
        private void btnPicture_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Bild einfügen";
                dlg.Filter = "Bilder (*.png;*.jpg;*.jpeg;*.bmp;*.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif|Alle Dateien (*.*)|*.*";
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    using (var stream = File.OpenRead(dlg.FileName))
                    {
                        var source = DocumentImageSource.FromStream(stream);
                        var pos = richEdit.Document.CaretPosition;
                        richEdit.Document.Shapes.InsertPicture(pos, source);
                    }
                    richEdit.Focus();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Bild einfügen fehlgeschlagen: " + ex.Message);
                }
            }
        }

        private void btnHyperlink_Click(object sender, EventArgs e)
        {
            try
            {
                new DevExpress.XtraRichEdit.Commands.InsertHyperlinkCommand(richEdit).Execute();
                richEdit.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Link einfügen fehlgeschlagen: " + ex.Message);
            }
        }
    }
}
