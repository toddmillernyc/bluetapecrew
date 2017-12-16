namespace BlueTapeCrew.Paypal.Models
{
    public class ShippingAddress
    {
        public string RecipientName { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
    }
}