using BlueTapeCrew.Models;
using PayPal.Api;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IPaypalService
    {
        APIContext GetApiContext(PaymentRequest paymentRequest);
        Payment GetPayment(PaymentRequest paymentRequest);
        string PaywithPaypal(PaymentRequest paymentRequest);
        Payment CompletePayment(CompletePaymentRequest paymentRequest);
        string GetAccessToken(string clientId, string clientSecret, string mode);
    }
}