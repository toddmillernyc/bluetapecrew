using System.Collections.Generic;

namespace Api.Models.Entities
{
    public class Categories
    {
        public Categories()
        {
            CategoryImages = new HashSet<CategoryImages>();
            ProductCategories = new HashSet<ProductCategories>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int? ImageId { get; set; }
        public string ImageUrl { get; set; }
        public bool Published { get; set; }

        public virtual Image Image { get; set; }
        public virtual ICollection<CategoryImages> CategoryImages { get; set; }
        public virtual ICollection<ProductCategories> ProductCategories { get; set; }
    }
}
