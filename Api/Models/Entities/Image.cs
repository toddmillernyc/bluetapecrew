using System.Collections.Generic;

namespace Api.Models.Entities
{
    public class Image
    {
        public Image()
        {
            Categories = new HashSet<Categories>();
            ProductImages = new HashSet<ProductImage>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public string MimeType { get; set; }
        public short? Width { get; set; }
        public short? Height { get; set; }

        public virtual ICollection<Categories> Categories { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
