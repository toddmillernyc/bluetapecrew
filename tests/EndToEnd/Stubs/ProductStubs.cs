using System.Collections.Generic;

namespace EndToEnd.Stubs
{
    public static class ProductStubs
    {
        public static IEnumerable<ProductStub> Products => new List<ProductStub>
        {
            new ProductStub
            {
                Image = "metro-dress",
                Name = "Metro Dress",
                Slug = "metro-dress",
                Price = 63.00m,
                Category = "Dresses",
                Description = "All over print of the concrete jungle on a black Jersey dress " +
                              "with curve accenting seams rock this dress on a night out!  " +
                              "You will feel amazing and be comfortable at the same time.",
                Colors = new List<string> { "Black/Grey" },
                Sizes = new List<string> { "XS", "S", "M" }
            }
        };
    }
}
