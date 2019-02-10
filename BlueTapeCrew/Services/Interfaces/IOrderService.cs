using System.Threading.Tasks;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> Create(Order order, CartViewModel cart);
        Task<Order> GetOrder(int id);
    }
}