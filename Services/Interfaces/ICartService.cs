using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface ICartService
    {

        Task<List<CartView>> Get(string sessionId);
        Task<CartViewModel> GetCartViewModel(string sessionId);
        Task AddOrUpdate(Cart cart);
        Task DecrementCartItem(int id);
        Task EmptyCart(string sessionId);
    }
}
