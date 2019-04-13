using Api.Models.Entities;
using Api.Repositories.Interfaces;
using Api.Services.Interfaces;
using System.Threading.Tasks;

namespace Api.Services
{
    public class SiteSettingsService : ISiteSettingsService
    {
        private readonly ISiteSettingsRepository _settingsRepository;

        public SiteSettingsService(ISiteSettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<SiteSetting> Get()
        {
            return await _settingsRepository.Get();
        }

        public async Task<SiteSetting> Set(SiteSetting siteSetting)
        {
            return await _settingsRepository.Set(siteSetting);
        }
    }
}