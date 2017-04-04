namespace BlueTapeCrew.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Page
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Page()
        {
            PageOpenGraphTags = new HashSet<PageOpenGraphTag>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageOpenGraphTag> PageOpenGraphTags { get; set; }
    }
}
