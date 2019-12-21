using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueTapeCrew.Models.Entities
{
    [Table("Categories")]
    public class Category
    {
        public Category()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int? ImageId { get; set; }
        public bool Published { get; set; }

        public virtual Image Image { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
