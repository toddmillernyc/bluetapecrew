using System.ComponentModel.DataAnnotations.Schema;

namespace BlueTapeCrew.Models.Entities
{
    [Table("ProductCategories")]
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}
