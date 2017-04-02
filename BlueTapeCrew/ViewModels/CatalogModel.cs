using System.Collections.Generic;

namespace BlueTapeCrew.ViewModels
{
    public class CatalogModel
    {
        public string CategoryName { get; set; }
        public IList<ProductsAzViewModel> Products { get; set; }
    }
}