namespace Services.Models
{
    public class ProductImage
    {
        public int ProductId { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public Product Product { get; set; }
    }
}
