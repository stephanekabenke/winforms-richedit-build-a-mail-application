using System.Drawing;
using DevExpress.XtraEditors;

namespace RichEditSendMail
{
    // Toolbar: Schriftart-/Groessen-Auswahl und vertikale Trenner.
    public partial class Form1
    {
        private void AddToolbarSeparators()
        {
            int[] positions = { 151, 365, 411, 489, 567, 645, 723, 771, 951, 1047 };
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

            // Chevron-Button ohne Rahmen/Box.
            cboFont.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            cboSize.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
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
    }
}
