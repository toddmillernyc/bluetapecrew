namespace BlueTapeCrew.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            AzImages = new HashSet<AzImage>();
            CartImages = new HashSet<CartImage>();
            Reviews = new HashSet<Review>();
            Styles = new HashSet<Style>();
            Categories = new HashSet<Category>();
            Images = new HashSet<Image>();
        }

        public int Id { get; set; }

        public int? ImageId { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [StringLength(255)]
        public string LinkName { get; set; }

        public virtual ICollection<AzImage> AzImages { get; set; }

        public virtual ICollection<CartImage> CartImages { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Style> Styles { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
