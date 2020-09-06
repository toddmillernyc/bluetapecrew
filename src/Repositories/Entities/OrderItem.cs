using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    [Table("OrderItems")]
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? SubTotal { get; set; }

        public virtual Order Order { get; set; }
    }
}
