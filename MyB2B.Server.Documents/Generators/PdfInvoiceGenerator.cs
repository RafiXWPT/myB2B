using System;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using MyB2B.Domain.Invoices;
using MyB2B.Server.Common;
using MyB2B.Server.Documents.Extensions;

namespace MyB2B.Server.Documents.Generators
{
    public class PdfInvoiceGenerator : IInvoiceGenerator
    {
        private readonly MemoryStream _documentMemoryStream;
        private readonly PdfWriter _pdfWriter;
        private readonly PdfDocument _pdfDocument;
        private Document _currentDocument;
        private byte[] _invoiceTemplate;

        private PdfFont _regularFont = PdfFontFactory.CreateFont("Fonts/Calibri.ttf", "CP1250", true);
        private PdfFont _boldFont = PdfFontFactory.CreateFont("Fonts/CALIBRIB.ttf", "CP1250", true);
        private PdfFont _italicsFont = PdfFontFactory.CreateFont("Fonts/CALIBRII.ttf", "CP1250", true);
        private PdfFont _boldedItalicsFont = PdfFontFactory.CreateFont("Fonts/CALIBRIZ.ttf", "CP1250", true);

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
            _currentDocument.SetFont(_regularFont);

            AddNewPage();
            _currentDocument.Add(new Paragraph().Add(new Text($"FAKTURA VAT ")).Add(new Text($"{invoice.Number}").SetFont(_boldFont))
                .SetMargin(0)
                .SetPadding(0)
                .SetFontSize(15)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetTextAlignment(TextAlignment.CENTER));

            var documentDatesParagraph = new Paragraph().SetPadding(0).SetMargin(0).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10);
            documentDatesParagraph.Add($"Data wystawienia dokumentu: {invoice.GeneratedAt:yyyy-MM-dd}");
            documentDatesParagraph.AddNewLine($"Data wykonania usługi: {invoice.CreatedAt:yyyy-MM-dd}");

            _currentDocument.Add(documentDatesParagraph);

            _currentDocument.AddEmptyLine();
            _currentDocument.AddLineSeparator();
            GenerateParticipantsSection(invoice);
            _currentDocument.AddLineSeparator();
            GeneratePaymentSection(invoice);
            _currentDocument.AddLineSeparator();
            GenerateItemsSection(invoice);
            GenerateItemsSummary(invoice);
            _currentDocument.AddLineSeparator();
            GenerateLeftToPaymentSummary(invoice);

            _currentDocument.AddEmptyLine(5);
            GenerateSignaturesSummary(invoice);

            _currentDocument.Close();

