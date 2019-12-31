using AutoMapper;
using BlueTapeCrew.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using Services.Models;
using System;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class UserService : IUserService, IDisposable
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IGuestUserService _guestUserService;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IMapper mapper, IGuestUserService guestUserService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _guestUserService = guestUserService;
        }

        public async Task<User> Find(string email)
        {
            if (string.IsNullOrEmpty(email)) return null;
            var applicationUser = await _userManager.FindByEmailAsync(email);
            if (applicationUser == null) return null;
            var user = _mapper.Map<User>(applicationUser);
            user.EmailIsConfirmed = await _userManager.IsEmailConfirmedAsync(applicationUser);
            return user;
        }

        public Task<GuestUser> GetGuestUser(string sessionId) => _guestUserService.FindBy(sessionId);

        public async Task<bool> UpdateUser(CheckoutRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null) throw new Exception("user not found");
            user.City = model.City;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PostalCode = model.PostalCode;
            user.State = model.State;
            user.Address = model.Address;
            user.PhoneNumber = model.Phone;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public Task CreateGuestUser(GuestUser model) => _guestUserService.Create(model);

        public void Dispose() => _userManager?.Dispose();
    }
}