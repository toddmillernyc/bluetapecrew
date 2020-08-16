using System.Collections.Generic;

namespace EndToEnd.Stubs
{
    public static class StringStubs
    {
        public static IEnumerable<string> Categories = new List<string>
        {
            "Bags","Dresses","Hoodies", "Legwear", "Limited Edition Prints", "Mens", "T-Shirts", "Womens"
        };

        public static IEnumerable<string> Colors = new List<string>
        {
            "Ash Grey", "Black", "Black/Grey", "black/violet", "Faded Blue", "Heather Grey", "Neon Red", "Nude/sheer","pale pink", "purple"
        };
    }
}
