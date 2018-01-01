using System;
using System.Data.Entity;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Services
{
    public class OrderService : IOrderService, IDisposable
    {
        private readonly BtcEntities _db = new BtcEntities();

        public async Task AddOrder(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
        }

        public async Task<Order> GetOrder(int id)
        {

            return await _db.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);

        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
