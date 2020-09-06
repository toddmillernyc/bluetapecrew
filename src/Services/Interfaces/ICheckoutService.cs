using System;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<CheckoutRequest> CreateCheckoutRequest(User user, string sessionId, string returnUrl);
        Task<string> Start(string sessionId, Uri requestUri, bool isSandbox);
        Task<string> Complete(CompletePaymentRequest request, bool isSandbox);
    }
}
