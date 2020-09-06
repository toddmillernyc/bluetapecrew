using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IShippingService
    {
        Task<decimal> Calculate(decimal subtotal);
    }
}