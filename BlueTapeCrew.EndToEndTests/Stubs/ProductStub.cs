using System.Collections.Generic;

namespace BlueTapeCrew.EndToEndTests.Stubs
{
    public class ProductStub
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<string> Colors { get; set; } = new List<string>();
        public List<string> Sizes { get; set; } = new List<string>();
        public decimal Price { get; set; }
    }
}
