using BlueTapeCrew.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface ICartCalculatorService
    {
        Task<CartTotals> CalculateCartTotals(IList<CartView> cartItems);
    }
}