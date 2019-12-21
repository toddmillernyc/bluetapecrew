using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Services.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BlueTapeCrew.Data;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Services
{
    public class OrderService : IOrderService, IDisposable
    {
        private readonly BtcEntities _db;

        public OrderService(BtcEntities db)
        {
            _db = db;
        }

        public async Task<int> Create(Order order, CartViewModel cart)
        {
            order.Shipping = cart.Totals.Shipping;
            order.SubTotal = cart.Totals.SubTotal;
            order.Total = cart.Totals.Total;

            order.OrderItems = cart.Items.Select(item => new OrderItem
            {
                Description = item.ProductName + " " + item.StyleText,
                Price = item.Price,
                SubTotal = item.SubTotal,
                Quantity = item.Quantity

            }).ToList();
            order.DateCreated = DateTime.Now;
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return order.Id;
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
