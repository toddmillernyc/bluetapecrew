namespace Services.Models
{
    public class CartView
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }
        public int StyleId { get; set; }
        public string ColorText { get; set; }
        public string Description { get; set; }
        public string StyleText { get; set; }
        public decimal? SubTotal { get; set; }
        public byte[] ImageData { get; set; }
    }
}
