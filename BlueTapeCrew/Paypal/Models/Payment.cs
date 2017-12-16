using System.Collections.Generic;

namespace BlueTapeCrew.Paypal.Models
{
    public class Payment
    {
        public Payment()
        {
            Transactions = new List<Transaction>();
        }

        public string Intent { get; set; }
        public Payer Payer { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public string NoteToPayer { get; set; }
        public RedirectUrls RedirectUrls { get; set; }
    }
}