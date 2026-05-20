using System.Drawing;
using System.IO;
using System.Text;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;

namespace RichEditSendMail
{
    // SVG-Icons der Toolbar-Buttons (Mail-Editor-Style).
    public partial class Form1
    {
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

            SetIcon(btnUndo,            SVG_UNDO,         "Rückgängig");
            SetIcon(btnRedo,            SVG_REDO,         "Wiederholen");
            SetIcon(btnDecreaseFont,    SVG_FONT_SHRINK,  "Schrift verkleinern");
            SetIcon(btnIncreaseFont,    SVG_FONT_GROW,    "Schrift vergrößern");
            SetIcon(btnSuperscript,     SVG_SUPERSCRIPT,  "Hochgestellt");
            SetIcon(btnSubscript,       SVG_SUBSCRIPT,    "Tiefgestellt");
            SetIcon(btnDecrementIndent, SVG_INDENT_LESS,  "Einzug verkleinern");
            SetIcon(btnIncrementIndent, SVG_INDENT_MORE,  "Einzug vergrößern");
            SetIcon(btnClearFormat,     SVG_CLEAR_FORMAT, "Formatierung löschen");

            UpdateFontColorIcon();
        }

        // Schriftfarbe-Icon: Buchstabe "A" mit farbigem Balken (aktuelle Farbe).
        private void UpdateFontColorIcon()
        {
            string hex = string.Format("#{0:X2}{1:X2}{2:X2}",
                _fontColor.R, _fontColor.G, _fontColor.B);
            btnFontColor.ImageOptions.SvgImage = SvgFromString(string.Format(SVG_FONT_COLOR_FORMAT, hex));
            btnFontColor.ImageOptions.SvgImageSize = new Size(18, 18);
            btnFontColor.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnFontColor.Text = string.Empty;
            btnFontColor.ToolTip = "Schriftfarbe";
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

        // Schriftfarbe: Buchstabe A plus farbiger Balken. {0} = Balkenfarbe.
        private const string SVG_FONT_COLOR_FORMAT = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><text x='4.2' y='11' font-family='Segoe UI' font-size='11' font-weight='700' fill='#333333'>A</text><rect x='2.5' y='12.4' width='11' height='2.8' rx='0.6' fill='{0}'/></svg>";

        private const string SVG_UNDO = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><path d='M3.5 8h6.5a3 3 0 0 1 0 6H6' fill='none' stroke='currentColor' stroke-width='1.4' stroke-linecap='round' stroke-linejoin='round'/><path d='M3.5 8l3-2.6M3.5 8l3 2.6' fill='none' stroke='currentColor' stroke-width='1.4' stroke-linecap='round' stroke-linejoin='round'/></svg>";
        private const string SVG_REDO = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><path d='M12.5 8H6a3 3 0 0 0 0 6h4' fill='none' stroke='currentColor' stroke-width='1.4' stroke-linecap='round' stroke-linejoin='round'/><path d='M12.5 8l-3-2.6M12.5 8l-3 2.6' fill='none' stroke='currentColor' stroke-width='1.4' stroke-linecap='round' stroke-linejoin='round'/></svg>";
        private const string SVG_FONT_GROW = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><text x='0.3' y='13' font-family='Segoe UI' font-size='12' font-weight='700' fill='currentColor'>A</text><path d='M12 12.5V5M12 5l-2 2.3M12 5l2 2.3' fill='none' stroke='currentColor' stroke-width='1.3' stroke-linecap='round' stroke-linejoin='round'/></svg>";
        private const string SVG_FONT_SHRINK = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><text x='1.5' y='13' font-family='Segoe UI' font-size='9' font-weight='700' fill='currentColor'>A</text><path d='M12 5v7.5M12 12.5l-2-2.3M12 12.5l2-2.3' fill='none' stroke='currentColor' stroke-width='1.3' stroke-linecap='round' stroke-linejoin='round'/></svg>";
        private const string SVG_SUPERSCRIPT = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><text x='1' y='13' font-family='Segoe UI' font-size='10' font-weight='700' fill='currentColor'>X</text><text x='9' y='7.5' font-family='Segoe UI' font-size='6.5' font-weight='700' fill='currentColor'>2</text></svg>";
        private const string SVG_SUBSCRIPT = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><text x='1' y='11.5' font-family='Segoe UI' font-size='10' font-weight='700' fill='currentColor'>X</text><text x='9' y='15' font-family='Segoe UI' font-size='6.5' font-weight='700' fill='currentColor'>2</text></svg>";
        private const string SVG_INDENT_MORE = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><path d='M2.5 3.5h11M7 7h6.5M7 9h6.5M2.5 12.5h11' fill='none' stroke='currentColor' stroke-width='1.4' stroke-linecap='round'/><path d='M2.7 6.3l2.6 1.7-2.6 1.7z' fill='currentColor'/></svg>";
        private const string SVG_INDENT_LESS = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><path d='M2.5 3.5h11M7 7h6.5M7 9h6.5M2.5 12.5h11' fill='none' stroke='currentColor' stroke-width='1.4' stroke-linecap='round'/><path d='M5.3 6.3L2.7 8l2.6 1.7z' fill='currentColor'/></svg>";
        private const string SVG_CLEAR_FORMAT = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'><path d='M3 13h10' fill='none' stroke='currentColor' stroke-width='1.4' stroke-linecap='round'/><path d='M6.5 13L3.3 9.8a1.3 1.3 0 0 1 0-1.8l4.7-4.7a1.3 1.3 0 0 1 1.8 0l3.2 3.2a1.3 1.3 0 0 1 0 1.8L9 13z' fill='none' stroke='currentColor' stroke-width='1.4' stroke-linejoin='round'/><path d='M6.6 5.1l4.3 4.3' fill='none' stroke='currentColor' stroke-width='1.3'/></svg>";
    }
}
