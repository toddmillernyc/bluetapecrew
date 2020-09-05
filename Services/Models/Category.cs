using System.Collections.Generic;

namespace Services.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ImageId { get; set; }
        public bool Published { get; set; }
        public Image Image { get; set; }
        public int Position { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
