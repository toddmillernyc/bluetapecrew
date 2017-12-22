using System;
using System.Collections.Generic;
using System.Linq;
using BlueTapeCrew.Models;
using PayPal.Api;

namespace BlueTapeCrew.Paypal
{
    public class PaymentRequest
    {
        private const string MoneyFormat = "0.00";

        public PaymentRequest(Uri requestUri, SiteSetting settings, IList<CartView> cart, int invoiceNumber, string accessToken,  bool isSandbox = true)
        {
            InitApiCredentials(settings, isSandbox);
            Init(settings, cart);
            ItemList = GetItemListFrom(cart);
            InvoiceNumber = invoiceNumber.ToString();
            ReturnUrl = $"{requestUri.Scheme}://{requestUri.Authority}/checkoutreview";
        }

        public string Currency => "USD";
        public string PaymentDescription => "BlueTapeCrew.com Purchase";
        public string PaymentMethod => "paypal";
        public string Intent => "sale";

        public string Shipping { get; set; }
        public string Tax => 0.ToString(MoneyFormat);
        public string Subtotal { get; set; }

        public string AccessToken { get; set; }
        public ItemList ItemList { get; set; }
        public string InvoiceNumber { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Total { get; set; }
        public string ReturnUrl { get; set; }

        private void InitApiCredentials(SiteSetting settings, bool isSandbox)
        {
            if (isSandbox)
            {
                ClientId = settings.PaypalSandBoxClientId;
                ClientSecret = settings.PaypalSandBoxSecret;
            }
            else
            {
                ClientId = settings.PaypalClientId;
                ClientSecret = settings.PaypalClientSecret;
            }
        }
        private void Init(SiteSetting settings, IEnumerable<CartView> cart)
        {
            const decimal tax = 0.00m;
            var subTotal = CalculateSubTotal(cart);
            var shipping = CalculateShipping(settings, subTotal);
            var total = subTotal + tax + shipping;

            Subtotal = subTotal.ToString(MoneyFormat);
            Total = total.ToString(MoneyFormat);
            Shipping = shipping.ToString(MoneyFormat);
        }

        private static decimal CalculateShipping(SiteSetting settings, decimal subtotal)
        {
            return subtotal > settings.FreeShippingThreshold 
                        ? 0 
                        : settings.FlatShippingRate;
        }

        private static decimal CalculateSubTotal(IEnumerable<CartView> cart)
        {
            var subTotal = cart.Where(item => item.SubTotal != null).Aggregate(0.00m, (current, item) => current + (decimal) item.SubTotal);
            return subTotal;
        }

        private static ItemList GetItemListFrom(IEnumerable<CartView> cart)
        {
            var itemList = new ItemList { items = new List<Item>() };
            foreach (var item in cart)
            {
                itemList.items.Add(new Item
                {
                    name = item.ProductName,
                    currency = "USD",
                    price = item.Price.ToString(MoneyFormat),
                    quantity = item.Quantity.ToString(),
                    sku = item.Id.ToString(),
                    description = item.Description,
                    url = $"https://bluetapecrew.com/products/{item.LinkName}",
                    tax = 0.ToString(MoneyFormat)
                });
            }
            return itemList;
        }

    }
}