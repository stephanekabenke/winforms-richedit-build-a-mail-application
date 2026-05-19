using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RichEditSendMail
{
    // Anhaenge: Auswahl, Kacheln, Datei-Icons und Groessenanzeige.
    public partial class Form1
    {
        private readonly List<string> _attachmentPaths = new List<string>();

        // Gestrichelte Drop-Zone, sichtbar solange keine Anhaenge da sind.
        private System.Windows.Forms.Panel _attachmentsPlaceholder;

        private void btnAddAttachment_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Anhänge wählen";
                dlg.Multiselect = true;
                dlg.Filter = "Alle Dateien (*.*)|*.*";
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                AddAttachmentsFromPaths(dlg.FileNames);
            }
        }

        // Fuegt mehrere Dateien als Anhaenge hinzu. Ordner und bereits
        // vorhandene oder nicht existierende Pfade werden uebersprungen.
        private void AddAttachmentsFromPaths(IEnumerable<string> paths)
        {
            if (paths == null) return;
            foreach (var path in paths)
            {
                if (string.IsNullOrEmpty(path)) continue;
                if (!File.Exists(path)) continue;
                if (_attachmentPaths.Contains(path)) continue;
                _attachmentPaths.Add(path);
                AddAttachmentTile(path);
            }
            UpdateAttachmentsVisibility();
        }

        // -------------------------------------------------------------
        // Drag and Drop
        // -------------------------------------------------------------

        // Aktiviert Datei-Drop auf Toolbar, Mail-Kopf, SMTP-Fusszeile und
        // die Anhang-Sidebar. Der RichEdit-Editor behaelt sein eigenes
        // Drag-Verhalten (Inline-Bild beim Ziehen ins Dokument).
        private void InitializeAttachmentDragDrop()
        {
            Control[] dropTargets =
            {
                this, toolbarPanel, panelControl1, smtpFooter,
                groupAttachments, attachmentsFlow
            };
            foreach (var c in dropTargets)
            {
                if (c == null) continue;
                c.AllowDrop = true;
                c.DragEnter += OnAttachmentDragEnter;
                c.DragDrop += OnAttachmentDragDrop;
            }
        }

        // Erzeugt die gestrichelte Drop-Zone fuer den leeren Zustand.
        private void InitializeAttachmentsPlaceholder()
        {
            var lbl = new System.Windows.Forms.Label
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                Text = "Dateien hierher ziehen,\num sie anzuhängen",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(140, 140, 145),
                BackColor = Color.Transparent
            };

            _attachmentsPlaceholder = new System.Windows.Forms.Panel
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(10)
            };
            _attachmentsPlaceholder.Paint += AttachmentsPlaceholder_Paint;
            _attachmentsPlaceholder.Controls.Add(lbl);

            groupAttachments.Controls.Add(_attachmentsPlaceholder);
            _attachmentsPlaceholder.BringToFront();

            foreach (var c in new Control[] { _attachmentsPlaceholder, lbl })
            {
                c.AllowDrop = true;
                c.DragEnter += OnAttachmentDragEnter;
                c.DragDrop += OnAttachmentDragDrop;
            }

            UpdateAttachmentsVisibility();
        }

        private void AttachmentsPlaceholder_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var r = _attachmentsPlaceholder.ClientRectangle;
            r.Inflate(-8, -8);
            if (r.Width <= 0 || r.Height <= 0) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (var pen = new Pen(Color.FromArgb(190, 195, 205), 1.5f))
            using (var path = RoundedRect(r, 10))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                e.Graphics.DrawPath(pen, path);
            }
        }

        private static void OnAttachmentDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }

        private void OnAttachmentDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null || !e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var paths = e.Data.GetData(DataFormats.FileDrop) as string[];
            AddAttachmentsFromPaths(paths);
        }

        // Die Anhang-Sidebar bleibt immer sichtbar. Bei leerem Zustand wird
        // die Drop-Zone gezeigt, sonst die Kachel-Liste.
        private void UpdateAttachmentsVisibility()
        {
            bool hasAttachments = _attachmentPaths.Count > 0;
            attachmentsFlow.Visible = hasAttachments;
            if (_attachmentsPlaceholder != null)
                _attachmentsPlaceholder.Visible = !hasAttachments;
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
    }
}
