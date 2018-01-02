using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueTapeCrew.Models.Entities
{
    public class PageOpenGraphTag
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PageId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OpenGraphTagId { get; set; }

        [Required]
        public string Content { get; set; }

        public virtual OpenGraphTag OpenGraphTag { get; set; }

        public virtual Page Page { get; set; }
    }
}
