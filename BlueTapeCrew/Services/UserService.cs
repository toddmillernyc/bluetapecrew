using BlueTapeCrew.Models;
using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using AutoMapper;

namespace BlueTapeCrew.Services
{
    public class UserService : IUserService, IDisposable
    {
        private readonly IGuestUserRepository _guestUsers;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IGuestUserRepository guestUsers, IMapper mapper)
        {
            _userManager = userManager;
            _guestUsers = guestUsers;
            _mapper = mapper;
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

        public async Task<GuestUser> GetGuestUser(string sessionId) => await _guestUsers.FindBy(sessionId);

        public async Task UpdateUser(CheckoutRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) throw new Exception("user not found");
            user = _mapper.Map<ApplicationUser>(model);
            await _userManager.UpdateAsync(user);
        }

        public async Task CreateGuestUser(GuestUser model) => await _guestUsers.Create(model);

        public void Dispose()
        {
            _userManager?.Dispose();
        }
    }
}