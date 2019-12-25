using BlueTapeCrew.ViewModels;
using System.Threading.Tasks;
using BlueTapeCrew.Models;
using Entities;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IUserService
    {
        Task<GuestUser> GetGuestUser(string sessionId);
        Task<bool> UpdateUser(CheckoutRequest model);
        Task CreateGuestUser(GuestUser model);
        Task<User> Find(string email);
    }
}
