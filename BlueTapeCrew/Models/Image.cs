namespace BlueTapeCrew.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Image()
        {
            Categories = new HashSet<Category>();
            Products = new HashSet<Product>();
            Products1 = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public byte[] ImageData { get; set; }

        [Required]
        [StringLength(255)]
        public string MimeType { get; set; }

        public short? Width { get; set; }

        public short? Height { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products1 { get; set; }
    }
}
