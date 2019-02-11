using System.Collections.Generic;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Tests.Stubs
{
    public static class CartViewStubs
    {
        public static IList<CartView> Get()
        {
            return new List<CartView>
            {
                new CartView
                {
                    Description = "The NYC Chomper is a design that has every landmark that you could think of in NYC.  With the soul of NYC in mind this piece has just about every corner and crevice of the city inside a angry metropolis face proving that this city can chew you up and spit you out.    Fine jersey 30/1 combed ring spun 100% cotton 4.4 oz/sq yd  Set on rib collar  Double needle sleeve hem & bottom hem  Tape shoulder to shoulder  Side seamed  Made in U.S.A",
                    Quantity = 2,
                    Price = 19.99m,
                    ProductName = "NYC Chomper",
                    Id = 1371,
                    CartId = "kvqjdu0heqtii04ype0egjzb",
                    StyleId = 1032,
                    LinkName = "nyc-chomper",
                    ColorText = "Heather Grey",
                    ImageData = new byte[0],
                    ProductId = 23,
                    StyleText = "Color: Heather Grey; Size: S",
                    SubTotal = 39.98m
                }
            };
        }
    }
}
