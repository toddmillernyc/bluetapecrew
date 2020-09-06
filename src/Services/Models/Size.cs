using System.Collections.Generic;

namespace Services.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string SizeText { get; set; }
        public int SizeOrder { get; set; }
        public IEnumerable<Style> Styles { get; set; }
    }
}
