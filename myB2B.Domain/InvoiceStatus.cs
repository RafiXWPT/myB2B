namespace myB2B.Domain
{
    public enum InvoiceStatus
    {
        PendingCreation = 0,
        WaitingForPayment = 10,
        Paid = 20,
        Cancelled = 30
    }
}