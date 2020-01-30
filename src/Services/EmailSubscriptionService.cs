using System.Threading.Tasks;
using MailChimp.Net;
using MailChimp.Net.Models;
using Services.Interfaces;

namespace Services
{
    public class EmailSubscriptionService : IEmailSubscriptionService
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public EmailSubscriptionService(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public async Task Subscribe(string email)
        {
            var settings = await _siteSettingsService.Get();
            var mc = new MailChimpManager(settings.MailChimpApiKey);
            var member = new Member { EmailAddress = email, StatusIfNew = Status.Subscribed };
            await mc.Members.AddOrUpdateAsync(settings.MailChimpListId, member);
        }
    }
}