using BlueTapeCrew.ViewModels;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductViewModel> GetProductViewModel(string name);
        Task<string> AddReview(int productId, string name, string email, string review, decimal rating);
        Task<IEnumerable<Product>> GetBestSellers(int count=3);
        Task<string> GetStylePrice(int id);
        Task Delete(int id);
    }
}
