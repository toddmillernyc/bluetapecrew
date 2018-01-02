using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlueTapeCrew.Models.Entities
{
    public class Size
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Size()
        {
            Styles = new HashSet<Style>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string SizeText { get; set; }

        public int SizeOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Style> Styles { get; set; }
    }
}
