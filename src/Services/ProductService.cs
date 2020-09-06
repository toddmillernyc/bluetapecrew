using AutoMapper;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity = Repositories.Entities;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStyleRepository _styleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            IStyleRepository styleRepository,
            ICategoryRepository categoryRepository,
            IReviewRepository reviewRepository,
            IProductImageRepository productImageRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _styleRepository = styleRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }

        public async Task AddImageToProduct(int productId, int imageId)
        {
            var model = new ProductImage {ProductId = productId, ImageId = imageId};
            var entity = _mapper.Map<Entity.ProductImage>(model);
            await _productImageRepository.Create(entity);
        }

        public async Task Create(Product product)
        {
            var entity = _mapper.Map<Product, Entity.Product>(product);
            await _productRepository.Create(entity);
            product.Id = entity.Id;

        }

        public async Task<Product> Find(int id)
        {
            var entity = await _productRepository.Find(id);
            return _mapper.Map<Product>(entity);
        }

        public async Task<IEnumerable<Product>> GetAllIncludeAll()
        {
            var entities = await _productRepository.GetAllIncludeAll();
            var model = _mapper.Map<IEnumerable<Product>>(entities);
            return model;
        }

        public Task Update(Product product)
        {
            var entity = _mapper.Map<Entity.Product>(product);
            return _productRepository.Update(entity);
        }

        public async Task<Product> FindIncludeAll(int id)
        {
            var entity = await _productRepository.FindIncludeAll(id);
            return _mapper.Map<Product>(entity);
        }

        public async Task<IEnumerable<Product>> GetProductsWithStylesAndImage(int take)
        {
            var entities = await _productRepository.GetProductsWithStylesAndImage(take);
            var model = _mapper.Map<IEnumerable<Product>>(entities);
            return model;
        }

        public Task Delete(int id) => _productRepository.Delete(id);

        public async Task<Product> FindBySlugIncludeAll(string name)
        {
            var entity = await _productRepository.FindBySlugIncludeAll(name);
            return _mapper.Map<Product>(entity);
        }

        //todo: keep users from adding multiple reviews
        public async Task<string> AddReview(Review review)
        {
            var entity = _mapper.Map<Entity.Review>(review);
            await _reviewRepository.Create(entity);
            return await _productRepository.GetSlug(review.ProductId);
        }

        public async Task<string> GetStylePrice(int id) => $"{(await _styleRepository.Find(id)).Price:n2}";

        public async Task<Image> GetImageBySlug(string slug)
        {
            var productEntity = await _productRepository.FindBySlugIncludeImage(slug);
            var model = _mapper.Map<Image>(productEntity.Image);
            return model;
        }

        public async Task<IDictionary<int, string>> GetProductNames()
        {
            var products = await _productRepository.GetAll();
            return products.ToDictionary(x => x.Id, x => x.ProductName);
        }
    }
}