namespace Api.Models.Entities
{
    public class OrderItems
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? SubTotal { get; set; }

        public virtual Orders Order { get; set; }
    }
}
