using PayPal.Api;

namespace BlueTapeCrew.Paypal
{
    public interface IPaypalService
    {
        APIContext GetApiContext(PaymentRequest paymentRequest);
        Payment GetPayment(PaymentRequest paymentRequest);
        string PaywithPaypal(PaymentRequest paymentRequest);
        Payment CompletePayment(CompletePaymentRequest paymentRequest);
        string GetAccessToken(string clientId, string clientSecret);
    }
}