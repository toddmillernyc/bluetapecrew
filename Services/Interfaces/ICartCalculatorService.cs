using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface ICartCalculatorService
    {
        Task<CartTotals> CalculateCartTotals(IList<CartView> cartItems);
    }
}