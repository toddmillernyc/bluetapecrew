using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<List<CartView>> GetBy(string cartId);
        Task Add(Cart cart);
        Task Update(Cart cart);
        Task<Cart> GetBy(int id);
        Task DeleteItem(int id);
        Task<Cart> GetBy(string cartId, int styleId);
        Task DeleteCart(string cartId);
    }
}
