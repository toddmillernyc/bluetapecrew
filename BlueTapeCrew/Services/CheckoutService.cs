using System;
using System.Threading.Tasks;
using BlueTapeCrew.Models;
using BlueTapeCrew.Services.Interfaces;

namespace BlueTapeCrew.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly ICartService _cartService;
        private readonly IInvoiceService _invoiceService;
        private readonly IPaypalService _paypalService;

        public CheckoutService(ISiteSettingsService siteSettingsService, 
            ICartService cartService, IInvoiceService invoiceService, IPaypalService paypalService)
        {
            _siteSettingsService = siteSettingsService;
            _cartService = cartService;
            _invoiceService = invoiceService;
            _paypalService = paypalService;
        }

        public async Task<string> Start(string sessionId, Uri requestUri, bool isSandbox)
        {
            var settings = await _siteSettingsService.Get();
            var cart = await _cartService.GetCartViewModel(sessionId);
            var invoice = await _invoiceService.Create(sessionId);
            var paymentRequest = new PaymentRequest(requestUri, settings, cart.Items, invoice.Id, string.Empty, isSandbox);
            var redirectUrl = _paypalService.PaywithPaypal(paymentRequest);
            return redirectUrl;
        }

        public async Task<string> Complete(CompletePaymentRequest request, bool isSandbox)
        {
            string clientId;
            string clientSecret;
            var settings = await _siteSettingsService.Get();

            if (isSandbox)
            {
                clientId = settings.PaypalSandBoxClientId;
                clientSecret = settings.PaypalSandBoxSecret;
            }
            else
            {
                clientId = settings.PaypalClientId;
                clientSecret = settings.PaypalClientSecret;
            }
            request.Token = _paypalService.GetAccessToken(clientId, clientSecret, isSandbox ? "sandbox" : "mode");
            var completedPayment = _paypalService.CompletePayment(request);
            return completedPayment.id;
        }
    }
}