using System.Collections.Generic;

namespace Services.Models
{
    public class CatalogModel
    {
        public CatalogModel(string categoryName, int position)
        {
            CategoryName = categoryName;
            Position = position;
        }

        public string CategoryName { get; set; }
        public int Position { get; set; }
        public IList<ProductsAzViewModel> Products { get; set; } = new List<ProductsAzViewModel>();
    }
}