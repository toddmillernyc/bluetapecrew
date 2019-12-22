using BlueTapeCrew.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IUserRegistrationService
    {
        Task SendEmailConfirmationLink(HttpRequest request, ApplicationUser user);
        Task<IdentityResult> ConfirmEmail(string userId, string encodedToken);
    }
}
