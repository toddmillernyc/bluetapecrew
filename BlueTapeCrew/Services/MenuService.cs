using BlueTapeCrew.Models;
using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
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
                .Select(category =>
                    new MenuCategory
                    {
                        Name = category.CategoryName,
                        Products = category.ProductCategories
                            .Select(x => x.Product)
                            .OrderBy(x => x.LinkName)
                            .ToDictionary(x => x.LinkName, x => x.ProductName)
                    })
                .OrderBy(x => x.Name);
        }
    }
}
