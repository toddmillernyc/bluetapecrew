using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Colors")]
    public class Color
    {
        public Color()
        {
            Styles = new HashSet<Style>();
        }

        public int Id { get; set; }
        public string ColorText { get; set; }

        public virtual ICollection<Style> Styles { get; set; }
    }
}
