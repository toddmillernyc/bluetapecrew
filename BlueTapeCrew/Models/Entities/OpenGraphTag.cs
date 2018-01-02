using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlueTapeCrew.Models.Entities
{
    public class OpenGraphTag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OpenGraphTag()
        {
            PageOpenGraphTags = new HashSet<PageOpenGraphTag>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Property { get; set; }

        [Required]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageOpenGraphTag> PageOpenGraphTags { get; set; }
    }
}
