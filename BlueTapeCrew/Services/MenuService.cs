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
        private readonly ICategoryProductsRepository _categoryProductsRepository;

        public MenuService(ICategoryProductsRepository categoryProductsRepository)
        {
            _categoryProductsRepository = categoryProductsRepository;
        }

        public async Task<IEnumerable<MenuCategory>> Get()
        {
            var categories = await _categoryProductsRepository.Get();
            return categories.Select(category => 
                new MenuCategory 
                {
                    Name = category.CategoryName,
                    Products = category.Products
                                .OrderBy(x=>x.LinkName)
                                .ToDictionary(x => x.LinkName, x => x.ProductName)
                })
                .OrderBy(x=>x.Name);
        }
    }
}
