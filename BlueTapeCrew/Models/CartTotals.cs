using System.ComponentModel.DataAnnotations;

namespace BlueTapeCrew.Models
{
    public class CartTotals
    {
        public int Count { get; set; }

        [DataType(DataType.Currency)]
        public decimal SubTotal { get; set; }

        [DataType(DataType.Currency)]
        public decimal Shipping { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
    }
}