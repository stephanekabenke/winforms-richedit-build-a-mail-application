using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DevExpress.Office.Services;
using DevExpress.Office.Utils;
using DevExpress.Utils;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit.Export.Html;

namespace RichEditSendMail {
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private readonly List<string> _attachmentPaths = new List<string>();
        private Color _fontColor = Color.Red;

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

        public Form1() {
            InitializeComponent();

            InitializeToolbar();
            InitializeIcons();
            InitializeContacts();
            AddToolbarSeparators();
        }

        private void AddToolbarSeparators()
        {
            int[] positions = { 151, 365, 423, 601, 695 };
            foreach (int x in positions)
            {
                var sep = new PanelControl
                {
                    BackColor = Color.FromArgb(225, 225, 230),
                    BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder,
                    Location = new Point(x, 12),
                    Size = new Size(1, 24)
                };
                toolbarPanel.Controls.Add(sep);
            }
        }

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

        // -------------------------------------------------------------
        // Toolbar Setup
        // -------------------------------------------------------------

        private void InitializeToolbar()
        {
            foreach (FontFamily ff in FontFamily.Families)
            {
                cboFont.Properties.Items.Add(ff.Name);
            }
            cboFont.EditValue = "Calibri";

            foreach (var size in new[] { "8", "9", "10", "11", "12", "14", "16", "18", "20", "24", "28", "32", "36", "48", "72" })
            {
                cboSize.Properties.Items.Add(size);
            }
            cboSize.EditValue = "11";

            cboFont.Properties.DrawItem += CboFont_DrawItem;
        }

        private void CboFont_DrawItem(object sender, DevExpress.XtraEditors.ListBoxDrawItemEventArgs e)
        {
            string name = e.Item as string;
            if (string.IsNullOrEmpty(name)) { e.Handled = true; return; }

            // Background eigene, nicht DefaultDraw (verhindert doppeltes Text-Rendering)
            using (var bg = new SolidBrush(e.Appearance.BackColor))
            {
                e.Cache.Graphics.FillRectangle(bg, e.Bounds);
            }

            Font itemFont = null;
            try { itemFont = new Font(name, 9F, FontStyle.Regular, GraphicsUnit.Point); }
            catch { itemFont = (Font)e.Appearance.Font.Clone(); }

            try
            {
                using (var br = new SolidBrush(e.Appearance.ForeColor))
                {
                    var sf = new StringFormat(StringFormatFlags.NoWrap)
                    {
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.EllipsisCharacter
                    };
                    var prevClip = e.Cache.Graphics.Clip;
                    e.Cache.Graphics.SetClip(e.Bounds);
                    var textRect = new Rectangle(e.Bounds.X + 4, e.Bounds.Y, e.Bounds.Width - 8, e.Bounds.Height);
                    e.Cache.Graphics.DrawString(name, itemFont, br, textRect, sf);
                    e.Cache.Graphics.Clip = prevClip;
                }
            }
            finally
            {
                itemFont.Dispose();
            }
            e.Handled = true;
        }

        // -------------------------------------------------------------
        // SVG Icons (Mail-Editor-Style)
        // -------------------------------------------------------------

        private void InitializeIcons()
        {
            // F/K/U/S bleiben als typografische Glyphen (siehe Designer)
            SetIcon(btnAlignLeft,     SVG_ALIGN_LEFT,   "Linksbündig");
            SetIcon(btnAlignCenter,   SVG_ALIGN_CENTER, "Zentriert");
            SetIcon(btnAlignRight,    SVG_ALIGN_RIGHT,  "Rechtsbündig");
            SetIcon(btnAlignJustify,  SVG_JUSTIFY,      "Blocksatz");
            SetIcon(btnBulletList,    SVG_BULLET,       "Aufzählung");
            SetIcon(btnNumberedList,  SVG_NUMBERED,     "Nummerierte Liste");
            SetIcon(btnPicture,       SVG_PICTURE,      "Bild einfügen");
            SetIcon(btnHyperlink,     SVG_LINK,         "Hyperlink einfügen");
            SetIcon(btnAddAttachment, SVG_PAPERCLIP,    "Anhang hinzufügen");
        }

        private static void SetIcon(SimpleButton btn, string svg, string tooltip)
        {
            btn.ImageOptions.SvgImage = SvgFromString(svg);
            btn.ImageOptions.SvgImageSize = new Size(18, 18);
            btn.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btn.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.None;
            btn.Text = string.Empty;
            btn.ToolTip = tooltip;
        }

        private static void SetIconWithText(SimpleButton btn, string svg, string text, string tooltip)
        {
            btn.ImageOptions.SvgImage = SvgFromString(svg);
            btn.ImageOptions.SvgImageSize = new Size(16, 16);
            btn.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            btn.Text = text;
            btn.ToolTip = tooltip;
        }

