using System.Collections.Generic;

namespace Services.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int? ImageId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string LinkName { get; set; }
        public Image Image { get; set; }
        public IEnumerable<CartImage> CartImages { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Style> Styles { get; set; }
        public string Slug { get; set; }
    }
}
