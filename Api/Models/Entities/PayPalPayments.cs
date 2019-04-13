namespace Api.Models.Entities
{
    public class PayPalPayments
    {
        public int Id { get; set; }
        public string Tx { get; set; }
        public string Amt { get; set; }
        public string Cc { get; set; }
    }
}
