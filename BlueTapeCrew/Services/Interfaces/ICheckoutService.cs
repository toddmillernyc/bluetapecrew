using BlueTapeCrew.Models;
using BlueTapeCrew.ViewModels;
using System;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<CheckoutRequest> CreateCheckoutRequest(string username, string returnUrl);
        Task<string> Start(string sessionId, Uri requestUri, bool isSandbox);
        Task<string> Complete(CompletePaymentRequest request, bool isSandbox);
    }
}
