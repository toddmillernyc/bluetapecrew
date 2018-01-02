using System.ComponentModel.DataAnnotations.Schema;

namespace BlueTapeCrew.Models.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string Description { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? Price { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? SubTotal { get; set; }

        public virtual Order Order { get; set; }
    }
}
