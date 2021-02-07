using System.Collections.Generic;

namespace Services.Models
{
    public class MenuCategory
    {
        public string Id => Name.Trim().ToLower().Replace(" ", "-");
        public string Name { get; set; }
        public bool IsPublished { get; set; }
        public Dictionary<string, string> Products { get; set; }
    }
}
