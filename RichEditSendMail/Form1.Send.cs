using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace RichEditSendMail
{
    // Validierung der Eingaben und Versand der Mail per SMTP.
    public partial class Form1
    {
        private void btnSend_Click(object sender, EventArgs e)
        {
            string fromAddress = (edtFrom.Text ?? string.Empty).Trim();
            string smtpServer = (edtSmtpServer.Text ?? string.Empty).Trim();
            string smtpPortText = (edtSmtpPort.Text ?? string.Empty).Trim();
            string smtpPassword = edtSmtpPassword.Text ?? string.Empty;
            string toRaw = edtTo.EditValue == null ? string.Empty : edtTo.EditValue.ToString();
            string ccRaw = edtCc.EditValue == null ? string.Empty : edtCc.EditValue.ToString();

            if (string.IsNullOrEmpty(fromAddress))
            {
                XtraMessageBox.Show("Bitte deine Absender-Adresse im Feld 'Von' eintragen.");
                return;
            }
            if (string.IsNullOrWhiteSpace(toRaw))
            {
                XtraMessageBox.Show("Bitte mindestens einen Empfänger im Feld 'An' eintragen.");
                return;
            }
            if (edtSubject.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("Bitte einen Betreff eintragen.");
                return;
            }
            if (string.IsNullOrEmpty(smtpServer))
            {
                XtraMessageBox.Show("Bitte SMTP-Server angeben.");
                return;
            }
            if (!int.TryParse(smtpPortText, out int smtpPort) || smtpPort <= 0)
            {
                XtraMessageBox.Show("Bitte gültigen SMTP-Port angeben.");
                return;
            }
            if (string.IsNullOrEmpty(smtpPassword))
            {
                XtraMessageBox.Show("Bitte App-Passwort eintragen.");
                return;
            }

            try
            {
                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(fromAddress);
                    foreach (var addr in SplitAddresses(toRaw))
                        mailMessage.To.Add(addr);
                    foreach (var addr in SplitAddresses(ccRaw))
                        mailMessage.CC.Add(addr);
                    mailMessage.Subject = edtSubject.Text;

                    var exporter = new RichEditMailMessageExporter(richEdit, mailMessage);
                    exporter.Export();

                    foreach (var path in _attachmentPaths)
                    {
                        if (File.Exists(path))
                        {
                            mailMessage.Attachments.Add(new Attachment(path));
                        }
                    }

                    using (var client = new SmtpClient(smtpServer, smtpPort))
                    {
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(fromAddress, smtpPassword);
                        client.Send(mailMessage);
                    }

                    XtraMessageBox.Show("Mail gesendet.", "Mail Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exc)
            {
                XtraMessageBox.Show("Senden fehlgeschlagen: " + exc.Message);
            }
        }

        private static IEnumerable<string> SplitAddresses(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) yield break;
            foreach (var part in s.Split(new[] { ';', ',' }))
            {
                var trimmed = part.Trim();
                if (!string.IsNullOrEmpty(trimmed)) yield return trimmed;
            }
        }
    }
}
