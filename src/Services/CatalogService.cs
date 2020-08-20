using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICategoryService _categoryService;

        public CatalogService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var catalog = new List<CatalogModel>();
            var categories = await _categoryService.GetAllPublishedWithProductsAndStyles();
            foreach (var category in categories)
            {
                var catalogModel = new CatalogModel(category.Name);
                var categoryProducts = category.ProductCategories.Select(x => x.Product).OrderBy(x => x.ProductName);
                foreach (var product in categoryProducts)
                {
                    catalogModel.Products.Add(new ProductCard
                    {
                        Id = product.Id,
                        Name = product.ProductName,
                        Slug = product.Slug,
                        Price = $"{product.Styles?.FirstOrDefault()?.Price:n2}",
                        ImgSource = "images/" + product.Slug + ".jpg"
                    });
                }
                catalog.Add(catalogModel);
            }
            return catalog;
        }
    }
}
