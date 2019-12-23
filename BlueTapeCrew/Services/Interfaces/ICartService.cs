using BlueTapeCrew.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface ICartService
    {

        Task<List<CartView>> Get(string sessionId);
        Task<CartViewModel> GetCartViewModel(string sessionId);
        Task AddOrUpdate(string sessionId, int styleId, int quantity);
        Task DecrementCartItem(int id);
        Task EmptyCart(string sessionId);
    }
}
