using System.ComponentModel.DataAnnotations;

namespace MyB2B.Domain.Invoices
{
    public enum PaymentMethod
    {
        [Display(Name = "Got�wka")]
        Cash = 100,

        [Display(Name = "Przelew bankowy")]
        BankTransfer = 200,

        [Display(Name = "Karta")]
        CreditCard = 300
    }
}