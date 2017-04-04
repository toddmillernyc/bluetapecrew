using System.Collections.Generic;
using System.Threading.Tasks;
using BlueTapeCrew.Models;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Interfaces
{
    public interface ICartService
    {

        Task<IEnumerable<CartView>> Get(string sessionId);
        Task SetQuantity(string sessionId,int styleId, int quantity);
        Task<CartViewModel> GetCartViewModel(string sessionId);
        Task<int> Post(string sessionId, int styleId, int quantity);
        Task DeleteItem(int id);
        Task EmptyCart(string sessionId);
    }
}
