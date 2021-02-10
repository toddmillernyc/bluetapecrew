using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> FindBySlugIncludeAll(string name);
        Task<string> AddReview(Review review);
        Task<string> GetStylePrice(int id);
        Task Delete(int id);
        Task<Image> GetImageBySlug(string slug);
        Task<IDictionary<int, string>> GetProductNames();
        Task AddImageToProduct(int productId, int imageId);
        Task Create(Product product);
        Task<Product> Find(int id);
        Task<IEnumerable<Product>> GetAllIncludeAll();
        Task Update(Product product);
        Task<Product> FindIncludeAll(int id);
        Task<IEnumerable<Product>> GetProductsWithStylesAndImage(int take);
        Task<IEnumerable<Product>> GetByCategoryId(int categoryId);
    }
}
