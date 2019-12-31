using PayPal.Api;
using Services.Models;

namespace Services.Interfaces
{
    public interface IPaypalService
    {
        PayPal.Api.APIContext GetApiContext(PaymentRequest paymentRequest);
        Payment GetPayment(PaymentRequest paymentRequest);
        string PayWithPaypal(PaymentRequest paymentRequest);
        Payment CompletePayment(CompletePaymentRequest paymentRequest);
        string GetAccessToken(string clientId, string clientSecret, string mode);
    }
}