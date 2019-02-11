using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly IShippingService _shippingService;

        public CartCalculatorService(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }

        public async Task<CartTotals> CalculateCartTotals(IList<CartView> cartItems)
        {
            if(!cartItems.Any()) return new CartTotals();
            var subTotal = cartItems.Sum(x => x.SubTotal);
            var shipping = await _shippingService.Caclulate(subTotal ?? 0.00m);
            var total = shipping + subTotal;
            return new CartTotals
            {
                Count = cartItems.Sum(x=>x.Quantity),
                SubTotal = subTotal ?? 0.00m,
                Shipping = shipping,
                Total = total ?? 0.00m
            };
        }
    }
}