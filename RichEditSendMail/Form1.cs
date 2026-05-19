namespace RichEditSendMail
{
    /// <summary>
    /// Haupt-Formular des Mail-Editors.
    /// Die Logik ist auf mehrere partial-class-Dateien aufgeteilt:
    ///   Form1.Toolbar.cs      - Schriftart-/Groessen-Auswahl, Trenner
    ///   Form1.Icons.cs        - SVG-Icons der Toolbar-Buttons
    ///   Form1.Contacts.cs     - Kontaktliste, Token-Felder (An/CC)
    ///   Form1.Formatting.cs   - Zeichen-/Absatzformatierung, Listen
    ///   Form1.Insert.cs       - Bild / Hyperlink einfuegen
    ///   Form1.Attachments.cs  - Anhaenge verwalten
    ///   Form1.Send.cs         - Mail versenden
    /// </summary>
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();

            InitializeToolbar();
            InitializeIcons();
            InitializeContacts();
            AddToolbarSeparators();
            InitializeAttachmentsPlaceholder();
            InitializeAttachmentDragDrop();
        }
    }
}
