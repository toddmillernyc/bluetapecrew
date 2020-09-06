using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Repositories.Interfaces;
using Services.Helpers;
using Services.Interfaces;
using Services.Models;
using Orders = Services.Models.Constants.Orders;
using Entity = Repositories.Entities;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IEmailService _emailService;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(ISiteSettingsService siteSettingsService,
            IEmailService emailService,
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _siteSettingsService = siteSettingsService;
            _emailService = emailService;
            _orderRepository = orderRepository;
            _mapper = mapper;
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
            var entity = _mapper.Map<Entity.Order>(order);
            await _orderRepository.Create(entity);
            return entity.Id;
        }

        public async Task<Order> SendConfirmationEmail(int orderId)
        {
            var orderEntity = await _orderRepository.GetWithItems(orderId);
            var order = _mapper.Map<Order>(orderEntity);
            var emailRequest = await GetSmtpRequest(order);
            await _emailService.SendEmail(emailRequest);
            return order;
        }

        private async Task<SmtpRequest> GetSmtpRequest(Order order)
        {
            var settings = await _siteSettingsService.Get();
            var textBody = EmailHelper.GetOrderConfirmationTextBody(order, order.IsGuestOrder);
            var htmlBody = EmailHelper.GetOrderConfirmationHtmlBody(order);
            return new SmtpRequest(settings, htmlBody, textBody, order.Email, Orders.EmailSubject);
        }

        public async Task<IEnumerable<Order>> GetBy(string userName)
        {
            var entities = await _orderRepository.GetOrdersWithItemsByUserName(userName);
            var model = _mapper.Map<IEnumerable<Order>>(entities);
            return model;
        }
    }
}
