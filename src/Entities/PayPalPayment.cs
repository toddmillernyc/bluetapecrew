using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    //todo: remove if not used & remove table from db
    [Table("PayPalPayments")]
    public class PayPalPayment
    {
        public int Id { get; set; }
        public string Tx { get; set; }
        public string Amt { get; set; }
        public string Cc { get; set; }
    }
}
