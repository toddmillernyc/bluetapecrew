using BlueTapeCrew.ViewModels;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IUserService
    {
        Task<GuestUser> GetGuestUser(string sessionId);
        Task UpdateUser(CheckoutViewModel model);
        Task CreateGuestUser(GuestUser model);
    }
}
