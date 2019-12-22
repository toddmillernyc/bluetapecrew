using System.Threading.Tasks;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BlueTapeCrew.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private const string Subject = "Confirm your email address";

        private readonly IEmailService _emailSender;
        private readonly ISiteSettingsService _settings;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRegistrationService(
            IEmailService emailSender,
            ISiteSettingsService settings,
            UserManager<ApplicationUser> userManager)
        {
            _emailSender = emailSender;
            _settings = settings;
            _userManager = userManager;
        }

        public async Task SendEmailConfirmationLink(HttpRequest request, ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = $"{request?.Scheme}://{request?.Host}{request?.PathBase}/Account/ConfirmEmail/{user.Id}?code={code}";
            var settings = await _settings.Get();
            var htmlBody = $"Please confirm your account by clicking <a href=\"{callbackUrl}\">here</a>";
            var smtpRequest = new SmtpRequest(settings, htmlBody, callbackUrl, user.Email, Subject);
            await _emailSender.SendEmail(smtpRequest);
        }
    }
}
