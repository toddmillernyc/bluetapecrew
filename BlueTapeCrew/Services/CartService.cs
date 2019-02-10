using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class CartService : ICartService, IDisposable
    {
        private readonly BtcEntities _db;
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly ICartCalculatorService _cartCalculatorService;

        public CartService(ISiteSettingsService siteSettingsService, ICartCalculatorService cartCalculatorService)
        {
            _db = new BtcEntities();
            _siteSettingsService = siteSettingsService;
            _cartCalculatorService = cartCalculatorService;
        }

        public async Task<List<CartView>> Get(string sessionId)
        {
            return await _db.CartViews.Where(x => x.CartId.Equals(sessionId)).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<CartViewModel> GetCartViewModel(string sessionId)
        {
            var cart = await _db.CartViews.Where(x => x.CartId == sessionId).ToListAsync();
            var totals = await _cartCalculatorService.CalculateCartTotals(cart);
            return new CartViewModel(cart, totals);
        }

        public async Task<int> Post(string sessionId, int styleId, int quantity)
        {
            var temp = _db.Carts.Where(x => x.CartId.Equals(sessionId)).ToList();
            var style = temp.FirstOrDefault(x => x.StyleId == styleId);

            if (style == null)
            {
                _db.Carts.Add(new Cart
                {
                    CartId = sessionId,
                    StyleId = styleId,
                    Count = quantity,
                    DateCreated = DateTime.UtcNow
                });
            }
            else
            {
                style.Count = style.Count + quantity;
                style.DateCreated = DateTime.UtcNow;
            }
            await _db.SaveChangesAsync();
            return 1;
        }

        public async Task DeleteItem(int id)
        {
            var temp = await _db.Carts.FindAsync(id);
            if (temp == null) return;
            if (temp.Count < 2)
            {
                _db.Carts.Remove(temp);
            }
            else if (temp.Count > 1)
            {
                temp.Count--;
            }
            _db.SaveChanges();
        }

        public async Task EmptyCart(string sessionId)
        {
            foreach (var item in await _db.Carts.Where(x => x.CartId.Equals(sessionId)).ToListAsync())
            {
                _db.Carts.Remove(item);
            }
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}