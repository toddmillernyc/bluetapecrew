using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Services
{
    public class CartService : ICartService
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public CartService(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public async Task<IEnumerable<CartView>> Get(string sessionId)
        {
            using (var db = new BtcEntities())
            {
                return await db.CartViews.Where(x => x.CartId.Equals(sessionId)).OrderByDescending(x => x.Id).ToListAsync();
            }
        }

        public async Task SetQuantity(string sessionId, int styleId, int quantity)
        {
            using (var db = new BtcEntities())
            {
                var items = db.Carts.Where(x => x.CartId.Equals(sessionId));
                var item = items.FirstOrDefault(x => x.StyleId == styleId);
                if (quantity == 0)
                {
                    db.Carts.Remove(item);
                }
                else
                {
                    if (item != null) item.Count = quantity;
                }
                await db.SaveChangesAsync();
            }
        }

        public async Task<CartViewModel> GetCartViewModel(string sessionId)
        {
            var settings = await _siteSettingsService.GetSettings();
            using (var db = new BtcEntities())
            {
                var cart = await db.CartViews.Where(x => x.CartId == sessionId).ToListAsync();
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
                    Items = db.CartViews.Where(x => x.CartId.Equals(sessionId)).OrderBy(x => x.Id).ToList(),
                    SubTotal = $"{subtotal:n2}",
                    Shipping = $"{shipping:n2}",
                    Total = $"{total:n2}"
                };
                return model;
            }
        }

        public async Task<int> Post(string sessionId, int styleId, int quantity)
        {
            using (var db = new BtcEntities())
            {
                var temp = db.Carts.Where(x => x.CartId.Equals(sessionId)).ToList();
                var style = temp.FirstOrDefault(x => x.StyleId == styleId);

                if (style == null)
                {
                    db.Carts.Add(new Cart
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
                await db.SaveChangesAsync();
                return 1;
            }
        }

        public async Task DeleteItem(int id)
        {
            using (var db = new BtcEntities())
            {
                var temp = await db.Carts.FindAsync(id);
                if (temp == null) return;
                if (temp.Count < 2)
                {
                    db.Carts.Remove(temp);
                }
                else if (temp.Count > 1)
                {
                    temp.Count--;
                }
                db.SaveChanges();
            }
        }

        public async Task EmptyCart(string sessionId)
        {
            using (var db = new BtcEntities())
            {
                foreach (var item in await db.Carts.Where(x => x.CartId.Equals(sessionId)).ToListAsync())
                {
                    db.Carts.Remove(item);
                }
                await db.SaveChangesAsync();
            }
        }
    }
}