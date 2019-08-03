namespace MyB2B.Web.Controllers.Logic.Invoice.Models
{
    public class InvoiceFileDto
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] FileContent { get; set; }
    }
}
