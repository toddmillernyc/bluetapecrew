using BlueTapeCrew.Data;
using BlueTapeCrew.Email;
using BlueTapeCrew.Models;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BlueTapeCrew.Models.Constants;

namespace BlueTapeCrew.Services
{
    public class OrderService : IOrderService, IDisposable
    {
        private readonly BtcEntities _db;
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IUserService _users;
        private readonly IEmailService _emailService;

        public OrderService(BtcEntities db,
            ISiteSettingsService siteSettingsService,
            IUserService users,
            IEmailService emailService)
        {
            _db = db;
            _siteSettingsService = siteSettingsService;
            _users = users;
            _emailService = emailService;
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

        public async Task<Order> SendConfirmationEmail(int orderId)
        {
            var order = await _db.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == orderId);
            var emailRequest = await GetSmtpRequest(order);
            await _emailService.SendEmail(emailRequest);
            return order;
        }

        private async Task<SmtpRequest> GetSmtpRequest(Order order)
        {
            var settings = await _siteSettingsService.Get();
            var user = await _users.Find(order.Email);
            var textBody = EmailTemplates.GetOrderConfirmationTextBody(order, user != null);
            var htmlBody = EmailTemplates.GetOrderConfirmationHtmlBody(order);
            return new SmtpRequest(settings, htmlBody, textBody, order.Email, Orders.EmailSubject);
        }

        public async Task<IEnumerable<Order>> GetBy(string userName) 
            => await _db.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.UserName == userName)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();

        public void Dispose() => _db?.Dispose();
    }
}
