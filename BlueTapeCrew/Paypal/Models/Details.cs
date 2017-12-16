namespace BlueTapeCrew.Paypal.Models
{
    public class Details
    {
        public string Subtotal { get; set; }
        public string Tax { get; set; }
        public string Shipping { get; set; }
        public string HandlingFee { get; set; }
        public string ShippingDiscount { get; set; }
        public string Insurance { get; set; }
    }
}