using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using Entities;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class SiteSettingsService : ISiteSettingsService
    {
        private readonly ISiteSettingsRepository _repository;

        public SiteSettingsService(ISiteSettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<SiteSetting> Get() => await _repository.Get();

        public async Task<SiteSetting> Set(SiteSetting siteSetting)
        {
            await _repository.DeleteAll();
            await _repository.Create(siteSetting);
            return siteSetting;
        }
    }
}