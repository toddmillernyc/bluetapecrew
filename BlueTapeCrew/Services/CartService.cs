using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Services
{
    public class CartService : ICartService
    {
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ICartRepository _cartRepository;

        public CartService(ICartCalculatorService cartCalculatorService, ICartRepository cartRepository)
        {
            _cartCalculatorService = cartCalculatorService;
            _cartRepository = cartRepository;
        }

        public async Task<List<CartView>> Get(string sessionId) => await _cartRepository.GetBy(sessionId);

        public async Task EmptyCart(string sessionId) => await _cartRepository.DeleteCart(sessionId);

        public async Task<CartViewModel> GetCartViewModel(string sessionId)
        {
            var cartItems = await _cartRepository.GetBy(sessionId);
            var totals = await _cartCalculatorService.CalculateCartTotals(cartItems);
            return new CartViewModel(cartItems, totals);
        }

        public async Task AddOrUpdate(string sessionId, int styleId, int quantity)
        {
            var cartItem = await _cartRepository.GetBy(sessionId, styleId);
            if (cartItem != null)
            {
                cartItem.Count += quantity;
                await _cartRepository.Update(cartItem);
            }
            else
            {
                await _cartRepository.Add(new Cart
                {
                    CartId = sessionId,
                    StyleId = styleId,
                    Count = quantity,
                    DateCreated = DateTime.UtcNow
                });
            }
        }

        public async Task DecrementCartItem(int id)
        {
            var cartItem = await _cartRepository.GetBy(id);
            if (cartItem.Count <= 1)
            {
                await _cartRepository.DeleteItem(id);
            }
            else
            {
                cartItem.Count--;
                await _cartRepository.Update(cartItem);
            }
        }
    }
}