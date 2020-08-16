using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Services.Interfaces;
using Services.Models;
using Site.Identity;

namespace Site.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private const string Subject = "Confirm your email address";

        private readonly IEmailService _emailSender;
        private readonly ISiteSettingsService _settings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserRegistrationService(
            IEmailService emailSender,
            ISiteSettingsService settings,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _emailSender = emailSender;
            _settings = settings;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null) return false;
            var token = DecodeToken(model.Code);
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            return result.Succeeded;
        }

        public async Task SendEmailConfirmationLink(HttpRequest request, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var token = await GetEncodedEmailConfirmToken(user);
            var callbackUrl = $"{request?.Scheme}://{request?.Host}{request?.PathBase}/Account/ConfirmEmail/{user.Id}?code={token}";
            var settings = await _settings.Get();
            var htmlBody = $"Please confirm your account by clicking <a href=\"{callbackUrl}\">here</a>";
            var smtpRequest = new SmtpRequest(settings, htmlBody, callbackUrl, user.Email, Subject);
            await _emailSender.SendEmail(smtpRequest);
        }

        public async Task<bool> SendPasswordResetLink(HttpRequest request, string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null) return false;

            var emailIsConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!emailIsConfirmed) return false;

            var token = await GetEncodedPasswordResetToken(user);
            var callbackUrl = $"{request?.Scheme}://{request?.Host}{request?.PathBase}/Account/ResetPassword/{user.Id}?code={token}";
            var settings = await _settings.Get();
            var htmlBody = $"Please reset your password by clicking <a href=\"{callbackUrl}\">here</a>";
            var textBody = callbackUrl;
            var smtpRequest = new SmtpRequest(settings, htmlBody, textBody, user.UserName, Constants.Account.ResetPasswordEmailSubject);
            await _emailSender.SendEmail(smtpRequest);
            return true;
        }

        public async Task<bool> ConfirmEmail(string userId, string encodedToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, DecodeToken(encodedToken));
            return result.Succeeded;
        }

        public async Task<IdentityResult> CreateUser(string email, string password)
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<bool> ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return false;
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result.Succeeded;
        }

        public async Task SignInBy(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            await _signInManager.SignInAsync(user, false);
        }

        public async Task<bool> SetPassword(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var result = await _userManager.AddPasswordAsync(user, password);
            return result.Succeeded;
        }

        private async Task<string> GetEncodedEmailConfirmToken(ApplicationUser user)
            => EncodeToken(await _userManager.GenerateEmailConfirmationTokenAsync(user));

        private async Task<string> GetEncodedPasswordResetToken(ApplicationUser user)
            => EncodeToken(await _userManager.GeneratePasswordResetTokenAsync(user));

        private static string EncodeToken(string token)
        {
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);
            return encodedToken;
        }

        private static string DecodeToken(string encodedToken)
        {
            var encodedBytes = WebEncoders.Base64UrlDecode(encodedToken);
            var decodedToken = Encoding.UTF8.GetString(encodedBytes);
            return decodedToken;
        }
    }
}
