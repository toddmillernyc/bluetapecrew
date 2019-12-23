using BlueTapeCrew.Services.Interfaces;
using Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public LoginService(SignInManager<ApplicationUser> signInManager) { _signInManager = signInManager; }

        public async Task<SignInResult> Login(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            return result;
        }
        public async Task Logout() => await _signInManager.SignOutAsync();
    }
}
