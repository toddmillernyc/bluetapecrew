using System.Threading.Tasks;
using BlueTapeCrew.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IUserRegistrationService
    {
        Task SendEmailConfirmationLink(HttpRequest request, ApplicationUser user);
    }
}
