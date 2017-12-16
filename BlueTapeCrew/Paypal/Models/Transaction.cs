namespace BlueTapeCrew.Paypal.Models
{
    public class Transaction
    {
        public Amount Amount { get; set; }
        public string Description { get; set; }
        public string Custom { get; set; }
        public string InvoiceNumber { get; set; }
        public PaymentOptions PaymentOptions { get; set; }
        public string SoftDescriptor { get; set; }
        public ItemList ItemList { get; set; }
    }
}