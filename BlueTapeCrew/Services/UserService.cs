using System.Data.Entity;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Services
{
    public class UserService : IUserService
    {
        public async Task<AspNetUser> GetUserByName(string name)
        {
            using (var db = new BtcEntities())
            {
                return await db.AspNetUsers.FirstOrDefaultAsync(x => x.UserName.Equals(name));
            }
        }

        public async Task<GuestUser> GetGuestUser(string sessionId)
        {
            using (var db = new BtcEntities())
            {
                return await db.GuestUsers.FirstOrDefaultAsync(x => x.SessionId.Equals(sessionId));
            }

        }

        //TODO: try to give checkoutviewmodel a user object rather than flat
        public async Task UpdateUser(string userName,string firstName, string lastName,
            string address,string city, string state,string zip, string phone, string email)
        {
            using (var db = new BtcEntities())
            {
                var dbUser = await db.AspNetUsers.FirstOrDefaultAsync(x => x.UserName.Equals(userName));
                dbUser.FirstName = firstName;
                dbUser.LastName = lastName;
                dbUser.Address = address;
                dbUser.City = city;
                dbUser.State = state;
                dbUser.PostalCode = zip;
                dbUser.PhoneNumber = phone;
                dbUser.Email = email;
                await db.SaveChangesAsync();
            }
        }

        public async Task CreateGuestUser(string sessionId, string firstName, string lastName,
            string address, string city, string state, string zip, string phone, string email)
        {
            using (var db = new BtcEntities())
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
                db.GuestUsers.Add(dbUser);
                await db.SaveChangesAsync();
            }
        }

    }
}