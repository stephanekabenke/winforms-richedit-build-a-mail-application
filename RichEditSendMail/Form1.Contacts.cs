using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraEditors;

namespace RichEditSendMail
{
    // Kontaktliste und Token-Felder (An / CC) inkl. Validierung.
    public partial class Form1
    {
        // Muster-Kontaktliste
        private readonly List<string> _contacts = new List<string>
        {
            "anna.mueller@example.com",
            "bernd.schmidt@example.com",
            "carl.weber@example.com",
            "diana.fischer@example.com",
            "erik.bauer@example.com",
            "franziska.schulz@example.com",
            "gerd.koch@example.com",
            "hanna.richter@example.com",
            "ingo.wolf@example.com",
            "juliane.zimmermann@example.com",
            "klaus.lange@example.com",
            "lisa.hartmann@example.com"
        };

        private void InitializeContacts()
        {
            foreach (var c in _contacts)
            {
                edtTo.Properties.Tokens.Add(new DevExpress.XtraEditors.TokenEditToken(c, c));
                edtCc.Properties.Tokens.Add(new DevExpress.XtraEditors.TokenEditToken(c, c));
            }

            edtTo.Properties.CustomDrawTokenBackground += TokenBackground_CustomDraw;
            edtCc.Properties.CustomDrawTokenBackground += TokenBackground_CustomDraw;

            edtTo.ValidateToken += AcceptAnyToken;
            edtCc.ValidateToken += AcceptAnyToken;
        }

        private void AcceptAnyToken(object sender, DevExpress.XtraEditors.TokenEditValidateTokenEventArgs e)
        {
            var edit = sender as DevExpress.XtraEditors.TokenEdit;
            string text = e.Description;

            if (IsValidEmail(text))
            {
                e.IsValid = true;
                if (edit != null) edit.ErrorText = string.Empty;
            }
            else
            {
                e.IsValid = false;
                if (edit != null) edit.ErrorText = "Ungültige E-Mail-Adresse";
            }
        }

        private static bool IsValidEmail(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(text.Trim());
                return addr.Address.Equals(text.Trim(), StringComparison.OrdinalIgnoreCase);
            }
            catch { return false; }
        }

        private void TokenBackground_CustomDraw(object sender, DevExpress.XtraEditors.TokenEditCustomDrawTokenBackgroundEventArgs e)
        {
            var rect = e.Bounds;
            rect.Inflate(-1, -2);
            using (var brush = new SolidBrush(Color.FromArgb(239, 246, 255)))
            using (var pen = new Pen(Color.FromArgb(204, 223, 246)))
            using (var path = RoundedRect(rect, 6))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            }
            e.Handled = true;
        }

        private static System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
