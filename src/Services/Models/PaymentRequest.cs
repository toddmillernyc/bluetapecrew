using PayPal.Api;
using System.Collections.Generic;
using System.Linq;

namespace Services.Models
{
    public class PaymentRequest
    {
        private const string MoneyFormat = "0.00";
        private const string SandboxMode = "sandbox";
        private const string LiveMode = "live";

        public PaymentRequest(IList<CartView> cart, PaymentRequestOptions options)
        {
            InitApiCredentialsForMode(options);
            Init(options, cart);
            ItemList = GetItemListFrom(cart);
            InvoiceNumber = options.InvoiceNumber.ToString();
            ReturnUrl = ($"{options.RequestUri}review").ToLower();
        }

        private void Init(PaymentRequestOptions options, IEnumerable<CartView> cart)
        {
            const decimal tax = 0.00m;
            var subTotal = CalculateSubTotal(cart);
            var shipping = CalculateShipping(options, subTotal);
            var total = subTotal + tax + shipping;

            Subtotal = subTotal.ToString(MoneyFormat);
            Total = total.ToString(MoneyFormat);
            Shipping = shipping.ToString(MoneyFormat);
        }

        public string Currency => "USD";
        public string PaymentDescription => "BlueTapeCrew.com Purchase";
        public string PaymentMethod => "paypal";
        public string Intent => "sale";
        public string Mode;

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

        private void InitApiCredentialsForMode(PaymentRequestOptions options)
        {
            if (options.IsSandbox)
            {
                Mode = SandboxMode;
                ClientId = options.PaypalSandBoxClientId;
                ClientSecret = options.PaypalSandBoxSecret;
                
            }
            else
            {
                Mode = LiveMode;
                ClientId = options.PaypalClientId;
                ClientSecret = options.PaypalClientSecret;
            }
        }

        private static decimal CalculateShipping(PaymentRequestOptions options, decimal subtotal)
        {
            var freeShippingThreshold = options.FreeShippingThreshold ?? 0.0m;
            var flatShippingRate = options.FlatShippingRate ?? 0.0m;
            return subtotal > freeShippingThreshold ? 0.0m : flatShippingRate;
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