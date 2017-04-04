namespace BlueTapeCrew.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            CategoryImages = new HashSet<CategoryImage>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        public int? ImageId { get; set; }

        [StringLength(2083)]
        public string ImageUrl { get; set; }

        public bool Published { get; set; }

        public virtual Image Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryImage> CategoryImages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
