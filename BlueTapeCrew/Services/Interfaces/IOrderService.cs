using BlueTapeCrew.ViewModels;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> Create(Order order, CartViewModel cart);
        Task<Order> SendConfirmationEmail(int orderId);
    }
}