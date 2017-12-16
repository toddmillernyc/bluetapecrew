using System.Collections.Generic;

namespace BlueTapeCrew.Paypal.Models
{
    public class Payment
    {
        public Payment()
        {
            Transactions = new List<Transaction>();
        }

        public Payment(string  subtotal, string noteToPayer, RedirectUrls redirectUrls)
        {
            var transaction = new Transaction();

            Intent = "sale";
            Payer = new Payer("paypal");
            Transactions.Add(transaction);
            NoteToPayer = noteToPayer;
            RedirectUrls = redirectUrls;
        }

        public string Intent { get; set; }
        public Payer Payer { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public string NoteToPayer { get; set; }
        public RedirectUrls RedirectUrls { get; set; }
    }
}