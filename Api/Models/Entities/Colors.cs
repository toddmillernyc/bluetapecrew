using System.Collections.Generic;

namespace Api.Models.Entities
{
    public class Colors
    {
        public Colors()
        {
            Styles = new HashSet<Style>();
        }

        public int Id { get; set; }
        public string ColorText { get; set; }

        public virtual ICollection<Style> Styles { get; set; }
    }
}
