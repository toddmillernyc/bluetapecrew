using System.Collections.Generic;
using System.Threading.Tasks;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface ICartService
    {

        Task<IEnumerable<CartView>> Get(string sessionId);
        Task<CartViewModel> GetCartViewModel(string sessionId);
        Task<int> Post(string sessionId, int styleId, int quantity);
        Task DeleteItem(int id);
        Task EmptyCart(string sessionId);
    }
}
