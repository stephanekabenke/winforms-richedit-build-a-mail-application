using System;
using DevExpress.XtraEditors;

namespace RichEditSendMail
{
    // SMTP-Einstellungen und Signatur laden, speichern und bearbeiten.
    public partial class Form1
    {
        private AppSettings _settings = new AppSettings();

        // Laedt gespeicherte Einstellungen in die Eingabefelder.
        private void LoadSettings()
        {
            _settings = AppSettings.Load();
            edtFrom.Text = _settings.From;
            edtSmtpServer.Text = _settings.SmtpServer;
            edtSmtpPort.Text = _settings.SmtpPort;
            edtSmtpPassword.Text = _settings.SmtpPassword;
        }

        // Uebernimmt die aktuellen SMTP-Feldwerte und speichert dauerhaft.
        private void SaveSettings()
        {
            _settings.From = edtFrom.Text ?? "";
            _settings.SmtpServer = edtSmtpServer.Text ?? "";
            _settings.SmtpPort = edtSmtpPort.Text ?? "";
            _settings.SmtpPassword = edtSmtpPassword.Text ?? "";
            try
            {
                _settings.Save();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Einstellungen konnten nicht gespeichert werden: " + ex.Message);
            }
        }

        private void btnSignature_Click(object sender, EventArgs e)
        {
            var memo = new MemoEdit();
            memo.Height = 150;

            var args = new XtraInputBoxArgs
            {
                Prompt = "Signatur (wird beim Senden ans Ende der Mail angehängt):",
                Editor = memo,
                DefaultResponse = _settings.Signature ?? ""
            };

            var result = XtraInputBox.Show(args);
            if (result == null) return;

            _settings.Signature = result.ToString();
            SaveSettings();
        }
    }
}
