namespace BlueTapeCrew.Models
{
    using System.ComponentModel.DataAnnotations;

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
