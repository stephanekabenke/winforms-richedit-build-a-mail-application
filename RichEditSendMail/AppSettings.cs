using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RichEditSendMail
{
    // Speichert SMTP-Einstellungen und Signatur unter
    // %APPDATA%\RichEditSendMail\settings.dat. Das Passwort wird per
    // Windows-DPAPI (benutzergebunden) verschluesselt abgelegt.
    public class AppSettings
    {
        public string From = "";
        public string SmtpServer = "";
        public string SmtpPort = "587";
        public string SmtpPassword = "";
        public string Signature = "";

        static string SettingsPath
        {
            get
            {
                string dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "RichEditSendMail");
                Directory.CreateDirectory(dir);
                return Path.Combine(dir, "settings.dat");
            }
        }

        public void Save()
        {
            var sb = new StringBuilder();
            sb.AppendLine("From=" + Escape(From));
            sb.AppendLine("Server=" + Escape(SmtpServer));
            sb.AppendLine("Port=" + Escape(SmtpPort));
            sb.AppendLine("Password=" + Protect(SmtpPassword));
            sb.AppendLine("Signature=" + Escape(Signature));
            File.WriteAllText(SettingsPath, sb.ToString(), Encoding.UTF8);
        }

        public static AppSettings Load()
        {
            var s = new AppSettings();
            try
            {
                if (!File.Exists(SettingsPath)) return s;
                foreach (var line in File.ReadAllLines(SettingsPath, Encoding.UTF8))
                {
                    int i = line.IndexOf('=');
                    if (i < 0) continue;
                    string key = line.Substring(0, i);
                    string val = line.Substring(i + 1);
                    switch (key)
                    {
                        case "From": s.From = Unescape(val); break;
                        case "Server": s.SmtpServer = Unescape(val); break;
                        case "Port": s.SmtpPort = Unescape(val); break;
                        case "Password": s.SmtpPassword = Unprotect(val); break;
                        case "Signature": s.Signature = Unescape(val); break;
                    }
                }
            }
            catch
            {
                // Beschaedigte Datei wird ignoriert, Standardwerte greifen.
            }
            return s;
        }

        static string Escape(string v)
        {
            return (v ?? "").Replace("\\", "\\\\").Replace("\r", "\\r").Replace("\n", "\\n");
        }

        static string Unescape(string v)
        {
            return (v ?? "").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\\\", "\\");
        }

        static string Protect(string plain)
        {
            if (string.IsNullOrEmpty(plain)) return "";
            try
            {
                byte[] enc = ProtectedData.Protect(
                    Encoding.UTF8.GetBytes(plain), null, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(enc);
            }
            catch { return ""; }
        }

        static string Unprotect(string enc)
        {
            if (string.IsNullOrEmpty(enc)) return "";
            try
            {
                byte[] dec = ProtectedData.Unprotect(
                    Convert.FromBase64String(enc), null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(dec);
            }
            catch { return ""; }
        }
    }
}
