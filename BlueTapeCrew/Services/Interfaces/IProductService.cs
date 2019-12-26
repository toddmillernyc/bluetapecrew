using BlueTapeCrew.ViewModels;
using Entities;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductViewModel> GetProductViewModel(string name);
        Task<string> AddReview(Review review);
        Task<string> GetStylePrice(int id);
        Task Delete(int id);
    }
}