            return _documentMemoryStream.ToArray();
        }

        private void GenerateParticipantsSection(Invoice invoice)
        {
            var invoiceParticipantsTable = new Table(new []{80, 220f, 60, 240f}).SetBorder(Border.NO_BORDER);

            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, "SPRZEDAWCA:");
            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, new Paragraph($"{invoice.DealerCompany} - {invoice.DealerName}").SetFont(_boldFont));
            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, "NABYWCA:");
            if (!string.IsNullOrEmpty(invoice.BuyerName) && !string.IsNullOrEmpty(invoice.BuyerCompany))
            {
                invoiceParticipantsTable.AddNoBorderTextCell(1, 1, new Paragraph($"{invoice.BuyerCompany} - {invoice.BuyerName}").SetFont(_boldFont));
            }
            else if (!string.IsNullOrEmpty(invoice.BuyerCompany) && string.IsNullOrEmpty(invoice.BuyerName))
            {
                invoiceParticipantsTable.AddNoBorderTextCell(1, 1, new Paragraph($"{invoice.BuyerCompany}").SetFont(_boldFont));
            }
            else
            {
                invoiceParticipantsTable.AddNoBorderTextCell(1, 1, new Paragraph($"{invoice.BuyerName}").SetFont(_boldFont));
            }

            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, "");
            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, $"{invoice.DealerAddress.GetStreetAndNumber()}");
            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, "");
            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, $"{invoice.BuyerAddress.GetStreetAndNumber()}");

            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, "");
            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, $"{invoice.DealerAddress.GetZipAndCity()}");
            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, "");
            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, $"{invoice.BuyerAddress.GetZipAndCity()}");

            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, "");
            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, $"{invoice.DealerAddress.Country}");
            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, "");
            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, $"{invoice.BuyerAddress.Country}");

            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, "");
            invoiceParticipantsTable.AddNoBorderTextCell(1, 1, $"NIP: {invoice.DealerNip}");
            if (!string.IsNullOrEmpty(invoice.BuyerNip))
            {
                invoiceParticipantsTable.AddNoBorderTextCell(1, 1, "");
                invoiceParticipantsTable.AddNoBorderTextCell(1, 1, $"NIP: {invoice.BuyerNip}");
            }

            _currentDocument.Add(invoiceParticipantsTable);
            _currentDocument.AddEmptyLine();
        }

        private void GeneratePaymentSection(Invoice invoice)
        {
            var documentPaymentParagraph = new Paragraph().SetPadding(0).SetMargin(0).SetFontSize(10);
            documentPaymentParagraph.Add($"Forma płatności: ");
            documentPaymentParagraph.Add(new Text($"{invoice.PaymentMethod.GetDisplayName()}").SetFont(_boldFont));
            documentPaymentParagraph.AddNewLine($"Termin płatności: {(int)(invoice.PaymentToDate-invoice.CreatedAt).TotalDays} dni ({invoice.PaymentToDate:yyyy-MM-dd})");
            if (invoice.PaymentMethod == PaymentMethod.BankTransfer)
            {
                documentPaymentParagraph.AddNewLine($"Na rachunek: ");
                documentPaymentParagraph.Add(new Text($"{invoice.PaymentBankAccount}").SetFont(_boldFont));
                documentPaymentParagraph.Add(" - ");
                documentPaymentParagraph.Add(new Text($"{invoice.PaymentBankName}").SetFont(_boldFont));
            }

            _currentDocument.Add(documentPaymentParagraph);
            _currentDocument.AddEmptyLine();
        }

        private void GenerateItemsSection(Invoice invoice)
        {
            var itemsTable = new Table(new []{20, 300, 20, 50, 50, 30, 50, 50f});
            var headerFontSize = 8;
            itemsTable.AddHeaderCell("Lp").SetFontSize(headerFontSize);
            itemsTable.AddHeaderCell("Towar / usługa").SetFontSize(headerFontSize);
            itemsTable.AddHeaderCell("Ilość").SetFontSize(headerFontSize);
            itemsTable.AddHeaderCell("Cena netto").SetFontSize(headerFontSize);
            itemsTable.AddHeaderCell("Wartość netto").SetFontSize(headerFontSize);
            itemsTable.AddHeaderCell("VAT%").SetFontSize(headerFontSize);
            itemsTable.AddHeaderCell("Kwota VAT").SetFontSize(headerFontSize);
            itemsTable.AddHeaderCell("Wartość brutto").SetFontSize(headerFontSize);

            var i = 0;
            foreach (var item in invoice.Items)
            {
                itemsTable.AddTextCell(1, 1, $"{++i}");
                itemsTable.AddTextCell(1, 1, $"{item.Product.Name}");
                itemsTable.AddTextCell(1, 1, $"{item.Quantity}", TextAlignment.RIGHT);
                itemsTable.AddTextCell(1, 1, $"{item.Product.NetPrice:C}", TextAlignment.RIGHT);
                itemsTable.AddTextCell(1, 1, $"{item.TotalNetAmount:C}", TextAlignment.RIGHT);
                itemsTable.AddTextCell(1, 1, $"{item.Product.VatRate}", TextAlignment.RIGHT);
                itemsTable.AddTextCell(1, 1, $"{item.TotalTaxAmount:C}", TextAlignment.RIGHT);
                itemsTable.AddTextCell(1, 1, $"{item.TotalGrossAmount:C}", TextAlignment.RIGHT);
            }

            itemsTable.AddNoBorderTextCell(1, 7, "Ogółem:", TextAlignment.RIGHT);
            itemsTable.AddTextCell(1, 1, $"{invoice.TotalGrossAmount:C}", TextAlignment.RIGHT);

            _currentDocument.Add(itemsTable);
            _currentDocument.AddEmptyLine();
        }

        private void GenerateItemsSummary(Invoice invoice)
        {
            _currentDocument.Add(new Paragraph($"Do zapłaty: {invoice.TotalGrossAmount:C}").SetMargin(0).SetPadding(0).SetFontSize(20));
            var plnValue = Math.Floor(invoice.TotalGrossAmount);
            var pennyValue = Math.Floor((invoice.TotalGrossAmount - plnValue)*100);
            var summaryParagraph = new Paragraph("Słownie: ").SetFont(_italicsFont).SetMargin(0).SetPadding(0).SetFontSize(10);
            if (plnValue > 0)
            {
                summaryParagraph.Add($"{LiczbyNaSlowaNETCore.NumberToText.Convert(plnValue)} złotych");
            }
            if (pennyValue > 0)
            {
                if (plnValue > 0)
                {
                    summaryParagraph.Add(" oraz ");
                }
                summaryParagraph.Add($"{LiczbyNaSlowaNETCore.NumberToText.Convert(pennyValue)} {(pennyValue%10 > 0 && pennyValue%10 < 5 ? "grosze" : "groszy")}");
            }
            _currentDocument.Add(summaryParagraph);
        }

        private void GenerateLeftToPaymentSummary(Invoice invoice)
        {
            var leftToPayParagraph = new Paragraph().SetMargin(0).SetPadding(0).SetFontSize(20);
            leftToPayParagraph.Add($"Pozostało do zapłaty: {invoice.TotalGrossAmount - invoice.PaidAmount:C}");

            _currentDocument.Add(leftToPayParagraph);
        }

        private void GenerateSignaturesSummary(Invoice invoice)
        {
            var signaturePlace = "..................................................";
            var dealerFooter = new Paragraph("Podpis osoby wystawiającej dokument").SetFontSize(10).SetFont(_italicsFont);
            var buyerFooter = new Paragraph("Podpis osoby uprawnionej do odbioru dokumentu").SetFontSize(10).SetFont(_italicsFont);

            var signatureTable = new Table(2, true);
            signatureTable.AddNoBorderTextCell(1, 1, signaturePlace);
            signatureTable.AddNoBorderTextCell(1, 1, signaturePlace, TextAlignment.RIGHT);
            signatureTable.AddNoBorderTextCell(1, 1, dealerFooter);
            signatureTable.AddNoBorderTextCell(1, 1, buyerFooter, TextAlignment.RIGHT);

            _currentDocument.Add(signatureTable);
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
