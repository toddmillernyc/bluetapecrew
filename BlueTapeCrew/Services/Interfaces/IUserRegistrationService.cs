using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IUserRegistrationService
    {
        Task SendEmailConfirmationLink(HttpRequest request, ApplicationUser user);
        Task<IdentityResult> ConfirmEmail(string userId, string encodedToken);
    }
}
