using MyB2B.Server.Documents.Generators;
using MyB2B.Web.Controllers.Logic.Invoice;
using NUnit.Framework;

namespace MyB2B.Web.Controllers.Logic.Tests.Invoice
{
    [TestFixture]
    public class InvoiceLogicTests : LogicTest<InvoiceLogic>
    {
        protected override void AdditionalRegistration()
        {
            RegisterService<IInvoiceGenerator, PdfInvoiceGenerator>();
        }

        [Test]
        public void can_generate_invoice()
        {
            BeginTest(() =>
            {
                Component.GenerateInvoice(0);
            });
        }

        [Test]
        public void can_download_generated_invoice()
        {
            BeginTest(() =>
            {
                Component.GenerateInvoice(0);
                var invoice = FakeInvoiceDb.GeneratedInvoice;

                Assert.IsTrue(invoice.Length > 0);
            });
        }
    }
}
