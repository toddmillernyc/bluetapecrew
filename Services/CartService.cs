using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using Entity = Repositories.Entities;

namespace Services
{
    public class CartService : ICartService
    {
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(ICartCalculatorService cartCalculatorService,
            ICartRepository cartRepository,
            IMapper mapper)
        {
            _cartCalculatorService = cartCalculatorService;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<List<CartView>> Get(string sessionId)
        {
            var entities = await _cartRepository.GetBy(sessionId);
            var model = _mapper.Map<List<CartView>>(entities);
            return model;
        }

        public async Task EmptyCart(string sessionId) => await _cartRepository.DeleteCart(sessionId);

        public async Task<CartViewModel> GetCartViewModel(string sessionId)
        {
            var cartItemEntities = await _cartRepository.GetBy(sessionId);
            var model = _mapper.Map<List<CartView>>(cartItemEntities);
            var totals = await _cartCalculatorService.CalculateCartTotals(model);
            return new CartViewModel(model, totals);
        }

        public async Task AddOrUpdate(Cart cart)
        {
            
            var cartItemEntity = await _cartRepository.GetBy(cart.SessionId, cart.StyleId);
            if (cartItemEntity != null)
            {
                cartItemEntity.Count += cart.Quantity;
                await _cartRepository.Update(cartItemEntity);
            }
            else
            {
                var entity = _mapper.Map<Entity.Cart>(cart);
                entity.DateCreated = DateTime.Now;
                await _cartRepository.Add(entity);
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