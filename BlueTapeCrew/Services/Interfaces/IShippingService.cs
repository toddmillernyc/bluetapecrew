using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IShippingService
    {
        Task<decimal> Calculate(decimal subtotal);
    }
}