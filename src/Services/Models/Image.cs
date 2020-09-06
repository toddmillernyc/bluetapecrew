using System.Collections.Generic;

namespace Services.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public string MimeType { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
