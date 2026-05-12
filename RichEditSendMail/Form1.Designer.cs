namespace RichEditSendMail {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            this.richEdit = new DevExpress.XtraRichEdit.RichEditControl();

            // Toolbar
            this.toolbarPanel = new DevExpress.XtraEditors.PanelControl();
            this.btnBold = new DevExpress.XtraEditors.SimpleButton();
            this.btnItalic = new DevExpress.XtraEditors.SimpleButton();
            this.btnUnderline = new DevExpress.XtraEditors.SimpleButton();
            this.btnStrike = new DevExpress.XtraEditors.SimpleButton();
            this.cboFont = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboSize = new DevExpress.XtraEditors.ComboBoxEdit();
            this.colorFont = new DevExpress.XtraEditors.ColorPickEdit();
            this.btnAlignLeft = new DevExpress.XtraEditors.SimpleButton();
            this.btnAlignCenter = new DevExpress.XtraEditors.SimpleButton();
            this.btnAlignRight = new DevExpress.XtraEditors.SimpleButton();
            this.btnAlignJustify = new DevExpress.XtraEditors.SimpleButton();
            this.btnBulletList = new DevExpress.XtraEditors.SimpleButton();
            this.btnNumberedList = new DevExpress.XtraEditors.SimpleButton();
            this.btnPicture = new DevExpress.XtraEditors.SimpleButton();
            this.btnHyperlink = new DevExpress.XtraEditors.SimpleButton();

            // Anhang-Bereich
            this.groupAttachments = new DevExpress.XtraEditors.GroupControl();
            this.attachmentsFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddAttachment = new DevExpress.XtraEditors.SimpleButton();

            // Mail-Felder
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.bottomSeparator = new DevExpress.XtraEditors.PanelControl();
            this.sepBelowAttachments = new DevExpress.XtraEditors.PanelControl();
            // SMTP Footer
            this.smtpFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblSmtpServer = new DevExpress.XtraEditors.LabelControl();
            this.edtSmtpServer = new DevExpress.XtraEditors.TextEdit();
            this.lblSmtpPort = new DevExpress.XtraEditors.LabelControl();
            this.edtSmtpPort = new DevExpress.XtraEditors.TextEdit();
            this.lblSmtpPassword = new DevExpress.XtraEditors.LabelControl();
            this.edtSmtpPassword = new DevExpress.XtraEditors.TextEdit();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.lblFrom = new DevExpress.XtraEditors.LabelControl();
            this.edtFrom = new DevExpress.XtraEditors.TextEdit();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.edtTo = new DevExpress.XtraEditors.TokenEdit();
            this.lblCc = new DevExpress.XtraEditors.LabelControl();
            this.edtCc = new DevExpress.XtraEditors.TokenEdit();
            this.lblSubject = new DevExpress.XtraEditors.LabelControl();
            this.edtSubject = new DevExpress.XtraEditors.TextEdit();

            ((System.ComponentModel.ISupportInitialize)(this.toolbarPanel)).BeginInit();
            this.toolbarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboFont.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorFont.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupAttachments)).BeginInit();
            this.groupAttachments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomSeparator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sepBelowAttachments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtpFooter)).BeginInit();
            this.smtpFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtSmtpServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSmtpPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSmtpPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtCc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSubject.Properties)).BeginInit();
            this.SuspendLayout();

            //
            // richEdit
            //
            this.richEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEdit.Location = new System.Drawing.Point(0, 48);
            this.richEdit.Margin = new System.Windows.Forms.Padding(4);
            this.richEdit.Name = "richEdit";
            this.richEdit.Size = new System.Drawing.Size(1200, 502);
            this.richEdit.TabIndex = 0;

            //
            // ========== TOOLBAR ==========
            //
            this.toolbarPanel.Controls.Add(this.btnBold);
            this.toolbarPanel.Controls.Add(this.btnItalic);
            this.toolbarPanel.Controls.Add(this.btnUnderline);
            this.toolbarPanel.Controls.Add(this.btnStrike);
            this.toolbarPanel.Controls.Add(this.cboFont);
            this.toolbarPanel.Controls.Add(this.cboSize);
            this.toolbarPanel.Controls.Add(this.colorFont);
            this.toolbarPanel.Controls.Add(this.btnAlignLeft);
            this.toolbarPanel.Controls.Add(this.btnAlignCenter);
            this.toolbarPanel.Controls.Add(this.btnAlignRight);
            this.toolbarPanel.Controls.Add(this.btnAlignJustify);
            this.toolbarPanel.Controls.Add(this.btnBulletList);
            this.toolbarPanel.Controls.Add(this.btnNumberedList);
            this.toolbarPanel.Controls.Add(this.btnPicture);
            this.toolbarPanel.Controls.Add(this.btnHyperlink);
            this.toolbarPanel.Controls.Add(this.btnAddAttachment);
            this.toolbarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolbarPanel.Location = new System.Drawing.Point(0, 0);
            this.toolbarPanel.Name = "toolbarPanel";
            this.toolbarPanel.Size = new System.Drawing.Size(1200, 48);
            this.toolbarPanel.TabIndex = 1;

            // -- Style group (F K U S) --
            this.btnBold.AllowFocus = false;
            this.btnBold.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBold.Appearance.Options.UseFont = true;
            this.btnBold.Location = new System.Drawing.Point(10, 9);
            this.btnBold.Name = "btnBold";
            this.btnBold.Size = new System.Drawing.Size(32, 30);
            this.btnBold.TabIndex = 0;
            this.btnBold.Text = "F";
            this.btnBold.ToolTip = "Fett";
            this.btnBold.Click += new System.EventHandler(this.btnBold_Click);

            this.btnItalic.AllowFocus = false;
            this.btnItalic.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.btnItalic.Appearance.Options.UseFont = true;
            this.btnItalic.Location = new System.Drawing.Point(44, 9);
            this.btnItalic.Name = "btnItalic";
            this.btnItalic.Size = new System.Drawing.Size(32, 30);
            this.btnItalic.TabIndex = 1;
            this.btnItalic.Text = "K";
            this.btnItalic.ToolTip = "Kursiv";
            this.btnItalic.Click += new System.EventHandler(this.btnItalic_Click);

            this.btnUnderline.AllowFocus = false;
            this.btnUnderline.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Underline);
            this.btnUnderline.Appearance.Options.UseFont = true;
            this.btnUnderline.Location = new System.Drawing.Point(78, 9);
            this.btnUnderline.Name = "btnUnderline";
            this.btnUnderline.Size = new System.Drawing.Size(32, 30);
            this.btnUnderline.TabIndex = 2;
            this.btnUnderline.Text = "U";
            this.btnUnderline.ToolTip = "Unterstrichen";
            this.btnUnderline.Click += new System.EventHandler(this.btnUnderline_Click);

            this.btnStrike.AllowFocus = false;
            this.btnStrike.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Strikeout);
            this.btnStrike.Appearance.Options.UseFont = true;
            this.btnStrike.Location = new System.Drawing.Point(112, 9);
            this.btnStrike.Name = "btnStrike";
            this.btnStrike.Size = new System.Drawing.Size(32, 30);
            this.btnStrike.TabIndex = 3;
            this.btnStrike.Text = "S";
            this.btnStrike.ToolTip = "Durchgestrichen";
            this.btnStrike.Click += new System.EventHandler(this.btnStrike_Click);

            // -- Font name + size --
            this.cboFont.Location = new System.Drawing.Point(158, 11);
            this.cboFont.Name = "cboFont";
            this.cboFont.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboFont.Properties.Appearance.Options.UseFont = true;
            this.cboFont.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.cboFont.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFont.Properties.NullValuePrompt = "Schriftart";
            this.cboFont.Size = new System.Drawing.Size(130, 26);
            this.cboFont.TabIndex = 4;
            this.cboFont.SelectedIndexChanged += new System.EventHandler(this.cboFont_SelectedIndexChanged);

            this.cboSize.Location = new System.Drawing.Point(294, 11);
            this.cboSize.Name = "cboSize";
            this.cboSize.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboSize.Properties.Appearance.Options.UseFont = true;
            this.cboSize.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.cboSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSize.Properties.NullValuePrompt = "Größe";
            this.cboSize.Size = new System.Drawing.Size(50, 26);
            this.cboSize.TabIndex = 5;
            this.cboSize.SelectedIndexChanged += new System.EventHandler(this.cboSize_SelectedIndexChanged);

            // -- Color --
            this.colorFont.EditValue = System.Drawing.Color.Red;
            this.colorFont.Location = new System.Drawing.Point(371, 9);
            this.colorFont.Name = "colorFont";
            this.colorFont.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.colorFont.Properties.Appearance.Options.UseFont = true;
            this.colorFont.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.colorFont.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorFont.Properties.ShowWebColors = false;
            this.colorFont.Properties.ShowSystemColors = false;
            this.colorFont.Properties.ShowCustomColors = true;
            this.colorFont.Properties.ShowAutomaticButton = false;
            this.colorFont.Properties.ShowMoreColorsButton = false;
            this.colorFont.Size = new System.Drawing.Size(60, 30);
            this.colorFont.TabIndex = 6;
            this.colorFont.ToolTip = "Schriftfarbe";
            this.colorFont.EditValueChanged += new System.EventHandler(this.colorFont_EditValueChanged);

            // -- Alignment --
            this.btnAlignLeft.AllowFocus = false;
            this.btnAlignLeft.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAlignLeft.Appearance.Options.UseFont = true;
            this.btnAlignLeft.Location = new System.Drawing.Point(429, 9);
            this.btnAlignLeft.Name = "btnAlignLeft";
            this.btnAlignLeft.Size = new System.Drawing.Size(38, 30);
            this.btnAlignLeft.TabIndex = 7;
            this.btnAlignLeft.Text = "≡";
            this.btnAlignLeft.ToolTip = "Linksbündig";
            this.btnAlignLeft.Click += new System.EventHandler(this.btnAlignLeft_Click);

            this.btnAlignCenter.AllowFocus = false;
            this.btnAlignCenter.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAlignCenter.Appearance.Options.UseFont = true;
            this.btnAlignCenter.Location = new System.Drawing.Point(471, 9);
            this.btnAlignCenter.Name = "btnAlignCenter";
            this.btnAlignCenter.Size = new System.Drawing.Size(38, 30);
            this.btnAlignCenter.TabIndex = 8;
            this.btnAlignCenter.Text = "≡̄";
            this.btnAlignCenter.ToolTip = "Zentriert";
            this.btnAlignCenter.Click += new System.EventHandler(this.btnAlignCenter_Click);

            this.btnAlignRight.AllowFocus = false;
            this.btnAlignRight.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAlignRight.Appearance.Options.UseFont = true;
            this.btnAlignRight.Location = new System.Drawing.Point(513, 9);
            this.btnAlignRight.Name = "btnAlignRight";
            this.btnAlignRight.Size = new System.Drawing.Size(38, 30);
            this.btnAlignRight.TabIndex = 9;
            this.btnAlignRight.Text = "≡";
            this.btnAlignRight.ToolTip = "Rechtsbündig";
            this.btnAlignRight.Click += new System.EventHandler(this.btnAlignRight_Click);

            this.btnAlignJustify.AllowFocus = false;
            this.btnAlignJustify.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAlignJustify.Appearance.Options.UseFont = true;
            this.btnAlignJustify.Location = new System.Drawing.Point(555, 9);
            this.btnAlignJustify.Name = "btnAlignJustify";
            this.btnAlignJustify.Size = new System.Drawing.Size(38, 30);
            this.btnAlignJustify.TabIndex = 10;
            this.btnAlignJustify.Text = "≣";
            this.btnAlignJustify.ToolTip = "Blocksatz";
            this.btnAlignJustify.Click += new System.EventHandler(this.btnAlignJustify_Click);

            // -- Lists --
            this.btnBulletList.AllowFocus = false;
            this.btnBulletList.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBulletList.Appearance.Options.UseFont = true;
            this.btnBulletList.Location = new System.Drawing.Point(607, 9);
            this.btnBulletList.Name = "btnBulletList";
            this.btnBulletList.Size = new System.Drawing.Size(38, 30);
            this.btnBulletList.TabIndex = 11;
            this.btnBulletList.Text = "•";
            this.btnBulletList.ToolTip = "Aufzählung";
            this.btnBulletList.Click += new System.EventHandler(this.btnBulletList_Click);

            this.btnNumberedList.AllowFocus = false;
            this.btnNumberedList.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNumberedList.Appearance.Options.UseFont = true;
            this.btnNumberedList.Location = new System.Drawing.Point(649, 9);
            this.btnNumberedList.Name = "btnNumberedList";
            this.btnNumberedList.Size = new System.Drawing.Size(38, 30);
            this.btnNumberedList.TabIndex = 12;
            this.btnNumberedList.Text = "1.";
            this.btnNumberedList.ToolTip = "Nummerierte Liste";
            this.btnNumberedList.Click += new System.EventHandler(this.btnNumberedList_Click);

            // -- Insert (Picture / Link) --
            this.btnPicture.AllowFocus = false;
            this.btnPicture.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPicture.Appearance.Options.UseFont = true;
            this.btnPicture.Location = new System.Drawing.Point(701, 9);
            this.btnPicture.Name = "btnPicture";
            this.btnPicture.Size = new System.Drawing.Size(44, 30);
            this.btnPicture.TabIndex = 13;
            this.btnPicture.Text = "";
            this.btnPicture.ToolTip = "Bild einfügen (inline)";
            this.btnPicture.Click += new System.EventHandler(this.btnPicture_Click);

            this.btnHyperlink.AllowFocus = false;
            this.btnHyperlink.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHyperlink.Appearance.Options.UseFont = true;
            this.btnHyperlink.Location = new System.Drawing.Point(749, 9);
            this.btnHyperlink.Name = "btnHyperlink";
            this.btnHyperlink.Size = new System.Drawing.Size(44, 30);
            this.btnHyperlink.TabIndex = 14;
            this.btnHyperlink.Text = "";
            this.btnHyperlink.ToolTip = "Hyperlink einfügen";
            this.btnHyperlink.Click += new System.EventHandler(this.btnHyperlink_Click);

            // -- Anhang (in Toolbar, neben Link) --
            this.btnAddAttachment.AllowFocus = false;
            this.btnAddAttachment.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddAttachment.Appearance.Options.UseFont = true;
            this.btnAddAttachment.Location = new System.Drawing.Point(797, 9);
            this.btnAddAttachment.Name = "btnAddAttachment";
            this.btnAddAttachment.Size = new System.Drawing.Size(44, 30);
            this.btnAddAttachment.TabIndex = 15;
            this.btnAddAttachment.Text = "";
            this.btnAddAttachment.ToolTip = "Anhang hinzufügen";
            this.btnAddAttachment.Click += new System.EventHandler(this.btnAddAttachment_Click);

            //
            // ========== ANHÄNGE (Yahoo-Style Tiles, unten) ==========
            //
            this.groupAttachments.Controls.Add(this.attachmentsFlow);
            this.groupAttachments.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupAttachments.Name = "groupAttachments";
            this.groupAttachments.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.groupAttachments.Size = new System.Drawing.Size(320, 800);
            this.groupAttachments.TabIndex = 2;
            this.groupAttachments.Text = "Anhänge";
            this.groupAttachments.Visible = false;

            // Vertikale Trennlinie zwischen Editor und Anhänge-Sidebar
            this.sepBelowAttachments.BackColor = System.Drawing.Color.FromArgb(225, 225, 230);
            this.sepBelowAttachments.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.sepBelowAttachments.Dock = System.Windows.Forms.DockStyle.Right;
            this.sepBelowAttachments.Name = "sepBelowAttachments";
            this.sepBelowAttachments.Size = new System.Drawing.Size(1, 800);
            this.sepBelowAttachments.TabIndex = 98;
            this.sepBelowAttachments.Visible = false;

            this.attachmentsFlow.AutoScroll = true;
            this.attachmentsFlow.BackColor = System.Drawing.Color.Transparent;
            this.attachmentsFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attachmentsFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.attachmentsFlow.Name = "attachmentsFlow";
            this.attachmentsFlow.Padding = new System.Windows.Forms.Padding(2);
            this.attachmentsFlow.WrapContents = false;

            //
            // ========== MAIL FIELDS + SEND ==========
            //
            this.panelControl1.Controls.Add(this.btnSend);
            this.panelControl1.Controls.Add(this.lblFrom);
            this.panelControl1.Controls.Add(this.edtFrom);
            this.panelControl1.Controls.Add(this.lblTo);
            this.panelControl1.Controls.Add(this.edtTo);
            this.panelControl1.Controls.Add(this.lblCc);
            this.panelControl1.Controls.Add(this.edtCc);
            this.panelControl1.Controls.Add(this.lblSubject);
            this.panelControl1.Controls.Add(this.edtSubject);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 670);
            this.panelControl1.Controls.Add(this.bottomSeparator);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1200, 230);
            this.panelControl1.TabIndex = 3;

            // Trennlinie unter dem Mail-Header
            this.bottomSeparator.BackColor = System.Drawing.Color.FromArgb(225, 225, 230);
            this.bottomSeparator.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.bottomSeparator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomSeparator.Location = new System.Drawing.Point(0, 229);
            this.bottomSeparator.Name = "bottomSeparator";
            this.bottomSeparator.Size = new System.Drawing.Size(1200, 1);
            this.bottomSeparator.TabIndex = 99;

            // Send-Button (prominent, ganze Höhe)
            this.btnSend.AllowFocus = false;
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.btnSend.Appearance.Options.UseFont = true;
            this.btnSend.Location = new System.Drawing.Point(1035, 18);
            this.btnSend.Margin = new System.Windows.Forms.Padding(5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(150, 115);
            this.btnSend.TabIndex = 100;
            this.btnSend.Text = "Senden";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // Von
            this.lblFrom.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFrom.Appearance.Options.UseFont = true;
            this.lblFrom.Location = new System.Drawing.Point(20, 24);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(30, 17);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "Von:";

            this.edtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtFrom.Location = new System.Drawing.Point(95, 20);
            this.edtFrom.Name = "edtFrom";
            this.edtFrom.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.edtFrom.Properties.Appearance.Options.UseFont = true;
            this.edtFrom.Properties.NullValuePrompt = "Deine E-Mail-Adresse";
            this.edtFrom.Size = new System.Drawing.Size(920, 38);
            this.edtFrom.TabIndex = 0;

            // An (Chips/Token + Dropdown)
            this.lblTo.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTo.Appearance.Options.UseFont = true;
            this.lblTo.Location = new System.Drawing.Point(20, 70);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(24, 17);
            this.lblTo.TabIndex = 0;
            this.lblTo.Text = "An:";

            this.edtTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtTo.Location = new System.Drawing.Point(95, 66);
            this.edtTo.Name = "edtTo";
            this.edtTo.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.edtTo.Properties.Appearance.Options.UseFont = true;
            this.edtTo.Properties.EditMode = DevExpress.XtraEditors.TokenEditMode.Manual;
            this.edtTo.Properties.TokenGlyphLocation = DevExpress.XtraEditors.TokenEditGlyphLocation.Right;
            this.edtTo.Properties.AutoHeight = false;
            this.edtTo.Properties.MaxExpandLines = 1;
            this.edtTo.Properties.DropDownRowCount = 8;
            this.edtTo.Size = new System.Drawing.Size(920, 44);
            this.edtTo.TabIndex = 1;

            // CC (Chips/Token + Dropdown)
            this.lblCc.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCc.Appearance.Options.UseFont = true;
            this.lblCc.Location = new System.Drawing.Point(20, 124);
            this.lblCc.Name = "lblCc";
            this.lblCc.Size = new System.Drawing.Size(24, 17);
            this.lblCc.TabIndex = 0;
            this.lblCc.Text = "CC:";

            this.edtCc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtCc.Location = new System.Drawing.Point(95, 120);
            this.edtCc.Name = "edtCc";
            this.edtCc.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.edtCc.Properties.Appearance.Options.UseFont = true;
            this.edtCc.Properties.EditMode = DevExpress.XtraEditors.TokenEditMode.Manual;
            this.edtCc.Properties.TokenGlyphLocation = DevExpress.XtraEditors.TokenEditGlyphLocation.Right;
            this.edtCc.Properties.AutoHeight = false;
            this.edtCc.Properties.MaxExpandLines = 1;
            this.edtCc.Properties.DropDownRowCount = 8;
            this.edtCc.Size = new System.Drawing.Size(920, 44);
            this.edtCc.TabIndex = 2;

            // Betreff
            this.lblSubject.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubject.Appearance.Options.UseFont = true;
            this.lblSubject.Location = new System.Drawing.Point(20, 176);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(47, 17);
            this.lblSubject.TabIndex = 0;
            this.lblSubject.Text = "Betreff:";

            this.edtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtSubject.Location = new System.Drawing.Point(95, 172);
            this.edtSubject.Name = "edtSubject";
            this.edtSubject.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.edtSubject.Properties.Appearance.Options.UseFont = true;
            this.edtSubject.Properties.NullValuePrompt = "Betreff";
            this.edtSubject.Size = new System.Drawing.Size(920, 38);
            this.edtSubject.TabIndex = 3;

            //
            // ========== SMTP FOOTER ==========
            //
            this.smtpFooter.Controls.Add(this.lblSmtpServer);
            this.smtpFooter.Controls.Add(this.edtSmtpServer);
            this.smtpFooter.Controls.Add(this.lblSmtpPort);
            this.smtpFooter.Controls.Add(this.edtSmtpPort);
            this.smtpFooter.Controls.Add(this.lblSmtpPassword);
            this.smtpFooter.Controls.Add(this.edtSmtpPassword);
            this.smtpFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smtpFooter.Name = "smtpFooter";
            this.smtpFooter.Size = new System.Drawing.Size(1200, 48);
            this.smtpFooter.TabIndex = 200;

            this.lblSmtpServer.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSmtpServer.Appearance.Options.UseFont = true;
            this.lblSmtpServer.Location = new System.Drawing.Point(12, 16);
            this.lblSmtpServer.Name = "lblSmtpServer";
            this.lblSmtpServer.Size = new System.Drawing.Size(75, 15);
            this.lblSmtpServer.TabIndex = 0;
            this.lblSmtpServer.Text = "SMTP-Server:";

            this.edtSmtpServer.EditValue = "smtp.gmail.com";
            this.edtSmtpServer.Location = new System.Drawing.Point(95, 12);
            this.edtSmtpServer.Name = "edtSmtpServer";
            this.edtSmtpServer.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.edtSmtpServer.Properties.Appearance.Options.UseFont = true;
            this.edtSmtpServer.Size = new System.Drawing.Size(250, 22);
            this.edtSmtpServer.TabIndex = 1;

            this.lblSmtpPort.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSmtpPort.Appearance.Options.UseFont = true;
            this.lblSmtpPort.Location = new System.Drawing.Point(360, 16);
            this.lblSmtpPort.Name = "lblSmtpPort";
            this.lblSmtpPort.Size = new System.Drawing.Size(28, 15);
            this.lblSmtpPort.TabIndex = 0;
            this.lblSmtpPort.Text = "Port:";

            this.edtSmtpPort.EditValue = "587";
            this.edtSmtpPort.Location = new System.Drawing.Point(395, 12);
            this.edtSmtpPort.Name = "edtSmtpPort";
            this.edtSmtpPort.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.edtSmtpPort.Properties.Appearance.Options.UseFont = true;
            this.edtSmtpPort.Size = new System.Drawing.Size(55, 22);
            this.edtSmtpPort.TabIndex = 2;

            this.lblSmtpPassword.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSmtpPassword.Appearance.Options.UseFont = true;
            this.lblSmtpPassword.Location = new System.Drawing.Point(470, 16);
            this.lblSmtpPassword.Name = "lblSmtpPassword";
            this.lblSmtpPassword.Size = new System.Drawing.Size(72, 15);
            this.lblSmtpPassword.TabIndex = 0;
            this.lblSmtpPassword.Text = "App-Passwort:";

            this.edtSmtpPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtSmtpPassword.Location = new System.Drawing.Point(550, 12);
            this.edtSmtpPassword.Name = "edtSmtpPassword";
            this.edtSmtpPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.edtSmtpPassword.Properties.Appearance.Options.UseFont = true;
            this.edtSmtpPassword.Properties.NullValuePrompt = "App-Passwort eingeben";
            this.edtSmtpPassword.Properties.PasswordChar = '●';
            this.edtSmtpPassword.Properties.UseSystemPasswordChar = false;
            this.edtSmtpPassword.Size = new System.Drawing.Size(620, 22);
            this.edtSmtpPassword.TabIndex = 3;

            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.richEdit);
            this.Controls.Add(this.sepBelowAttachments);
            this.Controls.Add(this.groupAttachments);
            this.Controls.Add(this.smtpFooter);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.toolbarPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mail Editor";

            ((System.ComponentModel.ISupportInitialize)(this.toolbarPanel)).EndInit();
            this.toolbarPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboFont.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorFont.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupAttachments)).EndInit();
            this.groupAttachments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.edtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtCc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomSeparator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sepBelowAttachments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSmtpServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSmtpPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSmtpPassword.Properties)).EndInit();
            this.smtpFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smtpFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);
        }

        DevExpress.XtraRichEdit.RichEditControl richEdit;
        #endregion

        // Toolbar
        private DevExpress.XtraEditors.PanelControl toolbarPanel;
        private DevExpress.XtraEditors.SimpleButton btnBold;
        private DevExpress.XtraEditors.SimpleButton btnItalic;
        private DevExpress.XtraEditors.SimpleButton btnUnderline;
        private DevExpress.XtraEditors.SimpleButton btnStrike;
        private DevExpress.XtraEditors.ComboBoxEdit cboFont;
        private DevExpress.XtraEditors.ComboBoxEdit cboSize;
        private DevExpress.XtraEditors.ColorPickEdit colorFont;
        private DevExpress.XtraEditors.SimpleButton btnAlignLeft;
        private DevExpress.XtraEditors.SimpleButton btnAlignCenter;
        private DevExpress.XtraEditors.SimpleButton btnAlignRight;
        private DevExpress.XtraEditors.SimpleButton btnAlignJustify;
        private DevExpress.XtraEditors.SimpleButton btnBulletList;
        private DevExpress.XtraEditors.SimpleButton btnNumberedList;
        private DevExpress.XtraEditors.SimpleButton btnPicture;
        private DevExpress.XtraEditors.SimpleButton btnHyperlink;

        // Anhänge
        private DevExpress.XtraEditors.GroupControl groupAttachments;
        private System.Windows.Forms.FlowLayoutPanel attachmentsFlow;
        private DevExpress.XtraEditors.SimpleButton btnAddAttachment;

        // Mail
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl bottomSeparator;
        private DevExpress.XtraEditors.PanelControl sepBelowAttachments;

        // SMTP Footer
        private DevExpress.XtraEditors.PanelControl smtpFooter;
        private DevExpress.XtraEditors.LabelControl lblSmtpServer;
        private DevExpress.XtraEditors.TextEdit edtSmtpServer;
        private DevExpress.XtraEditors.LabelControl lblSmtpPort;
        private DevExpress.XtraEditors.TextEdit edtSmtpPort;
        private DevExpress.XtraEditors.LabelControl lblSmtpPassword;
        private DevExpress.XtraEditors.TextEdit edtSmtpPassword;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraEditors.LabelControl lblFrom;
        private DevExpress.XtraEditors.TextEdit edtFrom;
        private DevExpress.XtraEditors.LabelControl lblTo;
        private DevExpress.XtraEditors.TokenEdit edtTo;
        private DevExpress.XtraEditors.LabelControl lblCc;
        private DevExpress.XtraEditors.TokenEdit edtCc;
        private DevExpress.XtraEditors.LabelControl lblSubject;
        private DevExpress.XtraEditors.TextEdit edtSubject;
    }
}
