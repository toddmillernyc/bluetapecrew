using System.Threading.Tasks;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IUserService
    {
        Task<AspNetUser> GetUserByName(string name);
        Task<GuestUser> GetGuestUser(string sessionId);
        Task UpdateUser(CheckoutViewModel model);

        Task CreateGuestUser(CheckoutViewModel model);
    }
}
