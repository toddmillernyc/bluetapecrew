using System.Collections.Generic;

namespace Api.Models.Entities
{
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
