using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface ICartCalculatorService
    {
        Task<CartTotals> CalculateCartTotals(IList<CartView> cartItems);
    }
}