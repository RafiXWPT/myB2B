using System;
using System.Collections.Generic;
using System.Text;
using MyB2B.Domain;
using MyB2B.Domain.Companies;
using MyB2B.Domain.Invoices;

namespace MyB2B.SampleObjects
{
    public static class Samples
    {
        public static Invoice SampleInvoice(string templatePath) => new Invoice()
        {
            Number = "0001/RP/SQS/03/2019",
            Template = string.IsNullOrEmpty(templatePath) ? null : System.IO.File.ReadAllBytes(templatePath),
            GeneratedAt = DateTime.Now,
            CreatedAt = DateTime.Now,

            DealerName = "Jan Nowak",
            DealerCompany = "IT Solutions",
            DealerNip = "5224051418",
            DealerAddress = new Address
            {
                City = "Kraków",
                Country = "Polska",
                Number = "123A/45",
                Street = "Krakowska",
                ZipCode = "31-123"
            },

            BuyerCompany = "Nowaks S.A.",
            BuyerNip = "3942739741",
            BuyerAddress = new Address
            {
                City = "Kraków",
                Country = "Polska",
                Number = "20",
                Street = "Aleje Jana Pawła II",
                ZipCode = "31-321"
            },

            PaymentMethod = PaymentMethod.BankTransfer,
            PaymentToDate = DateTime.Now.AddDays(7),
            PaymentBankAccount = "22 1230 3046 0100 5301 6731 4462",
            PaymentBankName = "mBank",

            Items = new List<InvoicePosition>
                {
                    new InvoicePosition
                    {
                        Product = new CompanyProduct
                        {
                            Name = "Usługi w zakresie produkcji i utrzymania systemów informatycznych",
                            NetPrice = 10000,
                            VatRate = 23
                        },
                        Quantity = 1,
                        TotalNetAmount = 10000,
                        TotalGrossAmount = 12300,
                        TotalTaxAmount = 2300
                    },
                    new InvoicePosition
                    {
                        Product = new CompanyProduct
                        {
                            Name = "Koszty dodatkowe",
                            NetPrice = 1000,
                            VatRate = 23
                        },
                        Quantity = 1,
                        TotalNetAmount = 1000,
                        TotalGrossAmount = 1230,
                        TotalTaxAmount = 230
                    },
                    new InvoicePosition
                    {
                        Product = new CompanyProduct
                        {
                            Name = "Kabel LAN",
                            NetPrice = 100,
                            VatRate = 23
                        },
                        Quantity = 6,
                        TotalNetAmount = 100*6,
                        TotalGrossAmount = 123*6,
                        TotalTaxAmount = 23*6
                    }
                },
            TotalGrossAmount = 14268m
        };
    }
}
