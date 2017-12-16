namespace BlueTapeCrew.Paypal.Models
{
    public class Payer
    {
        public Payer(string paymentMethod)
        {
            PaymentMethod = paymentMethod;
        }

        public string PaymentMethod { get; set; }
    }
}