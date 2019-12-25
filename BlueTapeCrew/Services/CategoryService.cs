using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using Entities;

namespace BlueTapeCrew.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Find(int id) => await _categoryRepository.Find(id);

        public async Task ChangeName(int categoryId, string name)
        {
            var category = await _categoryRepository.Find(categoryId);
            category.CategoryName = name;
            await _categoryRepository.Update(category);
        }

        public async Task<IEnumerable<Category>> GetAll() =>
            (await _categoryRepository.GetAll()).OrderBy(x => x.CategoryName);
    }
}
