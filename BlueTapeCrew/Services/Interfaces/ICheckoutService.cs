using System;
using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<string> Start(string sessionId, Uri requestUri, bool isSandbox);
        Task<string> Complete(CompletePaymentRequest request, bool isSandbox);
    }
}
