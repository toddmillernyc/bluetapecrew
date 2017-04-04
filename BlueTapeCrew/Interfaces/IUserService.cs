using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Interfaces
{
    public interface IUserService
    {
        Task<AspNetUser> GetUserByName(string name);
        Task<GuestUser> GetGuestUser(string sessionId);
        Task UpdateUser(string userName, string firstName, string lastName,
            string address, string city, string state, string zip, string phone, string email);

        Task CreateGuestUser(string sessionId, string firstName, string lastName,
            string address, string city, string state, string zip, string phone, string email);
    }
}
