using BlueTapeCrew.Services.Interfaces;
using MailChimp;
using MailChimp.Helper;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class EmailSubscriptionService : IEmailSubscriptionService
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public EmailSubscriptionService(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public async Task<string> Subscribe(string email)
        {
            var settings = await _siteSettingsService.Get();
            var mc = new MailChimpManager(settings.MailChimpApiKey);
            var mcEmail = new EmailParameter { Email = email };
            var results = mc.Subscribe(settings.MailChimpListId, mcEmail);
            return results.Email;
        }
    }
}