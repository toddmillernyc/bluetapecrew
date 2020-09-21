using System;
using System.Threading.Tasks;
using PayPal;
using Services.Interfaces;
using Services.Models;

namespace Services
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

        public async Task<CheckoutRequest> CreateCheckoutRequest(User user, string sessionId, string returnUrl)
        {
            var cart = await _cartService.GetCartViewModel(sessionId);
            var model = new CheckoutRequest(user, cart, returnUrl);
            return model;
        }

        public async Task<string> Start(string sessionId, Uri requestUri, bool isSandbox)
        {
            var settings = await _siteSettingsService.Get();
            var profile = await _siteSettingsService.GetSiteProfile();
            var cart = await _cartService.GetCartViewModel(sessionId);
            var paymentRequestOptions = new PaymentRequestOptions(profile, settings, requestUri, isSandbox);
            var paymentRequest = new PaymentRequest(cart.Items, paymentRequestOptions);
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