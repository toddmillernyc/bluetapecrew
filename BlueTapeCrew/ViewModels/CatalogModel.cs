using System.Collections.Generic;
using System.Drawing.Design;

namespace BlueTapeCrew.ViewModels
{
    public class CatalogModel
    {
        public CatalogModel(string categoryName)
        {
            CategoryName = categoryName;
        }

        public string CategoryName { get; set; }
        public IList<ProductsAzViewModel> Products { get; set; } = new List<ProductsAzViewModel>();
    }
}