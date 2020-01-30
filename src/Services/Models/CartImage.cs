namespace Services.Models
{
    public class CartImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public byte[] ImageData { get; set; }
        public Product Product { get; set; }
    }
}
