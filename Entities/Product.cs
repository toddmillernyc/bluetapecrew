using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Products")]
    public class Product
    {
        public Product()
        {
            CartImages = new HashSet<CartImage>();
            ProductCategories = new HashSet<ProductCategory>();
            ProductImages = new HashSet<ProductImage>();
            Reviews = new HashSet<Review>();
            Styles = new HashSet<Style>();
        }

        public int Id { get; set; }
        public int? ImageId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }

        [Column("LinkName")] //todo: update in database
        public string Slug { get; set; }

        public virtual Image Image { get; set; }
        public virtual ICollection<CartImage> CartImages { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Style> Styles { get; set; }
    }
}
