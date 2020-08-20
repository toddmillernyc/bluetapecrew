using System.Collections.Generic;

namespace Services.Models
{
    public class CatalogModel
    {
        public CatalogModel(string categoryName)
        {
            CategoryName = categoryName;
        }

        public string CategoryName { get; set; }
        public IList<ProductCard> Products { get; set; } = new List<ProductCard>();
    }
}