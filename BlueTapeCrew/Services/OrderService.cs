using System.Data.Entity;
using System.Threading.Tasks;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Services
{
    public class OrderService : IOrderService
    {
        public async Task AddOrder(Order order)
        {
            using (var db = new BtcEntities())
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Order> GetOrder(int id)
        {
            using (var db = new BtcEntities())
            {
               return await db.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
            }
        }
    }
}