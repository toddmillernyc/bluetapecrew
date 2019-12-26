using BlueTapeCrew.Email;
using BlueTapeCrew.Models;
using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BlueTapeCrew.Models.Constants;

namespace BlueTapeCrew.Services
{
    public class OrderService : IOrderService
    {
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IUserService _users;
        private readonly IEmailService _emailService;
        private readonly IOrderRepository _orderRepository;

        public OrderService(ISiteSettingsService siteSettingsService,
            IUserService users,
            IEmailService emailService,
            IOrderRepository orderRepository)
        {
            _siteSettingsService = siteSettingsService;
            _users = users;
            _emailService = emailService;
            _orderRepository = orderRepository;
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
            await _orderRepository.Create(order);
            return order.Id;
        }

        public async Task<Order> SendConfirmationEmail(int orderId)
        {
            var order = await _orderRepository.GetWithItems(orderId);
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

        public async Task<IEnumerable<Order>> GetBy(string userName) => await _orderRepository.GetOrdersWithItemsByUserName(userName);
    }
}
