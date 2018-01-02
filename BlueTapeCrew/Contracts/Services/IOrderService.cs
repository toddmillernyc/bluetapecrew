using System.Threading.Tasks;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Contracts.Services
{
    public interface IOrderService
    {
        Task AddOrder(Order order);
        Task<Order> GetOrder(int id);
    }
}