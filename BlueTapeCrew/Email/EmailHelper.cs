using System;
using System.Collections.Generic;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Email
{
    public static class EmailHelper
    {
        public static SmtpRequest GetTestEmailRequest(SiteSetting settings, string email)
        {
            var order = GetTestOrder(email);
            var textBody = EmailTemplates.GetOrderConfirmationTextBody(order, true);
            var htmlBody = EmailTemplates.GetOrderConfirmationHtmlBody(order);
            return new SmtpRequest(settings, htmlBody, textBody, order.Email);
        }

        private static Order GetTestOrder(string email)
        {
            return new Order
            {
                Id = 1,
                Invoice = new Invoice("abc"),
                Total = 100.00m,
                State = "NY",
                City = "Manhattan",
                Email = email,
                InvoiceId = 2,
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
                SessionId = "sessionabc",
                Zip = "10001"
            };
        }
    }
}