using BlueTapeCrew.Models;
using PayPal.Api;

namespace BlueTapeCrew.Paypal
{
    public interface IPaypalService
    {
        APIContext GetApiContext(PaymentRequest paymentRequest);
        Payment GetPayment(PaymentRequest paymentRequest);
        string PaywithPaypal(PaymentRequest paymentRequest);
    }
}