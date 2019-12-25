using BlueTapeCrew.ViewModels;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> Create(Order order, CartViewModel cart);
        Task<Order> SendConfirmationEmail(int orderId);
        Task<IEnumerable<Order>> GetBy(string userName);
    }
}