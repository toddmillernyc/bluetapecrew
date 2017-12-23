using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Repositories;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Services
{
    public class SiteSettingsService : ISiteSettingsService
    {
        private readonly ISettingsRepository _settingsRepository;

        public SiteSettingsService(ISendgridSettingsRepository sendgridSettingsRepository, ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<SiteSetting> GetSettings()
        {
            return await _settingsRepository.Get();
        }
    }
}