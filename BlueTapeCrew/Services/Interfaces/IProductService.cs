using BlueTapeCrew.ViewModels;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductViewModel> GetProductViewModel(string name);
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
    }
}
