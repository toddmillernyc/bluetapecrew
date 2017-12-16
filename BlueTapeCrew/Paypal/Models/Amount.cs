namespace BlueTapeCrew.Paypal.Models
{
    public class Amount
    {
        public string Total { get; set; }
        public string Currency { get; set; }
        public Details Details { get; set; }
    }
}