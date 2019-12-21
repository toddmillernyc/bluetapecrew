using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueTapeCrew.Models.Entities
{
    [Table("Images")]
    public class Image
    {
        public Image()
        {
            Categories = new HashSet<Category>();
            ProductImages = new HashSet<ProductImage>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public string MimeType { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
