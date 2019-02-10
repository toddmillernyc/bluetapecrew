using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Services.Interfaces;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class UserService : IUserService
    {
        private readonly BtcEntities _db = new BtcEntities();

        public async Task<AspNetUser> GetUserByName(string name)
        {
            return await _db.AspNetUsers.FirstOrDefaultAsync(x => x.UserName.Equals(name));
        }

        public async Task<GuestUser> GetGuestUser(string sessionId)
        {
            return await _db.GuestUsers.FirstOrDefaultAsync(x => x.SessionId.Equals(sessionId));
        }

        //TODO: try to give checkoutviewmodel a user object rather than flat
        public async Task UpdateUser(string userName, string firstName, string lastName,
            string address, string city, string state, string zip, string phone, string email)
        {
            var dbUser = await _db.AspNetUsers.FirstOrDefaultAsync(x => x.UserName.Equals(userName));
            dbUser.FirstName = firstName;
            dbUser.LastName = lastName;
            dbUser.Address = address;
            dbUser.City = city;
            dbUser.State = state;
            dbUser.PostalCode = zip;
            dbUser.PhoneNumber = phone;
            dbUser.Email = email;
            await _db.SaveChangesAsync();
        }

        public async Task CreateGuestUser(string sessionId, string firstName, string lastName,
            string address, string city, string state, string zip, string phone, string email)
        {
            var dbUser = new GuestUser
            {
                SessionId = sessionId,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                State = state,
                PostalCode = zip,
                PhoneNumber = phone,
                Email = email,

            };
            _db.GuestUsers.Add(dbUser);
            await _db.SaveChangesAsync();
        }
    }
}