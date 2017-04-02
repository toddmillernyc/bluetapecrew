using BlueTapeCrew.Models;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Interfaces
{
    public interface ICheckoutService
    {
        CheckoutViewModel GetCheckoutViewModel(string sessionId, AspNetUser user, CartViewModel cart);
    }
}