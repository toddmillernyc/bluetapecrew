using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class MenuService : IMenuService
    {
        private readonly ICategoryRepository _categoryRepository;

        public MenuService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<MenuCategory>> Get()
        {
            var categories = await _categoryRepository.GetAllWithProducts();
            return categories
                .OrderBy(X => X.Position)
                .Select(category =>
                    new MenuCategory
                    {
                        Name = category.Name,
                        IsPublished = category.Published,
                        Products = category.ProductCategories
                            .Select(x => x.Product)
                            .OrderBy(x => x.Slug)
                            .ToDictionary(x => x.Slug, x => x.ProductName)
                    });
        }
    }
}
