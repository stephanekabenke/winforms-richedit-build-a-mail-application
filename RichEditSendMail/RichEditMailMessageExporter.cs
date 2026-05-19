using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using DevExpress.Office.Services;
using DevExpress.Office.Utils;
using DevExpress.Utils;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit.Export.Html;

namespace RichEditSendMail
{
    // Exportiert den RichEdit-Inhalt als HTML-Mailbody und bettet
    // enthaltene Bilder als Inline-Anhaenge (cid:) ein.
    public class RichEditMailMessageExporter : IUriProvider
    {
        readonly RichEditControl control;
        readonly MailMessage message;
        List<AttachementInfo> attachments;
        int imageId;

        public RichEditMailMessageExporter(RichEditControl control, MailMessage message)
        {
            Guard.ArgumentNotNull(control, "control");
            Guard.ArgumentNotNull(message, "message");

            this.control = control;
            this.message = message;
        }

        public virtual void Export()
        {
            this.attachments = new List<AttachementInfo>();

            AlternateView htmlView = CreateHtmlView();
            message.AlternateViews.Add(htmlView);
            message.IsBodyHtml = true;
        }

        protected internal virtual AlternateView CreateHtmlView()
        {
            control.BeforeExport += OnBeforeExport;
            string htmlBody = control.Document.GetHtmlText(control.Document.Range, this);
            AlternateView view = AlternateView.CreateAlternateViewFromString(htmlBody, Encoding.UTF8, MediaTypeNames.Text.Html);
            control.BeforeExport -= OnBeforeExport;

            int count = attachments.Count;
            for (int i = 0; i < count; i++)
            {
                AttachementInfo info = attachments[i];
                LinkedResource resource = new LinkedResource(info.Stream, info.MimeType);
                resource.ContentId = info.ContentId;
                view.LinkedResources.Add(resource);
            }
            return view;
        }

        void OnBeforeExport(object sender, BeforeExportEventArgs e)
        {
            HtmlDocumentExporterOptions options = e.Options as HtmlDocumentExporterOptions;
            if (options != null)
            {
                options.Encoding = Encoding.UTF8;
            }
        }

        #region IUriProvider Members

        public string CreateCssUri(string rootUri, string styleText, string relativeUri)
        {
            return String.Empty;
        }

        public string CreateImageUri(string rootUri, OfficeImage image, string relativeUri)
        {
            string imageName = String.Format("image{0}", imageId);
            imageId++;

            OfficeImageFormat imageFormat = GetActualImageFormat(image.RawFormat);
            Stream stream = new MemoryStream(image.GetImageBytes(imageFormat));
            string mediaContentType = OfficeImage.GetContentType(imageFormat);
            AttachementInfo info = new AttachementInfo(stream, mediaContentType, imageName);
            attachments.Add(info);

            return "cid:" + imageName;
        }

        OfficeImageFormat GetActualImageFormat(OfficeImageFormat _officeImageFormat)
        {
            if (_officeImageFormat == OfficeImageFormat.Exif ||
                _officeImageFormat == OfficeImageFormat.MemoryBmp)
                return OfficeImageFormat.Png;
            else
                return _officeImageFormat;
        }
        #endregion
    }
}
