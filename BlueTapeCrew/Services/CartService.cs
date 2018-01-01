using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Services
{
    public class CartService : ICartService, IDisposable
    {
        private readonly BtcEntities _db;
        private readonly ISiteSettingsService _siteSettingsService;

        public CartService(ISiteSettingsService siteSettingsService)
        {
            _db = new BtcEntities();
            _siteSettingsService = siteSettingsService;
        }

        public async Task<IEnumerable<CartView>> Get(string sessionId)
        {
            return await _db.CartViews.Where(x => x.CartId.Equals(sessionId)).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task SetQuantity(string sessionId, int styleId, int quantity)
        {
            var items = _db.Carts.Where(x => x.CartId.Equals(sessionId));
            var item = items.FirstOrDefault(x => x.StyleId == styleId);
            if (quantity == 0)
            {
                _db.Carts.Remove(item);
            }
            else
            {
                if (item != null) item.Count = quantity;
            }
            await _db.SaveChangesAsync();
        }

        public async Task<CartViewModel> GetCartViewModel(string sessionId)
        {
            var settings = await _siteSettingsService.Get();
            var cart = await _db.CartViews.Where(x => x.CartId == sessionId).ToListAsync();
            if (!cart.Any())
            {
                return new CartViewModel
                {
                    Count = 0
                };
            }

            var subtotal = cart.Sum(x => x.SubTotal);
            decimal shipping = 0;
            if (subtotal < settings.FreeShippingThreshold)
            {
                shipping = settings.FlatShippingRate;
            }
            var total = subtotal + shipping;

            var model = new CartViewModel
            {
                Count = cart.Sum(x => x.Quantity),
                Items = _db.CartViews.Where(x => x.CartId.Equals(sessionId)).OrderBy(x => x.Id).ToList(),
                SubTotal = $"{subtotal:n2}",
                Shipping = $"{shipping:n2}",
                Total = $"{total:n2}"
            };
            return model;
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