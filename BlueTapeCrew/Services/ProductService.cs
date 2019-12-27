using System.Collections.Generic;
using System.Linq;
using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using Entities;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStyleRepository _styleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductImageRepository _productImageRepository;

        public ProductService(
            IProductRepository productRepository,
            IStyleRepository styleRepository,
            ICategoryRepository categoryRepository,
            IReviewRepository reviewRepository,
            IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _styleRepository = styleRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _productImageRepository = productImageRepository;
        }

        public async Task AddImageToProduct(int productId, int imageId)
        {
            var productImage = new ProductImage {ProductId = productId, ImageId = imageId};
            await _productImageRepository.Create(productImage);
        }

        public async Task Create(Product product) => await _productRepository.Create(product);
        public Task<Product> Find(int id) => _productRepository.Find(id);
        public Task<IEnumerable<Product>> GetAllIncludeAll() => _productRepository.GetAllIncludeAll();
        public Task Update(Product product) => _productRepository.Update(product);
        public Task<Product> FindIncludeAll(int id) => _productRepository.FindIncludeAll(id);
        public Task Delete(int id) => _productRepository.Delete(id);

        public async Task<ProductViewModel> GetProductViewModel(string name)
        {
            var product = await _productRepository.FindBySlugIncludeAll(name);
            var styleViews = await _styleRepository.GetByProductId(product.Id);
            var bestSellers = await _productRepository.GetProductsWithStylesAndImage(3);
            var categories = await _categoryRepository.GetAllPublishedWithProducts();
            return new ProductViewModel(product, styleViews, bestSellers, categories);
        }

        //todo: keep users from adding multiple reviews
        public async Task<string> AddReview(Review review)
        {
            await _reviewRepository.Create(review);
            return await _productRepository.GetSlug(review.ProductId);
        }

        public async Task<string> GetStylePrice(int id) => $"{(await _styleRepository.Find(id)).Price:n2}";

        public async Task<Image> GetImageBySlug(string slug) => (await _productRepository.FindBySlugIncludeImage(slug)).Image;

        public async Task<IDictionary<int, string>> GetProductNames()
        {
            var products = await _productRepository.GetAll();
            return products.ToDictionary(x => x.Id, x => x.ProductName);
        }
    }
}