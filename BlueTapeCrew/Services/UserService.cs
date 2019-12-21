using BlueTapeCrew.Data;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class UserService : IUserService, IDisposable
    {
        private readonly BtcEntities _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager, BtcEntities db)
        {
            _userManager = userManager;
            _db = db;
        }


        public async Task<GuestUser> GetGuestUser(string sessionId)
        {
            return await _db.GuestUsers.FirstOrDefaultAsync(x => x.SessionId.Equals(sessionId));
        }

        //TODO: try to give checkoutviewmodel a user object rather than flat
        public async Task UpdateUser(CheckoutViewModel model)
        {
            var dbUser = await _userManager.FindByNameAsync(model.UserName);
            if (dbUser == null) throw new Exception("user not found");
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

        public async Task CreateGuestUser(GuestUser model)
        {
            _db.GuestUsers.Add(model);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db?.Dispose();
            _userManager?.Dispose();
        }
    }
}