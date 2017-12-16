using System;
using System.Collections.Generic;

namespace BlueTapeCrew.Paypal.Models
{
    public class PaymentResponse
    {
        public PaymentResponse()
        {
            Transactions = new List<Transaction>();    
        }

        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string State { get; set; }
        public string Intent { get; set; }
        public Payer Payer { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public List<Link> Links { get; set; }
    }
}