        private static SvgImage SvgFromString(string xml)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            return SvgImage.FromStream(new MemoryStream(bytes));
        }

        // Fluent-Style Outline-Icons (16x16 viewBox, currentColor, stroke-based)
        private const string SVG_ALIGN_LEFT = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><path d='M2.5 3.5h11M2.5 7h7M2.5 10.5h11M2.5 14h7' stroke='currentColor' stroke-width='1.4' stroke-linecap='round' fill='none'/></svg>";
        private const string SVG_ALIGN_CENTER = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><path d='M2.5 3.5h11M4.5 7h7M2.5 10.5h11M4.5 14h7' stroke='currentColor' stroke-width='1.4' stroke-linecap='round' fill='none'/></svg>";
        private const string SVG_ALIGN_RIGHT = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><path d='M2.5 3.5h11M6.5 7h7M2.5 10.5h11M6.5 14h7' stroke='currentColor' stroke-width='1.4' stroke-linecap='round' fill='none'/></svg>";
        private const string SVG_JUSTIFY = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><path d='M2.5 3.5h11M2.5 7h11M2.5 10.5h11M2.5 14h11' stroke='currentColor' stroke-width='1.4' stroke-linecap='round' fill='none'/></svg>";
        private const string SVG_BULLET = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><circle cx='3' cy='4' r='1' fill='currentColor'/><circle cx='3' cy='8' r='1' fill='currentColor'/><circle cx='3' cy='12' r='1' fill='currentColor'/><path d='M6 4h8M6 8h8M6 12h8' stroke='currentColor' stroke-width='1.4' stroke-linecap='round' fill='none'/></svg>";
        private const string SVG_NUMBERED = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><text x='0.4' y='5.4' font-family='Segoe UI' font-size='4.2' font-weight='600' fill='currentColor'>1</text><text x='0.4' y='9.4' font-family='Segoe UI' font-size='4.2' font-weight='600' fill='currentColor'>2</text><text x='0.4' y='13.4' font-family='Segoe UI' font-size='4.2' font-weight='600' fill='currentColor'>3</text><path d='M6 4h8M6 8h8M6 12h8' stroke='currentColor' stroke-width='1.4' stroke-linecap='round' fill='none'/></svg>";
        private const string SVG_PICTURE = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><rect x='1.5' y='2.5' width='13' height='11' rx='1.2' fill='none' stroke='currentColor' stroke-width='1.4'/><circle cx='5.5' cy='6' r='1.1' fill='currentColor'/><path d='M2 12l3.5-3 2.5 2 3-3 3 3.5' fill='none' stroke='currentColor' stroke-width='1.3' stroke-linejoin='round'/></svg>";
        private const string SVG_LINK = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><path d='M6.5 4.5h-2a3.5 3.5 0 1 0 0 7h2m3 0h2a3.5 3.5 0 1 0 0-7h-2M5.5 8h5' fill='none' stroke='currentColor' stroke-width='1.4' stroke-linecap='round'/></svg>";
        private const string SVG_PAPERCLIP = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><path d='M13 6.5l-6 6a3 3 0 1 1-4.2-4.2L8.7 2.4a2 2 0 1 1 2.8 2.8L5.7 11a1 1 0 1 1-1.4-1.4L9.6 4.3' fill='none' stroke='currentColor' stroke-width='1.4' stroke-linecap='round' stroke-linejoin='round'/></svg>";

        // -------------------------------------------------------------
        // Helpers
        // -------------------------------------------------------------

        private void ApplyToCharacters(Action<CharacterProperties> action)
        {
            try
            {
                var doc = richEdit.Document;
                var sel = doc.Selection;
                var cp = doc.BeginUpdateCharacters(sel);
                try { action(cp); }
                finally { doc.EndUpdateCharacters(cp); }
                richEdit.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Formatierung fehlgeschlagen: " + ex.Message);
            }
        }

        private void ApplyToParagraphs(Action<ParagraphProperties> action)
        {
            try
            {
                var doc = richEdit.Document;
                var sel = doc.Selection;
                var pp = doc.BeginUpdateParagraphs(sel);
                try { action(pp); }
                finally { doc.EndUpdateParagraphs(pp); }
                richEdit.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Formatierung fehlgeschlagen: " + ex.Message);
            }
        }

        // -------------------------------------------------------------
        // Character formatting
        // -------------------------------------------------------------

        private void btnBold_Click(object sender, EventArgs e)
        {
            ApplyToCharacters(cp =>
            {
                bool current = cp.Bold == true;
                cp.Bold = !current;
            });
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            ApplyToCharacters(cp =>
            {
                bool current = cp.Italic == true;
                cp.Italic = !current;
            });
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            ApplyToCharacters(cp =>
            {
                cp.Underline = cp.Underline == UnderlineType.None ? UnderlineType.Single : UnderlineType.None;
            });
        }

        private void btnStrike_Click(object sender, EventArgs e)
        {
            ApplyToCharacters(cp =>
            {
                cp.Strikeout = cp.Strikeout == StrikeoutType.None ? StrikeoutType.Single : StrikeoutType.None;
            });
        }

        private void cboFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFont.SelectedItem == null) return;
            string name = cboFont.SelectedItem.ToString();
            ApplyToCharacters(cp => { cp.FontName = name; });
        }

        private void cboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSize.SelectedItem == null) return;
            if (!float.TryParse(cboSize.SelectedItem.ToString(), out float size)) return;
            ApplyToCharacters(cp => { cp.FontSize = size; });
        }

        private void colorFont_EditValueChanged(object sender, EventArgs e)
        {
            Color c = colorFont.Color;
            if (c == Color.Empty) return;
            _fontColor = c;
            ApplyToCharacters(cp => { cp.ForeColor = c; });
        }

        // -------------------------------------------------------------
        // Paragraph alignment
        // -------------------------------------------------------------

        private void btnAlignLeft_Click(object sender, EventArgs e)
        {
            ApplyToParagraphs(pp => { pp.Alignment = ParagraphAlignment.Left; });
        }

        private void btnAlignCenter_Click(object sender, EventArgs e)
        {
            ApplyToParagraphs(pp => { pp.Alignment = ParagraphAlignment.Center; });
        }

        private void btnAlignRight_Click(object sender, EventArgs e)
        {
            ApplyToParagraphs(pp => { pp.Alignment = ParagraphAlignment.Right; });
        }

        private void btnAlignJustify_Click(object sender, EventArgs e)
        {
            ApplyToParagraphs(pp => { pp.Alignment = ParagraphAlignment.Justify; });
        }

        // -------------------------------------------------------------
        // Lists
        // -------------------------------------------------------------

        private void btnBulletList_Click(object sender, EventArgs e)
        {
            try
            {
                new DevExpress.XtraRichEdit.Commands.ToggleBulletedListCommand(richEdit).Execute();
                richEdit.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Aufzählung fehlgeschlagen: " + ex.Message);
            }
        }

        private void btnNumberedList_Click(object sender, EventArgs e)
        {
            try
            {
                new DevExpress.XtraRichEdit.Commands.ToggleSimpleNumberingListCommand(richEdit).Execute();
                richEdit.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Nummerierung fehlgeschlagen: " + ex.Message);
            }
        }

        // -------------------------------------------------------------
        // Insert
        // -------------------------------------------------------------

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

        // -------------------------------------------------------------
        // Kontakte
        // -------------------------------------------------------------


        private static IEnumerable<string> SplitAddresses(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) yield break;
            foreach (var part in s.Split(new[] { ';', ',' }))
            {
                var trimmed = part.Trim();
                if (!string.IsNullOrEmpty(trimmed)) yield return trimmed;
            }
        }

        // -------------------------------------------------------------
        // Anhänge
        // -------------------------------------------------------------

        private void btnAddAttachment_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Anhänge wählen";
                dlg.Multiselect = true;
                dlg.Filter = "Alle Dateien (*.*)|*.*";
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                foreach (var path in dlg.FileNames)
                {
                    if (_attachmentPaths.Contains(path)) continue;
                    _attachmentPaths.Add(path);
                    AddAttachmentTile(path);
                }
                UpdateAttachmentsVisibility();
            }
        }

        private void UpdateAttachmentsVisibility()
        {
            bool show = _attachmentPaths.Count > 0;
            groupAttachments.Visible = show;
            sepBelowAttachments.Visible = show;
        }

        private void AddAttachmentTile(string path)
        {
            var tile = new System.Windows.Forms.Panel
            {
                Size = new Size(210, 60),
                BackColor = Color.White,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Margin = new Padding(4)
            };

            var iconBox = new System.Windows.Forms.PictureBox
            {
                Size = new Size(34, 34),
                Location = new Point(8, 12),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent
            };
            Icon icon = GetFileIcon(path);
            if (icon != null) iconBox.Image = icon.ToBitmap();

            var lblName = new System.Windows.Forms.Label
            {
                Text = Path.GetFileName(path),
                AutoEllipsis = true,
                Location = new Point(48, 8),
                Size = new Size(130, 22),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            var lblSize = new System.Windows.Forms.Label
            {
                Text = FormatFileSize(path),
                Location = new Point(48, 32),
                Size = new Size(130, 18),
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.Gray
            };

            var btnRemove = new System.Windows.Forms.Button
            {
                Text = "✕",
                Size = new Size(22, 22),
                Location = new Point(182, 4),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TabStop = false,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 230, 230);
            btnRemove.Click += (s, ev) =>
            {
                _attachmentPaths.Remove(path);
                attachmentsFlow.Controls.Remove(tile);
                tile.Dispose();
                UpdateAttachmentsVisibility();
            };

            tile.Controls.Add(btnRemove);
            tile.Controls.Add(iconBox);
            tile.Controls.Add(lblName);
            tile.Controls.Add(lblSize);
            btnRemove.BringToFront();

            new System.Windows.Forms.ToolTip().SetToolTip(tile, path);
            attachmentsFlow.Controls.Add(tile);
        }

        private static string FormatFileSize(string path)
        {
            try
            {
                long bytes = new FileInfo(path).Length;
                if (bytes < 1024) return bytes + " B";
                if (bytes < 1024 * 1024) return (bytes / 1024.0).ToString("0.0") + " KB";
                return (bytes / 1024.0 / 1024.0).ToString("0.0") + " MB";
            }
            catch
            {
                return "";
            }
        }

        // Windows-Datei-Icon via Shell-API
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [DllImport("user32.dll")]
        private static extern bool DestroyIcon(IntPtr hIcon);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_LARGEICON = 0x0;

        private static Icon GetFileIcon(string path)
        {
            try
            {
                var info = new SHFILEINFO();
                SHGetFileInfo(path, 0, ref info, (uint)Marshal.SizeOf(info), SHGFI_ICON | SHGFI_LARGEICON);
                if (info.hIcon == IntPtr.Zero) return null;
                var icon = (Icon)Icon.FromHandle(info.hIcon).Clone();
                DestroyIcon(info.hIcon);
                return icon;
            }
            catch
            {
                return null;
            }
        }

        // -------------------------------------------------------------
        // Send
        // -------------------------------------------------------------

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

        // -------------------------------------------------------------
        // Inline-Image Exporter (unverändert)
        // -------------------------------------------------------------

        public class RichEditMailMessageExporter : IUriProvider {
            readonly RichEditControl control;
            readonly MailMessage message;
            List<AttachementInfo> attachments;
            int imageId;

            public RichEditMailMessageExporter(RichEditControl control, MailMessage message) {
                Guard.ArgumentNotNull(control, "control");
                Guard.ArgumentNotNull(message, "message");

                this.control = control;
                this.message = message;
            }

            public virtual void Export() {
                this.attachments = new List<AttachementInfo>();

                AlternateView htmlView = CreateHtmlView();
                message.AlternateViews.Add(htmlView);
                message.IsBodyHtml = true;
            }

            protected internal virtual AlternateView CreateHtmlView() {
                control.BeforeExport += OnBeforeExport;
                string htmlBody = control.Document.GetHtmlText(control.Document.Range, this);
                AlternateView view = AlternateView.CreateAlternateViewFromString(htmlBody, Encoding.UTF8, MediaTypeNames.Text.Html);
                control.BeforeExport -= OnBeforeExport;

                int count = attachments.Count;
                for (int i = 0; i < count; i++) {
                    AttachementInfo info = attachments[i];
                    LinkedResource resource = new LinkedResource(info.Stream, info.MimeType);
                    resource.ContentId = info.ContentId;
                    view.LinkedResources.Add(resource);
                }
                return view;
            }

            void OnBeforeExport(object sender, BeforeExportEventArgs e) {
                HtmlDocumentExporterOptions options = e.Options as HtmlDocumentExporterOptions;
                if (options != null) {
                    options.Encoding = Encoding.UTF8;
                }
            }

            #region IUriProvider Members

            public string CreateCssUri(string rootUri, string styleText, string relativeUri) {
                return String.Empty;
            }
            public string CreateImageUri(string rootUri, OfficeImage image, string relativeUri) {
                string imageName = String.Format("image{0}", imageId);
                imageId++;

                OfficeImageFormat imageFormat = GetActualImageFormat(image.RawFormat);
                Stream stream = new MemoryStream(image.GetImageBytes(imageFormat));
                string mediaContentType = OfficeImage.GetContentType(imageFormat);
                AttachementInfo info = new AttachementInfo(stream, mediaContentType, imageName);
                attachments.Add(info);

                return "cid:" + imageName;
            }

            OfficeImageFormat GetActualImageFormat(OfficeImageFormat _officeImageFormat) {
                if (_officeImageFormat == OfficeImageFormat.Exif ||
                    _officeImageFormat == OfficeImageFormat.MemoryBmp)
                    return OfficeImageFormat.Png;
                else
                    return _officeImageFormat;
            }
            #endregion
        }

        public class AttachementInfo {
            Stream stream;
            string mimeType;
            string contentId;

            public AttachementInfo(Stream stream, string mimeType, string contentId) {
                this.stream = stream;
                this.mimeType = mimeType;
                this.contentId = contentId;
            }

            public Stream Stream { get { return stream; } }
            public string MimeType { get { return mimeType; } }
            public string ContentId { get { return contentId; } }
        }
    }
}
