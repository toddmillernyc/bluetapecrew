using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.Models;

namespace Site.Services
{
    public interface IUserRegistrationService
    {
        Task<bool> ResetPassword(ResetPasswordRequest model);
        Task SendEmailConfirmationLink(HttpRequest request, string username);
        Task<bool> ConfirmEmail(string userId, string encodedToken);
        Task<IdentityResult> CreateUser(string email, string password);
        Task<bool> SendPasswordResetLink(HttpRequest request, string email);
        Task<bool> ChangePassword(string userName, string oldPassword, string newPassword);
        Task SignInBy(string username);
        Task<bool> SetPassword(string username, string password);
    }
}