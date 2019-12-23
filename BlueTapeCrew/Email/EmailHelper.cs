using System;
using System.Collections.Generic;
using BlueTapeCrew.Models;
using Entities;

namespace BlueTapeCrew.Email
{
    public static class EmailHelper
    {
        public static SmtpRequest GetTestEmailRequest(SiteSetting settings, string email, string sessionId, string subject)
        {
            var order = GetTestOrder(email, sessionId);
            var textBody = EmailTemplates.GetOrderConfirmationTextBody(order, true);
            var htmlBody = EmailTemplates.GetOrderConfirmationHtmlBody(order);
            return new SmtpRequest(settings, htmlBody, textBody, order.Email, subject);
        }

        private static Order GetTestOrder(string email, string sessionId)
        {
            return new Order
            {
                Id = 1,
                Total = 100.00m,
                State = "NY",
                City = "Manhattan",
                Email = email,
                Phone = "555-555-5555",
                Shipping = 5.00m,
                UserName = "customeruser",
                Address = "123 easy st",
                SubTotal = 100.00m,
                DateCreated = DateTime.UtcNow,
                FirstName = "customer",
                IpAddress = "192.168.1.1",
                LastName = "Smith",
                OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Description = "Product description",
                            Price = 95.00m,
                            Quantity = 1,
                            SubTotal = 95.00m
                        }
                    },
                SessionId = sessionId,
                Zip = "10001"
            };
        }
    }
}