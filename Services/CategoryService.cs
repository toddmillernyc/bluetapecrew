using AutoMapper;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity = Repositories.Entities;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductCategoriesRepository _productCategoriesRepository;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IProductCategoriesRepository productCategoriesRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productCategoriesRepository = productCategoriesRepository;
            _mapper = mapper;
        }

        public async Task<Category> Find(int id)
        {
            var entity = await _categoryRepository.Find(id);
            var model = _mapper.Map<Category>(entity);
            return model;
        }

        public Task Delete(int id) => _categoryRepository.Delete(id);

        public Task Create(Category category)
        {
            var entity = _mapper.Map<Entity.Category>(category);
            return _categoryRepository.Create(entity);
        }

        public async Task ChangeName(int categoryId, string name)
        {
            var category = await _categoryRepository.Find(categoryId);
            category.Name = name;
            await _categoryRepository.Update(category);
        }

        public async Task TogglePublish(int id)
        {
            var category = await _categoryRepository.Find(id);
            category.Published = !category.Published;
            await _categoryRepository.Update(category);
        }

        public Task AddProductCategory(ProductCategory productCategory)
        {
            var entity = _mapper.Map<Entity.ProductCategory>(productCategory);
            return _productCategoriesRepository.Create(entity);
        }

        public async Task<IEnumerable<Category>> GetAllWithProducts()
        {
            var entities = await _categoryRepository.GetAllWithProducts();
            var model = _mapper.Map<IEnumerable<Category>>(entities);
            return model;
        }

        public Task DeleteProductCategory(ProductCategory productCategory)
        {
            var entity = _mapper.Map<Entity.ProductCategory>(productCategory);
            return _productCategoriesRepository.Delete(entity);
        }

        public async Task<IEnumerable<Category>> GetAllPublishedWithProductsAndStyles()
        {
            var entities = await _categoryRepository.GetAllPublishedWithProductsAndStyles();
            var model = _mapper.Map<IEnumerable<Category>>(entities);
            return model;
        }

        public async Task<IEnumerable<Category>> GetAllPublishedWithProducts()
        {
            var entities = await _categoryRepository.GetAllPublishedWithProducts();
            var model = _mapper.Map<IEnumerable<Category>>(entities);
            return model;
        }
    }
}