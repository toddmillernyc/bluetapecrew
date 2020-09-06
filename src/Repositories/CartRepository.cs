using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class CartRepository : ICartRepository, IDisposable
    {
        private readonly BtcEntities _db;

        public CartRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task<List<CartView>> GetBy(string cartId)
        {
            return await _db.CartViews.Where(x => x.CartId == cartId).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<Cart> GetBy(string cartId, int styleId)
        {
            var cart = await _db.Carts.FirstOrDefaultAsync(x => x.SessionId == cartId && x.StyleId == styleId);
            return cart;
        }

        public async Task Add(Cart cart)
        {
            _db.Carts.Add(cart);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Cart cart)
        {
            _db.Entry(cart).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<Cart> GetBy(int id) => await _db.Carts.FindAsync(id);


        public async Task DeleteItem(int id)
        {
            var cart = await _db.Carts.FindAsync(id);
            if (cart == null) throw new Exception($"Cart item {id} not found");
            _db.Carts.Remove(cart);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCart(string cartId)
        {
            var cartItems = _db.Carts.Where(x => x.SessionId == cartId);
            _db.Carts.RemoveRange(cartItems);
            await _db.SaveChangesAsync();
        }

        public void Dispose() => _db?.Dispose();
    }
}