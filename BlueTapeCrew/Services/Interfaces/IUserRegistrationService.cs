using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<bool> ResetPassword(ResetPasswordRequest model);
        Task SendEmailConfirmationLink(HttpRequest request, string username);
        Task<bool> ConfirmEmail(string userId, string encodedToken);
        Task<bool> CreateUser(string email, string password);
        Task<bool> SendPasswordResetLink(HttpRequest request, string email);
    }
}