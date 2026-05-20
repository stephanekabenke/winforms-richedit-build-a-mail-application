using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.API.Native;

namespace RichEditSendMail
{
    // Zeichen-/Absatzformatierung und Listen.
    public partial class Form1
    {
        private Color _fontColor = Color.Red;

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

        // Liest eine Zeichen-Eigenschaft der aktuellen Auswahl aus.
        private bool SelectionHas(Func<CharacterProperties, bool> predicate)
        {
            var doc = richEdit.Document;
            var cp = doc.BeginUpdateCharacters(doc.Selection);
            try { return predicate(cp); }
            finally { doc.EndUpdateCharacters(cp); }
        }

        // Wendet eine Zeichen-Formatierung nur auf Nicht-Leerzeichen an.
        // Die Auswahl wird in Wort-Bloecke zerlegt, sodass Unterstreichung
        // und Durchstreichung an jedem Leerzeichen aufhoeren.
        private void ApplyToWords(Action<CharacterProperties> action)
        {
            try
            {
                var doc = richEdit.Document;
                var sel = doc.Selection;
                string text = doc.GetText(sel);

                if (string.IsNullOrEmpty(text))
                {
                    // Keine Auswahl: direkt auf die Cursorposition anwenden.
                    var cpCaret = doc.BeginUpdateCharacters(sel);
                    try { action(cpCaret); }
                    finally { doc.EndUpdateCharacters(cpCaret); }
                    richEdit.Focus();
                    return;
                }

                int baseStart = sel.Start.ToInt();
                doc.BeginUpdate();
                try
                {
                    int i = 0;
                    while (i < text.Length)
                    {
                        if (char.IsWhiteSpace(text[i])) { i++; continue; }

                        int runStart = i;
                        while (i < text.Length && !char.IsWhiteSpace(text[i])) i++;

                        var range = doc.CreateRange(baseStart + runStart, i - runStart);
                        var cp = doc.BeginUpdateCharacters(range);
                        try { action(cp); }
                        finally { doc.EndUpdateCharacters(cp); }
                    }
                }
                finally
                {
                    doc.EndUpdate();
                }
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
            bool isOn = SelectionHas(cp => cp.Underline != UnderlineType.None);
            if (isOn)
                ApplyToCharacters(cp => { cp.Underline = UnderlineType.None; });
            else
                ApplyToWords(cp => { cp.Underline = UnderlineType.Single; });
        }

        private void btnStrike_Click(object sender, EventArgs e)
        {
            bool isOn = SelectionHas(cp => cp.Strikeout != StrikeoutType.None);
            if (isOn)
                ApplyToCharacters(cp => { cp.Strikeout = StrikeoutType.None; });
            else
                ApplyToWords(cp => { cp.Strikeout = StrikeoutType.Single; });
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

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            Color chosen;
            using (var dlg = new ColorDialog())
            {
                dlg.Color = _fontColor;
                dlg.FullOpen = true;
                dlg.AnyColor = true;
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                chosen = dlg.Color;
            }

            _fontColor = chosen;
            UpdateFontColorIcon();
            ApplyToCharacters(cp => { cp.ForeColor = chosen; });
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
        // Erweiterte Toolbar-Befehle
        // -------------------------------------------------------------

        private void RunCommand(Action command)
        {
            try
            {
                command();
                richEdit.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Befehl fehlgeschlagen: " + ex.Message);
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            RunCommand(() => new DevExpress.XtraRichEdit.Commands.UndoCommand(richEdit).Execute());
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            RunCommand(() => new DevExpress.XtraRichEdit.Commands.RedoCommand(richEdit).Execute());
        }

        private void btnIncreaseFont_Click(object sender, EventArgs e)
        {
            RunCommand(() => new DevExpress.XtraRichEdit.Commands.IncreaseFontSizeCommand(richEdit).Execute());
        }

        private void btnDecreaseFont_Click(object sender, EventArgs e)
        {
            RunCommand(() => new DevExpress.XtraRichEdit.Commands.DecreaseFontSizeCommand(richEdit).Execute());
        }

        private void btnSuperscript_Click(object sender, EventArgs e)
        {
            RunCommand(() => new DevExpress.XtraRichEdit.Commands.ToggleFontSuperscriptCommand(richEdit).Execute());
        }

        private void btnSubscript_Click(object sender, EventArgs e)
        {
            RunCommand(() => new DevExpress.XtraRichEdit.Commands.ToggleFontSubscriptCommand(richEdit).Execute());
        }

        private void btnIncrementIndent_Click(object sender, EventArgs e)
        {
            RunCommand(() => new DevExpress.XtraRichEdit.Commands.IncrementIndentCommand(richEdit).Execute());
        }

        private void btnDecrementIndent_Click(object sender, EventArgs e)
        {
            RunCommand(() => new DevExpress.XtraRichEdit.Commands.DecrementIndentCommand(richEdit).Execute());
        }

        private void btnClearFormat_Click(object sender, EventArgs e)
        {
            RunCommand(() => new DevExpress.XtraRichEdit.Commands.ClearFormattingCommand(richEdit).Execute());
        }
    }
}
