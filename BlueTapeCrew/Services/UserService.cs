using System;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Services.Interfaces;
using System.Data.Entity;
using System.Threading.Tasks;
using BlueTapeCrew.ViewModels;

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
        public async Task UpdateUser(CheckoutViewModel model)
        {
            var dbUser = await _db.AspNetUsers.FirstOrDefaultAsync(x => x.UserName.Equals(model.UserName));
            if(dbUser == null) throw new Exception("user not found");
            dbUser.FirstName = model.FirstName;
            dbUser.LastName = model.LastName;
            dbUser.Address = model.Address;
            dbUser.City = model.City;
            dbUser.State = model.State;
            dbUser.PostalCode = model.Zip;
            dbUser.PhoneNumber = model.Phone;
            dbUser.Email = model.Email;
            await _db.SaveChangesAsync();
        }

        public async Task CreateGuestUser(CheckoutViewModel model)
        {
            var dbUser = new GuestUser
            {
                SessionId = model.SessionId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                City = model.City,
                State = model.State,
                PostalCode = model.Zip,
                PhoneNumber = model.Phone,
                Email = model.Email,

            };
            _db.GuestUsers.Add(dbUser);
            await _db.SaveChangesAsync();
        }
    }
}