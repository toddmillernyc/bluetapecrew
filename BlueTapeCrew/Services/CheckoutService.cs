using System;
using System.Threading.Tasks;
using BlueTapeCrew.Models;
using BlueTapeCrew.Services.Interfaces;
using PayPal;

namespace BlueTapeCrew.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly ICartService _cartService;
        private readonly IPaypalService _paypalService;

        public CheckoutService(
            ISiteSettingsService siteSettingsService, 
            ICartService cartService,
            IPaypalService paypalService)
        {
            _siteSettingsService = siteSettingsService;
            _cartService = cartService;
            _paypalService = paypalService;
        }

        public async Task<string> Start(string sessionId, Uri requestUri, bool isSandbox)
        {
            var settings = await _siteSettingsService.Get();
            var cart = await _cartService.GetCartViewModel(sessionId);
            var invoiceId = DateTime.Now.Ticks;
            var paymentRequest = new PaymentRequest(requestUri, settings, cart.Items, invoiceId, string.Empty, isSandbox);
            var redirectUrl = _paypalService.PayWithPaypal(paymentRequest);
            return redirectUrl;
        }

        public async Task<string> Complete(CompletePaymentRequest request, bool isSandbox)
        {
            try
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
            catch (PaymentsException ex)
            {
                throw new Exception(ex.Response);
            }

        }
    }
}