using System.Threading.Tasks;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddOrder(Order order);
        Task<Order> GetOrder(int id);
    }
}