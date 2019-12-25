using BlueTapeCrew.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<bool> ResetPassword(ResetPasswordRequest model);
        Task SendEmailConfirmationLink(HttpRequest request, string username);
        Task<bool> ConfirmEmail(string userId, string encodedToken);
        Task<bool> CreateUser(string email, string password);
        Task<bool> SendPasswordResetLink(HttpRequest request, string email);
        Task<bool> ChangePassword(string userName, string oldPassword, string newPassword);
        Task SignInBy(string username);
        Task<bool> SetPassword(string username, string password);
    }
}