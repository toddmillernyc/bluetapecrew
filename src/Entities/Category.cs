using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Categories")]
    public class Category
    {
        public Category()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int Id { get; set; }

        [Column("CategoryName")] //todo: update in database
        public string Name { get; set; }

        public int? ImageId { get; set; }
        public bool Published { get; set; }

        public int Position { get; set; }

        public virtual Image Image { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
