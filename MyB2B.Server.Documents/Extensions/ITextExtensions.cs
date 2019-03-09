using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace MyB2B.Server.Documents.Extensions
{
    public static class ITextExtensions
    {
        public static void AddEmptyLine(this Document document, int times = 1)
        {
            for(var i = 0; i < times; i++)
                document.Add(new Paragraph(""));
        }

        public static void AddLineSeparator(this Document document, int size = 1)
        {
            document.Add(new LineSeparator(new SolidLine(size)));
        }

        public static Paragraph AddNewLine(this Paragraph paragraph, string content)
        {
            return paragraph.Add($"\n{content}");
        }

        public static Paragraph AddSplitLine(this Paragraph paragraph, string leftContent, string rightContent)
        {
            paragraph.Add(leftContent);
            paragraph.Add(new Tab());
            paragraph.AddTabStops(new TabStop(1000, TabAlignment.RIGHT));
            paragraph.Add(rightContent);

            return paragraph;
        }

        public static Table AddTextCell(this Table table, int rows, int cols, string content)
        {
            return AddTextCell(table, rows, cols, content, TextAlignment.LEFT, true);
        }

        public static Table AddTextCell(this Table table, int rows, int cols, Paragraph paragraph)
        {
            return AddTextCell(table, rows, cols, paragraph, TextAlignment.LEFT, true);
        }

        public static Table AddTextCell(this Table table, int rows, int cols, string content, TextAlignment alignment)
        {
            return AddTextCell(table, rows, cols, new Paragraph(content), alignment, true);
        }

        public static Table AddNoBorderTextCell(this Table table, int rows, int cols, string content)
        {
            return AddTextCell(table, rows, cols, content, TextAlignment.LEFT, false);
        }

        public static Table AddNoBorderTextCell(this Table table, int rows, int cols, string content, TextAlignment alignment)
        {
            return AddTextCell(table, rows, cols, content, alignment, false);
        }

        public static Table AddNoBorderTextCell(this Table table, int rows, int cols, Paragraph paragraph)
        {
            return AddTextCell(table, rows, cols, paragraph, TextAlignment.LEFT, false);
        }

        public static Table AddNoBorderTextCell(this Table table, int rows, int cols, Paragraph paragraph, TextAlignment alignment)
        {
            return AddTextCell(table, rows, cols, paragraph, alignment, false);
        }

        public static Table AddTextCell(this Table table, int rows, int cols, string content, TextAlignment alignment, bool withBorder)
        {
            return AddTextCell(table, rows, cols, new Paragraph(content), alignment, withBorder);
        }

        public static Table AddTextCell(this Table table, int rows, int cols, Paragraph paragraph, TextAlignment alignment, bool withBorder)
        {
            return table.AddCell(new Cell(rows, cols)
                .SetMargin(0)
                .SetPadding(0)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHeight(18)
                .SetBorder(withBorder ? new SolidBorder(1) : Border.NO_BORDER)
                .Add(paragraph)
                .SetTextAlignment(alignment)
                .SetPaddingLeft(alignment == TextAlignment.LEFT ? 3 : 0)
                .SetPaddingRight(alignment == TextAlignment.RIGHT ? 3 : 0)
                .SetMarginLeft(alignment == TextAlignment.LEFT ? 3 : 0)
                .SetMarginRight(alignment == TextAlignment.RIGHT ? 3 : 0));
        }
    }
}