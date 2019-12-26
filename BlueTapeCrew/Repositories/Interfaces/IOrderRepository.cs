using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetWithItems(int id);
        Task Create(Order order);
        Task<IEnumerable<Order>> GetOrdersWithItemsByUserName(string username);
    }
}
