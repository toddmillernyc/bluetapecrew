namespace Repositories.Entities
{
    public class ProductImage
    {
        public int ProductId { get; set; }
        public int ImageId { get; set; }

        public virtual Image Image { get; set; }
        public virtual Product Product { get; set; }
    }
}
