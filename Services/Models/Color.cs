using System.Collections.Generic;

namespace Services.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string ColorText { get; set; }
        public IEnumerable<Style> Styles { get; set; }
    }
}
