using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MyB2B.Domain.Invoice;
using Org.BouncyCastle.Crypto.Paddings;

namespace MyB2B.Server.Documents.Generators
{
    public class PdfInvoiceGenerator : IInvoiceGenerator
    {
        private readonly MemoryStream _documentMemoryStream;
        private readonly PdfWriter _pdfWriter;
        private readonly PdfDocument _pdfDocument;
        private Document _currentDocument;
        private byte[] _invoiceTemplate;

        public PdfInvoiceGenerator()
        {
            _documentMemoryStream = new MemoryStream();
           _pdfWriter = new PdfWriter(_documentMemoryStream);
           _pdfDocument = new PdfDocument(_pdfWriter);
           _currentDocument = new Document(_pdfDocument, PageSize.A4);
        }

        public byte[] Generate(Invoice invoice)
        {
            _invoiceTemplate = invoice.Template;

            AddNewPage();
            _currentDocument.Add(new Paragraph("FAKTURA VAT")
                .SetFontSize(25)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetTextAlignment(TextAlignment.CENTER));

            _currentDocument.Close();

            return _documentMemoryStream.ToArray();
        }

        private void AddNewPage()
        {
            if (_invoiceTemplate != null)
            {
                CreatePageWithTemplate(_pdfDocument, _invoiceTemplate);
            }
            else
            {
                _currentDocument.GetPdfDocument().AddNewPage();
            }
        }

        private void CreatePageWithTemplate(PdfDocument document, byte[] template)
        {
            var canvas = new PdfCanvas(document.AddNewPage().NewContentStreamBefore(), document.GetLastPage().GetResources(), document);
            canvas.AddImage(ImageDataFactory.Create(template), PageSize.A4.Clone(), false);
        }
    }
}
