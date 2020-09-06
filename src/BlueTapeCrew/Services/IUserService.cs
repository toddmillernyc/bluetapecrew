﻿using System.Threading.Tasks;
using Services.Models;

namespace BlueTapeCrew.Services
{
    public interface IUserService
    {
        Task<GuestUser> GetGuestUser(string sessionId);
        Task<bool> UpdateUser(CheckoutRequest model);
        Task CreateGuestUser(GuestUser model);
        Task<User> Find(string email);
    }
}
