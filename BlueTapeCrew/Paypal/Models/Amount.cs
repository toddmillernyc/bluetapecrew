namespace BlueTapeCrew.Paypal.Models
{
    public class Amount
    {
        public Amount(Details details, string currency = "USD")
        {
            Total =
                Details.Subtotal +
                Details.Tax +
                Details.Shipping +
                Details.HandlingFee +
                Details.ShippingDiscount +
                Details.Insurance;

        }

        public string Total { get; set; }
        public string Currency { get; set; }
        public Details Details { get; set; }
    }
}