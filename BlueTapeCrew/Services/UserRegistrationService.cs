using BlueTapeCrew.Models;
using BlueTapeCrew.Services.Interfaces;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;

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
            var token = await GetEncodedToken(user);
            var callbackUrl = $"{request?.Scheme}://{request?.Host}{request?.PathBase}/Account/ConfirmEmail/{user.Id}?code={token}";
            var settings = await _settings.Get();
            var htmlBody = $"Please confirm your account by clicking <a href=\"{callbackUrl}\">here</a>";
            var smtpRequest = new SmtpRequest(settings, htmlBody, callbackUrl, user.Email, Subject);
            await _emailSender.SendEmail(smtpRequest);
        }

        public async Task<IdentityResult> ConfirmEmail(string userId, string encodedToken)
        {
            var decodedBytes = WebEncoders.Base64UrlDecode(encodedToken);
            var code = Encoding.UTF8.GetString(decodedBytes);
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result;
        }

        private async Task<string> GetEncodedToken(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);
            return encodedToken;
        }
    }
}
