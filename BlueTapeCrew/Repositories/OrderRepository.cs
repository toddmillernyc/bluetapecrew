using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BtcEntities _db;

        public OrderRepository(BtcEntities db) { _db = db; }

        public Task<Order> GetWithItems(int id) => _db.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);

        public async Task Create(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersWithItemsByUserName(string userName) => await _db.Orders
                                                                                                        .Include(x => x.OrderItems)
                                                                                                        .Where(x => x.UserName == userName)
                                                                                                        .OrderByDescending(x => x.DateCreated)
                                                                                                        .ToListAsync();
    }
}
