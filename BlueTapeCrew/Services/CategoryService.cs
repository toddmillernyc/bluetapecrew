using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductCategoriesRepository _productCategoriesRepository;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IProductCategoriesRepository productCategoriesRepository)
        {
            _categoryRepository = categoryRepository;
            _productCategoriesRepository = productCategoriesRepository;
        }

        public async Task<Category> Find(int id) => await _categoryRepository.Find(id);
        public async Task<IEnumerable<Category>> GetAll() => (await _categoryRepository.GetAllWithProducts()).OrderBy(x => x.CategoryName);
        public Task Delete(int id) => _categoryRepository.Delete(id);
        public Task Create(Category category) => _categoryRepository.Create(category);

        public async Task ChangeName(int categoryId, string name)
        {
            var category = await _categoryRepository.Find(categoryId);
            category.CategoryName = name;
            await _categoryRepository.Update(category);
        }

        public async Task TogglePublish(int id)
        {
            var category = await _categoryRepository.Find(id);
            category.Published = !category.Published;
            await _categoryRepository.Update(category);
        }

        public Task AddProductCategory(ProductCategory productCategory) => _productCategoriesRepository.Create(productCategory);
        public async Task<IEnumerable<Category>> GetAllWithProducts() => await _categoryRepository.GetAllWithProducts();
    }
}