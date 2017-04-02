using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Interfaces
{
    public interface IOrderService
    {
        Task AddOrder(Order order);
        Task<Order> GetOrder(int id);
    }
}