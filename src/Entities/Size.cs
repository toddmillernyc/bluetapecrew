using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Sizes")]
    public class Size
    {
        public Size()
        {
            Styles = new HashSet<Style>();
        }

        public int Id { get; set; }
        public string SizeText { get; set; }
        public int SizeOrder { get; set; }

        public virtual ICollection<Style> Styles { get; set; }
    }
}
