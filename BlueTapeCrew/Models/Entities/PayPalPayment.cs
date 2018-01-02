using System.ComponentModel.DataAnnotations;

namespace BlueTapeCrew.Models.Entities
{
    public class PayPalPayment
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Tx { get; set; }

        [StringLength(255)]
        public string Amt { get; set; }

        [StringLength(255)]
        public string Cc { get; set; }
    }
}
