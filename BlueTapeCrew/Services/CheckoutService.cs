using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Services
{
    public class CheckoutService : ICheckoutService
    {
        public CheckoutViewModel GetCheckoutViewModel(string sessionId,AspNetUser user,CartViewModel cart)
        {
            if (user == null) return new CheckoutViewModel {Cart = cart};
            return new CheckoutViewModel
                {
                    Cart = cart,
                    Address = user.Address,
                    City = user.City,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.PhoneNumber,
                    State = user.State,
                    Zip = user.PostalCode
                };
        }
    }
}