using System.Collections.Generic;

namespace Services.Models
{
    public class MenuCategory
    {
        public string Name { get; set; }
        public Dictionary<string, string> Products { get; set; }
    }
}
